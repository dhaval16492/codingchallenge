import { Component, TemplateRef, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Device } from '../models/device.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { DeviceState } from '../models/device.state';
import { addDevice, addDeviceFailure, deleteDevice, loadDevices, updateDevice } from '../actions/device.actions';
import { AppConfirmationDialogComponent } from '../shared/app-confirmation-dialog/app-confirmation-dialog.component';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { PaginatedList } from '../models/paginated-list.model';
import { ToasterService } from '../service/toaster.service';
import { selectDeviceError } from '../selector/device.selector';

@Component({
  selector: 'app-device',
  templateUrl: './device.component.html',
  styleUrls: ['./device.component.css']
})
export class DeviceComponent {
  devices: Observable<Device[]>;
  deviceDetail: Observable<PaginatedList<Device>>;

  displayedColumns: string[] = ['type', 'description', 'edit', 'delete'];
  isAddDeviceModalVisible = false;
  deviceForm!: FormGroup;
  isEditing: boolean = false;
  editedDeviceId: number = 0;
  loading$: Observable<boolean>;
  filterType: string = '';
  filterDescription: string = '';
  filter: Device;
  loading: boolean = false;

  @ViewChild('addDeviceDialogTemplate', { static: true }) addDeviceDialogTemplate!: TemplateRef<any>;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  totalRows = 0;
  pageSize = 10;
  currentPage = 0;
  pageSizeOptions: number[] = [10, 25, 100];
  constructor(private store: Store<{ devices: DeviceState }>, private fb: FormBuilder, private dialog: MatDialog,
    private toasterService: ToasterService) {
    this.devices = this.store.select((state) => state.devices?.devices?.paginatedList ?? []);
    this.deviceDetail = this.store.select((state) => state.devices?.devices ?? { totalPages: 0, paginatedList: [], totalCount: 0 });
    this.deviceDetail.subscribe(x => {
      this.totalRows = x.totalCount;
    })   

    this.loading$ = this.store.select((state) => state.devices?.loading);


    this.filter = {
      id: 0,
      type: '',
      description: '',
      pageNumber: this.currentPage,
      pageSize: this.pageSize
    }
  }

  /**
   * Initialize the component.
   */
  ngOnInit() {
    this.loadDevices();

    this.deviceForm = this.fb.group({
      type: ['', Validators.required],
      description: ['', [Validators.required]]
    });
  }

  /**
   * Load the list of devices.
   */
  loadDevices() {
    this.store.dispatch(loadDevices({ devices: this.filter }));
  }

  /**
   * Apply filter based on type and description.
   */
  applyFilter(): void {
    this.store.dispatch(loadDevices({ devices: { id: 0, type: this.filterType, description: this.filterDescription, pageSize: this.pageSize ?? 0, pageNumber: this.currentPage ?? 0 } }));

  }

  /**
   * Save a new or updated device.
   * @param type - The type of the device.
   * @param description - The description of the device.
   */
  saveDevice(type: string, description: string) {
    if (this.deviceForm.valid) {
      let device: Device = {
        id: 0,
        type: type,
        description: description,
      };

      if (this.isEditing) {
        const updatedDevice: Device = {
          id: this.editedDeviceId,
          ...this.deviceForm.value,
        };
        this.store.dispatch(
          updateDevice({ device: updatedDevice })
        );
        this.editedDeviceId = 0;

      } else {
        this.store.dispatch(addDevice({ device: device }));
      }
      this.isEditing = false;
      this.closeAddDeviceDialog();

    }
  }

  /**
   * Open the device dialog for editing or adding.
   * @param device - The device to edit or null to add a new one.
   */
  openDeviceDialog(device?: Device): void {
    this.dialog.open(this.addDeviceDialogTemplate, {
      width: '500px', // Adjust width as needed
      data: { device }
    });
    if (device) {
      this.editedDeviceId = device.id;
      this.deviceForm.patchValue({
        type: device.type,
        description: device.description,
      });
      this.isEditing = true;
    } else {
      this.editedDeviceId = 0;
      this.isEditing = false;

    }
  }

  /**
   * Close the add device dialog.
   */
  closeAddDeviceDialog(): void {
    this.deviceForm.reset();
    this.dialog.closeAll();
  }

  /**
   * Open a confirmation dialog for deleting a device.
   * @param id - The ID of the device to be deleted.
   */
  openConfirmationDialog(id: number): void {
    const dialogRef = this.dialog.open(AppConfirmationDialogComponent, {
      width: '300px', // Adjust the width as needed
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.store.dispatch(deleteDevice({ id }));
      }
    });
  }

  /**
   * Handle page change event.
   * @param event - The PageEvent containing page size and index.
   */
  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.applyFilter();
  }
}

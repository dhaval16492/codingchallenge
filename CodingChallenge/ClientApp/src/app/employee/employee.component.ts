import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AppConfirmationDialogComponent } from '../shared/app-confirmation-dialog/app-confirmation-dialog.component';
import { addEmployee, deleteEmployee, loadEmployees, updateEmployee } from '../actions/employee.actions';
import { EmployeeState } from '../models/employee.state';
import { DeviceState } from '../models/device.state';
import { Device } from '../models/device.model';
import { loadDevices } from '../actions/device.actions';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { PaginatedList } from '../models/paginated-list.model';
import { ToasterService } from '../service/toaster.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  employees: Observable<Employee[]>;
  employeeDetail: Observable<PaginatedList<Employee>>;

  displayedColumns: string[] = ['name', 'email', 'device', 'edit', 'delete'];
  isAddEmployeeModalVisible = false;
  employeeForm!: FormGroup;
  isEditing: boolean = false;
  editedEmployeeId: number = 0;
  deviceList: Observable<Device[]>;
  loading$: Observable<boolean>;
  selectedDeviceIds: number[] = [];
  devices: Device[] = [];
  filterName: string = '';
  filterEmail: string = '';


  filterDevice: Device = {
    id: 0,
    type: '',
    description: '',
    pageSize: 0//For getting all data without pagination
  };

  @ViewChild('addEmployeeDialogTemplate', { static: true }) addEmployeeDialogTemplate!: TemplateRef<any>;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  totalRows = 0;
  pageSize = 10;
  currentPage = 0;
  pageSizeOptions: number[] = [10, 25, 100];

  matcher: any;

  /**
   * Filter for employees
   */
  filterEmployee: Employee = {
    id: 0,
    name: '',
    email: '',
    pageNumber: this.currentPage,
    pageSize: this.pageSize
  }
  constructor(private store: Store<{ employees: EmployeeState }>,
    private deviceStore: Store<{ devices: DeviceState }>,
    private fb: FormBuilder, private dialog: MatDialog) {
    this.employees = this.store.select((state) => state.employees.employees?.paginatedList ?? []);
    this.employeeDetail = this.store.select((state) => state.employees?.employees ?? { totalPages: 0, paginatedList: [], totalCount: 0 });
    this.employeeDetail.subscribe(x => {
      this.totalRows = x.totalCount;
    })
    this.deviceList = this.deviceStore.select((state) => state.devices.devices?.paginatedList ?? []);

    this.loading$ = this.store.select((state) => state.employees.loading);
    this.loading$ = this.deviceStore.select((state) => state.devices.loading);


  }
  ngOnInit() {
    this.loadEmployees();
    this.store.dispatch(loadDevices({ devices: this.filterDevice }));
    this.deviceList.subscribe(x => {
      this.devices = x;

    });
    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      selectedDevices: [],
    });

  }

  /**
   * Get the names of selected devices
   * @param deviceList - List of devices
   */
  getDeviceNames(deviceList: Device[]): string {
    return deviceList?.map((device) => device.type).join(', ');
  }

  /**
   * Load employees and apply filtering
   */
  loadEmployees() {
    this.store.dispatch(loadEmployees({ employee: this.filterEmployee }));
  }

  /**
   * Apply filtering to employees
   */
  applyFilter(): void {
    this.store.dispatch(loadEmployees({ employee: { id: 0, name: this.filterName, email: this.filterEmail, pageSize: this.pageSize ?? 0, pageNumber: this.currentPage ?? 0 } }));

  }

  /**
   * Save or update an employee
   * @param name - Employee name
   * @param email - Employee email
   * @param device - Selected devices
   */
  saveEmployee(name: string, email: string, device: any) {

    if (this.employeeForm.valid) {
      let employee: Employee = {
        id: 0,
        name: name,
        email: email,
        deviceList: this.devices.filter(x => this.selectedDeviceIds.includes(x.id))
      };

      if (this.isEditing) {
        const updatedEmployee: Employee = {
          id: this.editedEmployeeId,
          deviceList: this.devices.filter(x => this.selectedDeviceIds.includes(x.id)),
          ...this.employeeForm.value,
        };
        this.store.dispatch(
          updateEmployee({ employee: updatedEmployee })
        );
        this.editedEmployeeId = 0;
      } else {
        this.store.dispatch(addEmployee({ employee: employee }));
      }
      this.isEditing = false;
      this.closeAddEmployeeDialog();

    }
  }

  /**
   * Open the employee dialog for editing or adding a new employee
   * @param employee - The employee to edit
   */
  openEmployeeDialog(employee?: Employee): void {
    this.selectedDeviceIds = [...employee?.deviceList?.map(x => x.id) ?? []]; this.dialog.open(this.addEmployeeDialogTemplate, {
      width: '500px', // Adjust width as needed
      data: { employee }
    });
    if (employee) {
      this.editedEmployeeId = employee.id;
      this.employeeForm.patchValue({
        name: employee.name,
        email: employee.email,
      });
      this.isEditing = true;
    } else {
      this.editedEmployeeId = 0;
      this.isEditing = false;

    }
  }

  /**
   * Close the employee dialog
   */
  closeAddEmployeeDialog(): void {
    this.employeeForm.reset();
    this.dialog.closeAll();
  }

  /**
   * Open a confirmation dialog for deleting an employee
   * @param id - The ID of the employee to delete
   */
  openConfirmationDialog(id: number): void {
    const dialogRef = this.dialog.open(AppConfirmationDialogComponent, {
      width: '300px', // Adjust the width as needed
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.store.dispatch(deleteEmployee({ id }));
      }
    });
  }

  /**
   * Handle page change event
   * @param event - Page event
   */
  pageChanged(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex;
    this.applyFilter();
  }
}

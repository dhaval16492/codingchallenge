import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';

import { Device } from '../models/device.model'; // Import your Device model
import { DeviceService } from '../service/device.service';
import * as DeviceActions from '../actions/device.actions';
import { HttpErrorResponse } from '@angular/common/http';
import { ToasterService } from '../service/toaster.service';

@Injectable()
export class DeviceEffects {
  constructor(
    private actions$: Actions,
    private deviceService: DeviceService, // Inject your DeviceService
    private toasterService: ToasterService
  ) { }

  /**
   * Effect for loading devices
   */
  loadDevices$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeviceActions.loadDevices),
      mergeMap((action) =>
        this.deviceService.getDevices(action.devices).pipe(
          map((devices) => DeviceActions.loadDevicesSuccess({ devices })),
          catchError((error) => of(DeviceActions.loadDevicesFailure({ error })))
        )
      )
    )
  );

  /**
   * Effect for adding an device 
   */
  addDevice$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeviceActions.addDevice),
      mergeMap((action) =>
        this.deviceService.addDevice(action.device).pipe(
          map((device: Device) => {
            this.toasterService.showSuccess('Device added successfully');
            return DeviceActions.addDeviceSuccess({ device });

          }),
          catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              console.log(error.error.error)
              // Handle the CustomException error
              this.toasterService.showError(error.error.error);
              return of(DeviceActions.addDeviceFailure({ error: error.error }));
            } else {
              // Handle other errors
              this.toasterService.showError('An error occurred');
              console.error('An error occurred:', error);
              // Dispatch an action for a generic error or other handling
              return of(DeviceActions.addDeviceFailure({ error: 'An error occurred' }));
            }
          })
        )
      )
    )
  );

  /**
   * Effect for updating an device
   */
  updateDevice$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeviceActions.updateDevice),
      mergeMap((action) =>
        this.deviceService.updateDevice(action.device).pipe(
          map((device: Device) => {
            this.toasterService.showSuccess('Device updated successfully');
            return DeviceActions.updateDeviceSuccess({ device })

          }),
          catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              // Handle the CustomException error
              this.toasterService.showError('test');
              return of(DeviceActions.updateDeviceFailure({ error }));
            } else {
              // Handle other errors
              this.toasterService.showError('An error occurred');
              // Dispatch an action for a generic error or other handling
              return of(DeviceActions.updateDeviceFailure({ error }));
            }
          })
        )
      )
    )
  );

  /**
   * Effect for deleting an device
   */
  deleteDevice$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeviceActions.deleteDevice),
      mergeMap((action) =>
        this.deviceService.deleteDevice(action.id).pipe(
          map(() => {
            this.toasterService.showSuccess('Device deleted successfully');
            return DeviceActions.deleteDeviceSuccess({ id: action.id })

          } ),
          catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              // Handle the CustomException error
              this.toasterService.showError(error.error.error);
              return of(DeviceActions.deleteDeviceFailure({ error }));
            } else {
              // Handle other errors
              this.toasterService.showError('An error occurred');
              // Dispatch an action for a generic error or other handling
              return of(DeviceActions.deleteDeviceFailure({ error }));
            }
          })
        )
      )
    )
  );

  /**
   * Effect for getting an device by ID
   */
  getDeviceById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeviceActions.getDeviceById),
      mergeMap((action) =>
        this.deviceService.getDeviceById(action.id).pipe(
          map((device) =>
            DeviceActions.getDeviceByIdSuccess({ device })
          ),
          catchError((error) =>
            of(DeviceActions.getDeviceByIdFailure({ error }))
          )
        )
      )
    )
  );
}

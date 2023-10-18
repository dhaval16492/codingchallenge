import { createAction, props } from '@ngrx/store';
import { PaginatedList } from '../models/paginated-list.model';
import { Device } from '../models/device.model';

/**
 * LoadDevice
 */
export const loadDevices = createAction('[Device] Load Devices', props<{ devices: Device }>());

/**
 * Load Device Success
 */
export const loadDevicesSuccess = createAction('[Device] Load Devices Success', props<{ devices: PaginatedList<Device> }>());

/**
 * Load Device Failure
 */
export const loadDevicesFailure = createAction('[Device] Load Devices Failure', props<{ error: any }>());

/**
 * Add Device
 */
export const addDevice = createAction(
  '[Device] Add Device',
  props<{ device: Device }>()
);

/**
 * Add Device Success
 */
export const addDeviceSuccess = createAction(
  '[Device] Add Device Success',
  props<{ device: Device }>()
);

/**
 * Add Device Failure
 */
export const addDeviceFailure = createAction(
  '[Device] Add Device Failure',
  props<{ error: any }>()
);

/**
 * Update Device
 */
export const updateDevice = createAction(
  '[Device] Update Device',
  props<{ device: Device }>()
);

/**
 * Update Device Success
 */
export const updateDeviceSuccess = createAction(
  '[Device] Update Device Success',
  props<{ device: Device }>()
);

/**
 * Update Device failure
 */
export const updateDeviceFailure = createAction(
  '[Device] Update Device Failure',
  props<{ error: any }>()
);

/**
 * Delete Device
 */
export const deleteDevice = createAction(
  '[Device] Delete Device',
  props<{ id: number }>()
);

/**
 * Delete Device Success
 */
export const deleteDeviceSuccess = createAction(
  '[Device] Delete Device Success',
  props<{ id: number }>()
);

/**
 * Delete Device failure
 */
export const deleteDeviceFailure = createAction(
  '[Device] Delete Device Failure',
  props<{ error: any }>()
);

/**
 * Get device by Id
 */
export const getDeviceById = createAction(
  '[Device] Get Device by ID',
  props<{ id: number }>()
);

/**
 * get Device by id Success
 */
export const getDeviceByIdSuccess = createAction(
  '[Device] Get Device Success',
  props<{ device: Device }>()
);

/**
 * Get device By id failure
 */
export const getDeviceByIdFailure = createAction(
  '[Device] Get Device Failure',
  props<{ error: any }>()
);

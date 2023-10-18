import { createReducer, on } from '@ngrx/store';
import * as DeviceActions from '../actions/device.actions';
import { DeviceState } from '../models/device.state';

export const initialState: DeviceState = {
  devices: null,
  loading: false,
  error: null
};

// Create a reducer for the device entity
export const deviceReducer = createReducer(
  initialState,

  // Load Devices
  on(DeviceActions.loadDevices, (state) => ({ ...state, loading: true })),
  on(DeviceActions.loadDevicesSuccess, (state, { devices }) => ({ ...state, devices, loading: false })),
  on(DeviceActions.loadDevicesFailure, (state, { error }) => ({ ...state, error, loading: false })),

  // Add Device

  on(DeviceActions.addDevice, (state) => ({ ...state, loading: true })),
  on(DeviceActions.addDeviceSuccess, (state, { device }) => ({
    ...state,
    devices: state.devices ? {
      ...state.devices,
      paginatedList: [...state.devices.paginatedList, device],
      totalCount: state.devices.totalCount + 1
    } : null,
    loading: false,
    error:null
  })),
  on(DeviceActions.addDeviceFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  })),

  // Update Device
  on(DeviceActions.updateDevice, (state) => ({ ...state, loading: true })),
  on(DeviceActions.updateDeviceSuccess, (state, { device }) => ({
    ...state,
    devices: state.devices
      ? {
        ...state.devices,
        paginatedList: state.devices.paginatedList.map((deviceDetail) =>
          deviceDetail.id === device.id ? device : deviceDetail
        ),
      }
      : null,
    loading: false
  })),
  on(DeviceActions.updateDeviceFailure, (state, { error }) => ({
    ...state,
    loading:false,
    error
  })),

  // Delete Device
  on(DeviceActions.deleteDevice, (state) => ({ ...state, loading: true })),
  on(DeviceActions.deleteDeviceSuccess, (state, { id }) => ({
    ...state,
    devices: state.devices
      ? {
        ...state.devices,
        paginatedList: state.devices.paginatedList.filter(
          (device) => device.id !== id
        ),
        totalCount: state.devices.totalCount - 1,
      }
      : null,
    loading: false
  })),
  on(DeviceActions.deleteDeviceFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  })),
  on(DeviceActions.getDeviceByIdSuccess, (state, { device }) => ({
    ...state,    
    device,
    loading: false
  }))
);


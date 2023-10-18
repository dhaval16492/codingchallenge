import { createSelector } from '@ngrx/store';
import { DeviceState } from '../models/device.state';

export const selectDeviceError = createSelector(
  (state: { devices: DeviceState }) => state.devices.error,
  (error) => error
);

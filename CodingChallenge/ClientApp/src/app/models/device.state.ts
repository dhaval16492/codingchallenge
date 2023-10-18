import { Device } from "./device.model";
import { PaginatedList } from "./paginated-list.model";

/**
 * Represents the state for devices in the application.
 */
export interface DeviceState {
  devices: PaginatedList<Device> | null; // List of devices with pagination information
  loading: boolean; // Indicates if devices are being loaded
  error: any; // Stores any error that occurs during device-related actions
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'; // Import your environment settings
import { Device } from '../models/device.model';
import { PaginatedList } from '../models/paginated-list.model';

@Injectable({
  providedIn: 'root',
})
export class DeviceService {
  private apiUrl = `${environment.apiBaseUrl}/device`; // Set your API URL here

  constructor(private http: HttpClient) { }

  /**
   * Fetch all devices from the API.
   * @param devices - Object containing filtering parameters for devices.
   * @returns An observable of a paginated list of devices.
   */
  getDevices(devices: Device): Observable<(PaginatedList<Device>)> {
    return this.http.get<PaginatedList<Device>>(this.apiUrl, { params: { type: devices.type, description: devices.description, pageSize: devices.pageSize ?? 0, pageNumber: devices.pageNumber ?? 0 } });
  }

  /**
   * Add a new device to the API.
   * @param device - The device object to be added.
   * @returns An observable of the added device.
   */
  addDevice(device: Device): Observable<Device> {
    return this.http.post<Device>(this.apiUrl, device);
  }

  /**
   * Update an existing device in the API.
   * @param device - The device object to be updated.
   * @returns An observable of the updated device.
   */
  updateDevice(device: Device): Observable<Device> {
    const url = `${this.apiUrl}`;
    return this.http.put<Device>(url, device);
  }

  /**
   * Delete a device by ID from the API.
   * @param id - The ID of the device to be deleted.
   * @returns An observable indicating the success of the delete operation.
   */
  deleteDevice(id: number): Observable<void> {
    const url = `${this.apiUrl}?id=${id}`;
    return this.http.delete<void>(url);
  }

  /**
   * Get a device by ID from the API.
   * @param id - The ID of the device to be retrieved.
   * @returns An observable of the device with the specified ID.
   */
  getDeviceById(id: number): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/api/device/${id}`);
  }
}

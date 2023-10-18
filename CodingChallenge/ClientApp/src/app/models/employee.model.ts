import { Device } from "./device.model";
import { PageRequest } from "./page-requet.model";

/**
 * Represents an employee in the application.
 */
export interface Employee extends PageRequest {
  id: number; // Unique identifier for the employee
  name: string; // The name of the employee
  email: string; // The email address of the employee
  deviceList?: Device[]; // List of devices associated with the employee (optional)
}

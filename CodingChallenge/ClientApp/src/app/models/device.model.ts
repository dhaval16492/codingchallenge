import { PageRequest } from "./page-requet.model";

/**
 * Interface representing a Device with pagination details.
 */
export interface Device extends PageRequest {
  id: number;         // Unique identifier for the device
  type: string;       // Type of the device
  description: string;  // Description of the device
}

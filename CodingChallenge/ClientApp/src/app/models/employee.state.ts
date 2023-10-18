import { Employee } from './employee.model';
import { PaginatedList } from './paginated-list.model';

// Define the state interface
export interface EmployeeState {
  employees: PaginatedList<Employee> | null;
  loading: boolean;
  error: any;
}

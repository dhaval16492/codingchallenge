import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'; // Import your environment settings
import { Employee } from '../models/employee.model';
import { PaginatedList } from '../models/paginated-list.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private apiUrl = `${environment.apiBaseUrl}/employee`; // Set your API URL here

  constructor(private http: HttpClient) { }

  /**
   * Fetch all employees from the API.
   * @param employee - Object containing filtering parameters for employees.
   * @returns An observable of a paginated list of employees.
   */
  getEmployees(employee: Employee): Observable<(PaginatedList<Employee>)> {
    return this.http.get<PaginatedList<Employee>>(this.apiUrl, { params: { name: employee.name, email: employee.email, pageSize: employee.pageSize ?? 0, pageNumber: employee.pageNumber ?? 0 } });
  }

  /**
   * Add a new employee to the API.
   * @param employee - The employee object to be added.
   * @returns An observable of the added employee.
   */
  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee);
  }

  /**
   * Update an existing employee in the API.
   * @param employee - The employee object to be updated.
   * @returns An observable of the updated employee.
   */
  updateEmployee(employee: Employee): Observable<Employee> {
    const url = `${this.apiUrl}`;
    return this.http.put<Employee>(url, employee);
  }

  /**
   * Delete an employee by ID from the API.
   * @param id - The ID of the employee to be deleted.
   * @returns An observable indicating the success of the delete operation.
   */
  deleteEmployee(id: number): Observable<void> {
    console.log(id);
    const url = `${this.apiUrl}?id=${id}`;
    return this.http.delete<void>(url);
  }

  /**
   * Get an employee by ID from the API.
   * @param id - The ID of the employee to be retrieved.
   * @returns An observable of the employee with the specified ID.
   */
  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/api/employee/${id}`);
  }
}

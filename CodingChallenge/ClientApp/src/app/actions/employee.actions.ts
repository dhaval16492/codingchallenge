import { createAction, props } from '@ngrx/store';
import { Employee } from '../models/employee.model';
import { PaginatedList } from '../models/paginated-list.model';

/**
 * Load Employees
 */
export const loadEmployees = createAction('[Employee] Load Employees', props<{ employee: Employee }>());

/**
 * Load employee success
 */
export const loadEmployeesSuccess = createAction('[Employee] Load Employees Success', props<{ employees: PaginatedList<Employee> }>());

/**
 * Load employee failure
 */
export const loadEmployeesFailure = createAction('[Employee] Load Employees Failure', props<{ error: any }>());


/**
 * Add employee
 */
export const addEmployee = createAction(
  '[Employee] Add Employee',
  props<{ employee: Employee }>()
);

/**
 * Add employee sucess
 */
export const addEmployeeSuccess = createAction(
  '[Employee] Add Employee Success',
  props<{ employee: Employee }>()
);

/**
 * Add employee failure
 */
export const addEmployeeFailure = createAction(
  '[Employee] Add Employee Failure',
  props<{ error: any }>()
);

/**
 * Update employee
 */
export const updateEmployee = createAction(
  '[Employee] Update Employee',
  props<{ employee: Employee }>()
);

/**
 * Update employee success
 */
export const updateEmployeeSuccess = createAction(
  '[Employee] Update Employee Success',
  props<{ employee: Employee }>()
);

/**
 * Update employee failure
 */
export const updateEmployeeFailure = createAction(
  '[Employee] Update Employee Failure',
  props<{ error: any }>()
);

/**
 * Delete employee
 */
export const deleteEmployee = createAction(
  '[Employee] Delete Employee',
  props<{ id: number }>()
);

/**
 * Delete employee success
 */
export const deleteEmployeeSuccess = createAction(
  '[Employee] Delete Employee Success',
  props<{ id: number }>()
);

/**
 * Delete employee failure
 */
export const deleteEmployeeFailure = createAction(
  '[Employee] Delete Employee Failure',
  props<{ error: any }>()
);

/**
 * Get employee by id
 */
export const getEmployeeById = createAction(
  '[Employee] Get Employee by ID',
  props<{ id: number }>()
);

/**
 * Get employee by Id success
 */
export const getEmployeeByIdSuccess = createAction(
  '[Employee] Get Employee Success',
  props<{ employee: Employee }>()
);

/**
 * Get employee by id failure
 */
export const getEmployeeByIdFailure = createAction(
  '[Employee] Get Employee Failure',
  props<{ error: any }>()
);

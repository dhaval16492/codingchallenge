import { createReducer, on } from '@ngrx/store';
import * as EmployeeActions from '../actions/employee.actions';
import { EmployeeState } from '../models/employee.state';

export const initialState: EmployeeState = {
  employees: null,
  loading: false,
  error: null,
};

// Create a reducer for the employee entity
export const employeeReducer = createReducer(
  initialState,

  // Load Employees
  on(EmployeeActions.loadEmployees, (state) => ({ ...state, loading: true })),
  on(EmployeeActions.loadEmployeesSuccess, (state, { employees }) => ({ ...state, employees, loading: false })),
  on(EmployeeActions.loadEmployeesFailure, (state, { error }) => ({ ...state, error, loading: false })),

  // Add Employee

  on(EmployeeActions.addEmployee, (state) => ({ ...state, loading: true })),
  on(EmployeeActions.addEmployeeSuccess, (state, { employee }) => ({
    ...state,
    employees: state.employees ? {
      ...state.employees,
      paginatedList: [...state.employees.paginatedList, employee],
      totalCount: state.employees.totalCount + 1
    } : null,
    loading: false
  })),
  on(EmployeeActions.addEmployeeFailure, (state, { error }) => ({
    ...state,
    loading: false,
    error,
  })),

  // Update Employee
  on(EmployeeActions.updateEmployee, (state) => ({ ...state, loading: true })),
  on(EmployeeActions.updateEmployeeSuccess, (state, { employee }) => ({
    ...state,
    employees: state.employees
      ? {
        ...state.employees,
        paginatedList: state.employees.paginatedList.map((employeeDetail) =>
          employeeDetail.id === employee.id ? employee : employeeDetail
        ),
      }
      : null,
    loading: false
  })),
  on(EmployeeActions.updateEmployeeFailure, (state, { error }) => ({
    ...state,
    error,
  })),

  // Delete Employee
  on(EmployeeActions.deleteEmployee, (state) => ({ ...state, loading: true })),
  on(EmployeeActions.deleteEmployeeSuccess, (state, { id }) => ({
    ...state,
    employees: state.employees
      ? {
        ...state.employees,
        paginatedList: state.employees.paginatedList.filter(
          (employee) => employee.id !== id
        ),
        totalCount:state.employees.totalCount-1,
      } : null,
    loading: false
  })),
  on(EmployeeActions.deleteEmployeeFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  })),
  on(EmployeeActions.getEmployeeByIdSuccess, (state, { employee }) => ({
    ...state,
    // You can update an existing employee or add it to the list as needed
    employee,
  }))
);


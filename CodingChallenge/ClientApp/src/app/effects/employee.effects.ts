import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, mergeMap } from 'rxjs/operators';

import { Employee } from '../models/employee.model';
import { EmployeeService } from '../service/employee.service';
import * as EmployeeActions from '../actions/employee.actions';
import { ToasterService } from '../service/toaster.service';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class EmployeeEffects {
  constructor(
    private actions$: Actions,
    private employeeService: EmployeeService,
    private toasterService: ToasterService
  ) { }

  /**
   * Effect for loading employees
   */
  loadEmployees$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EmployeeActions.loadEmployees),
      mergeMap((action) =>
        this.employeeService.getEmployees(action.employee).pipe(
          map((employees) => EmployeeActions.loadEmployeesSuccess({ employees })),
          catchError((error) => of(EmployeeActions.loadEmployeesFailure({ error })))
        )
      )
    )
  );

  /**
   * Effect for adding an employee
   */
  addEmployee$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EmployeeActions.addEmployee),
      mergeMap((action) =>
        this.employeeService.addEmployee(action.employee).pipe(
          map((employee: Employee) => {
            this.toasterService.showSuccess('Employee added successfully');
            return EmployeeActions.addEmployeeSuccess({ employee });
          }),
          catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              // Handle the CustomException error
              this.toasterService.showError(error.error.error);
              return of(EmployeeActions.addEmployeeFailure({ error: error.error }));
            } else {
              // Handle other errors
              this.toasterService.showError('An error occurred');
              // Dispatch an action for a generic error or other handling
              return of(EmployeeActions.addEmployeeFailure({ error }));
            }
          })
        )
      )
    )
  );

  /**
   * Effect for updating an employee
   */
  updateEmployee$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EmployeeActions.updateEmployee),
      mergeMap((action) =>
        this.employeeService.updateEmployee(action.employee).pipe(
          map((employee: Employee) => {
            this.toasterService.showSuccess('Employee updated successfully');
            return EmployeeActions.updateEmployeeSuccess({ employee });
          }),
          catchError((error) => {
            if (error instanceof HttpErrorResponse && error.status === 400) {
              // Handle the CustomException error
              this.toasterService.showError(error.error.error);
              return of(EmployeeActions.updateEmployeeFailure({ error: error.error }));
            } else {
              // Handle other errors
              this.toasterService.showError('An error occurred');
              // Dispatch an action for a generic error or other handling
              return of(EmployeeActions.updateEmployeeFailure({ error }));
            }
          })
        )
      )
    )
  );

  /**
   * Effect for deleting an employee
   */
  deleteEmployee$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EmployeeActions.deleteEmployee),
      mergeMap((action) =>
        this.employeeService.deleteEmployee(action.id).pipe(
          map(() => {
            this.toasterService.showSuccess('Employee deleted successfully');
            return EmployeeActions.deleteEmployeeSuccess({ id: action.id });
          }),
          catchError((error) => {
            // Handle other errors
            this.toasterService.showError('An error occurred');
            // Dispatch an action for a generic error or other handling
            return of(EmployeeActions.deleteEmployeeFailure({ error }));
          })
        )
      )
    )
  );

  /**
   * Effect for getting an employee by ID
   */
  getEmployeeById$ = createEffect(() =>
    this.actions$.pipe(
      ofType(EmployeeActions.getEmployeeById),
      mergeMap((action) =>
        this.employeeService.getEmployeeById(action.id).pipe(
          map((employee) =>
            EmployeeActions.getEmployeeByIdSuccess({ employee })
          ),
          catchError((error) =>
            of(EmployeeActions.getEmployeeByIdFailure({ error }))
          )
        )
      )
    )
  );
}

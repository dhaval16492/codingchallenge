using CodingChallenge.Application.Dtos;
using System.Collections.Generic;

namespace CodingChallenge.Application.Interfaces
{
    /// <summary>
    /// Interface defining the contract for employee-related service operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Retrieves a list of employees based on the provided filter criteria.
        /// </summary>
        /// <param name="employeeDto">A data transfer object containing filtering criteria for employees.</param>
        /// <returns>A paginated list of employees in the form of EmployeeDto objects.</returns>
        Pagination<EmployeeDto> GetEmployee(EmployeeDto employeeDto);

        /// <summary>
        /// Retrieves an employee by their unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve.</param>
        /// <returns>An EmployeeDto object representing the employee with the specified ID.</returns>
        EmployeeDto GetEmployeeById(int id);

        /// <summary>
        /// Adds a new employee based on the provided data.
        /// </summary>
        /// <param name="employeeDto">A data transfer object representing the employee to be added.</param>
        /// <returns>An EmployeeDto object representing the newly added employee.</returns>
        EmployeeDto AddEmployee(EmployeeDto employeeDto);

        /// <summary>
        /// Updates an existing employee's details based on the provided data.
        /// </summary>
        /// <param name="employeeDto">A data transfer object containing updated employee details.</param>
        /// <returns>An EmployeeDto object representing the updated employee.</returns>
        EmployeeDto UpdateEmployee(EmployeeDto employeeDto);

        /// <summary>
        /// Deletes an employee with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to be deleted.</param>
        void DeleteEmployee(int id);
    }
}

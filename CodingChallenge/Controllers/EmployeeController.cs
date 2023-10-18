using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        #region Member Variables
        private readonly IEmployeeService _employeeService;
        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service for performing employee-related operations.</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Retrieves a list of employees based on the provided query parameters.
        /// </summary>
        /// <param name="employeeDto">The query parameters for filtering employees.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the list of employees.</returns>
        [HttpGet]
        public IActionResult GetEmployees([FromQuery] EmployeeDto employeeDto)
        {
            var result = _employeeService.GetEmployee(employeeDto);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an employee by their unique ID.
        /// </summary>
        /// <param name="id">The unique ID of the employee to be retrieved.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the employee details.</returns>
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var result = _employeeService.GetEmployeeById(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new employee and returns it as a successful response.
        /// </summary>
        /// <param name="employeeDto">The employee data for creating a new employee.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the newly created employee.</returns>
        [HttpPost]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            try
            {
                return Ok(_employeeService.AddEmployee(employeeDto));
            }
            catch (CustomException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates employee details and returns the updated employee as a successful response.
        /// </summary>
        /// <param name="employeeDto">The employee data for updating an existing employee.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the updated employee.</returns>
        [HttpPut]
        public IActionResult UpdateEmployeeDetails(EmployeeDto employeeDto)
        {
            try
            {
                return Ok(_employeeService.UpdateEmployee(employeeDto));
            }
            catch (CustomException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes employee details. Returns a successful response upon successful deletion.
        /// </summary>
        /// <param name="id">The ID of the employee to be deleted.</param>
        /// <returns>Returns an <see cref="IActionResult"/> indicating the status of the deletion operation.</returns>
        [HttpDelete]
        public IActionResult DeleteEmployeeDetails(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok();
        }
        #endregion Public Methods
    }
}

using CodingChallenge.Application.Dtos;
using CodingChallenge.Application.Interfaces;
using CodingChallenge.Common.CustomExceptions;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.Controllers
{
    /// <summary>
    /// Controller for managing device-related operations.
    /// </summary>
    [ApiController]
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        #region Member Variables
        private readonly IDeviceService _deviceService;
        #endregion Member Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceController"/> class.
        /// </summary>
        /// <param name="deviceService">The device service for performing device-related operations.</param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        #endregion Constructor

        #region Public Methods
        /// <summary>
        /// Retrieves a list of devices based on the provided query parameters.
        /// </summary>
        /// <param name="deviceDto">The query parameters for filtering devices.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the list of devices.</returns>
        [HttpGet]
        public IActionResult GetDevices([FromQuery] DeviceDto deviceDto)
        {
            var result = _deviceService.GetDevice(deviceDto);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new device and returns it as a successful response.
        /// </summary>
        /// <param name="deviceDto">The device data for creating a new device.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the newly created device.</returns>
        [HttpPost]
        public IActionResult Create(DeviceDto deviceDto)
        {
            try
            {
                return Ok(_deviceService.AddDevice(deviceDto));
            }
            catch (CustomException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Updates device details and returns the updated device as a successful response.
        /// </summary>
        /// <param name="deviceDto">The device data for updating an existing device.</param>
        /// <returns>Returns an <see cref="IActionResult"/> containing the updated device.</returns>
        [HttpPut]
        public IActionResult UpdateDeviceDetails(DeviceDto deviceDto)
        {
            try
            {
                return Ok(_deviceService.UpdateDevice(deviceDto));
            }
            catch (CustomException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes device details. Returns a successful response upon successful deletion.
        /// </summary>
        /// <param name="id">The ID of the device to be deleted.</param>
        /// <returns>Returns an <see cref="IActionResult"/> indicating the status of the deletion operation.</returns>
        [HttpDelete]
        public IActionResult DeleteDeviceDetails(int id)
        {
            try
            {
                _deviceService.DeleteDevice(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }
        #endregion Public Methods

    }
}

using CodingChallenge.Application.Dtos;

namespace CodingChallenge.Application.Interfaces
{
    /// <summary>
    /// Interface defining the contract for device-related service operations.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Retrieves a paginated list of devices based on provided criteria.
        /// </summary>
        /// <param name="deviceDto">Data transfer object containing search criteria and pagination settings.</param>
        /// <returns>A paginated list of devices in the form of DeviceDto objects.</returns>
        Pagination<DeviceDto> GetDevice(DeviceDto deviceDto);

        /// <summary>
        /// Adds a new device based on the provided data.
        /// </summary>
        /// <param name="deviceDto">Data transfer object containing device information to be added.</param>
        /// <returns>The created device in the form of a DeviceDto object.</returns>
        DeviceDto AddDevice(DeviceDto deviceDto);

        /// <summary>
        /// Updates an existing device based on the provided data.
        /// </summary>
        /// <param name="deviceDto">Data transfer object containing updated device information.</param>
        /// <returns>The updated device in the form of a DeviceDto object.</returns>
        DeviceDto UpdateDevice(DeviceDto deviceDto);

        /// <summary>
        /// Deletes a device with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the device to be deleted.</param>
        void DeleteDevice(int id);
    }
}

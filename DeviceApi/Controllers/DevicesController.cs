using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeviceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DeviceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DevicesContext _db;

        public DevicesController(DevicesContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Add device - POST api/devices/add
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<Device>> Add([FromBody]Device device)
        {
            if (device == null || string.IsNullOrEmpty(device.Name) || string.IsNullOrEmpty(device.Brand))
            {
                return BadRequest();
            }
            
            if (device.Created == DateTime.MinValue)
                device.Created = DateTime.Today;

            _db.Devices.Add(device);

            await _db.SaveChangesAsync();

            return Ok(device);
        }

        /// <summary>
        /// Get device by identifier - GET api/devices/getbyid/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<Device>> GetById(int id)
        {
            Device device = await _db.Devices.FirstOrDefaultAsync(x => x.Id == id);

            if (device == null)
                return NotFound();

            return new ObjectResult(device);
        }

        /// <summary>
        /// List all devices - GET api/devices/getall
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<Device>>> GetAll()
        {
            return await _db.Devices.ToListAsync();
        }
        
        /// <summary>
        /// Update device (full and partial) - PUT api/devices/update
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<Device>> Update([FromBody]Device device)
        {
            if (device == null)
            {
                return BadRequest();
            }

            if (!IsDeviceExist(device.Id))
            {
                return NotFound();
            }

            var storedDevice = _db.Devices.First(x => x.Id == device.Id);
            storedDevice.Name = string.IsNullOrEmpty(device.Name) ? storedDevice.Name : device.Name;
            storedDevice.Brand = string.IsNullOrEmpty(device.Brand) ? storedDevice.Brand : device.Brand;
            storedDevice.Created = device.Created == DateTime.MinValue ? storedDevice.Created : device.Created;

            _db.Update(storedDevice);

            await _db.SaveChangesAsync();

            return Ok(storedDevice);
        }

        /// <summary>
        /// Delete a device - DELETE api/devices/delete/{id}
        /// </summary>
        /// <param name="id">Identifier of a device</param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Device>> Delete(int id)
        {
            var device = await _db.Devices.FirstOrDefaultAsync(x => x.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            _db.Devices.Remove(device);

            await _db.SaveChangesAsync();

            return Ok(device);
        }

        /// <summary>
        /// Search device by brand - GET api/devices/search/{brand}
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [HttpGet("search/{brand}")]
        public async Task<ActionResult<IEnumerable<Device>>> Search(string brand)
        {
            var devices = await _db.Devices.Where(x => x.Brand == brand).ToListAsync();

            if (devices == null || devices.Count == 0)
                return NotFound();

            return new ObjectResult(devices);
        }

        private bool IsDeviceExist(int id)
        {
            return _db.Devices.Any(e => e.Id == id);
        }
    }
}

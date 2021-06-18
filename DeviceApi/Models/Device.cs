using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceApi.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public DateTime Created { get; set; }
    }
}

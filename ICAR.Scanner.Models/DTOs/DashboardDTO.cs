using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAR.Scanner.Models.DTOs
{
    public class DashboardDTO
    {
        public int UserCount { get; set; }
        public int SensorCount { get; set; }
        public int TreeCount { get; set; }

        public List<UserDTO> Users { get; set; } = new();
        public List<SensorDTO> Sensors { get; set; } = new();
        public List<TreesDto> Trees { get; set; } = new();
    }
}

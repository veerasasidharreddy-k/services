using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightHouseMRO.AdminService.Domain.Entities
{
    public class TimeStampedEntity
    {
        public int Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class TimeADay
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string TimeInDay { get; set; }
    }
}

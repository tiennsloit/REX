using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public class TimeADayService: ITimeADayService
    {
        public ICollection<TimeADay> GetTimesADay()
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.TimeADays.ToList();
            }
        }
    }
}

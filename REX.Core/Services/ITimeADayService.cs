using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface ITimeADayService
    {
        ICollection<TimeADay> GetTimesADay();
    }
}
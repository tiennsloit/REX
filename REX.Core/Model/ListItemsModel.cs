using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Model
{
    public class ListItemsModel
    {
        public ICollection<District> Districts { get; set; }
        public ICollection<TimeADay> TimesInDay { get; set; }
        public ICollection<RiceType> RiceTypes { get; set; }
    }
}

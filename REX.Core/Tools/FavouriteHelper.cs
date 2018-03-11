using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace REX.Core.Tools
{
    public static class FavouriteHelper
    {
        public static bool IsEqual(this Favourite favourite1, Favourite favourite2)
        {
            return favourite1.ContactId == favourite2.ContactId
                && favourite1.Price1 == favourite2.Price1
                && favourite1.Price2 == favourite2.Price2
                && favourite1.ProductTypeId == favourite2.ProductTypeId
                && favourite1.Weight == favourite2.Weight;
        }
    }
}

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
            var jsonFav1 = new JavaScriptSerializer().Serialize(favourite1);
            var jsonFav2 = new JavaScriptSerializer().Serialize(favourite2);
            return jsonFav1 == jsonFav2;
        }
    }
}

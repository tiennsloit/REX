using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using REX.Core.Tools;
namespace REX.Core.Services
{
    public class FavouriteService : IFavouriteService
    {
        /// <summary>
        /// Calculate to make the latest list of favourite to update to database after receive the order from clent.
        /// </summary>
        /// <param name="latestFavourite">The latest favourite.</param>
        /// <param name="allFavourites">All favourites.</param>
        /// <returns></returns>
        public ICollection<Favourite> MergeFavourites(Favourite latestFavourite, ICollection<Favourite> allFavourites)
        {
            var res = allFavourites;
            if (res == null || !res.Any())
            {
                latestFavourite.IsCurrently = true;
                return new List<Favourite> { latestFavourite };
            }

            if (allFavourites.Where(x => x.IsEqual(latestFavourite)).Count() == 0)
            {
                latestFavourite.IsCurrently = true;
                foreach (var fav in allFavourites)
                {
                    fav.IsCurrently = false;
                }
                res.Add(latestFavourite);
            }
            else
            {
                foreach (var fav in allFavourites)
                {
                    if (fav.IsEqual(latestFavourite))
                    {
                        fav.IsCurrently = true;
                    }
                    else
                    {
                        fav.IsCurrently = false;
                    }
                }
            }

            return res;
        }
    }
}

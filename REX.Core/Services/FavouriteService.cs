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

            latestFavourite.IsCurrently = true;
            if (res == null || !res.Any())
            {
                return new List<Favourite> { latestFavourite };
            }

            if (allFavourites.Where(x => x.IsEqual(latestFavourite)).Count() == 0)
            {
                foreach (var fav in allFavourites)
                {
                    fav.IsCurrently = false;
                   
                }
                latestFavourite.Id = 0; //make sure this item's id be 0 so that the dbcontext will add a new item
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

        public ICollection<Favourite> GetFavourites(int contactId)
        {
            using (var dbContext = new RexDbContext())
            {
                return dbContext.Favourites.Where(x => x.ContactId == contactId).ToList();
            }
        }
    }
}

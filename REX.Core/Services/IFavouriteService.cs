using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IFavouriteService
    {
        ICollection<Favourite> MergeFavourites(Favourite latestFavourite, ICollection<Favourite> allFavourites);
        ICollection<Favourite> GetFavourites(int contactId);
        Favourite GetCurrentFavourite(int contactId);
    }
}
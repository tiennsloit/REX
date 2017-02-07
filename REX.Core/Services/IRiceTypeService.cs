using REX.Data;
using System.Collections.Generic;

namespace REX.Core.Services
{
    public interface IRiceTypeService
    {
        ICollection<RiceType> GetRiceTypes();
    }
}
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public interface IDistrictService
    {
        void InsertDistrict(string name);
        ICollection<District> GetDistricts();
    }
}

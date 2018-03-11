using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class Favourite
    {
        public int Id { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        [JsonIgnore]
        public Contact Contact { get; set; }
        public ProductType ProductType { get; set; }
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public bool IsCurrently { get; set; }
        public decimal Weight { get; set; }

    }
}

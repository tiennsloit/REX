using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class Contact
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string FaceBookName { get; set; }
        [MaxLength(100)]
        public string Phone1 { get; set; }
        [MaxLength(100)]
        public string Phone2 { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public District District { get; set; }

        /// <summary>
        /// Changes
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual ICollection<Favourite> Favourites { get; set; }
        public TimeADay TimeCanReceived { get; set; }
        [ForeignKey("TimeCanReceived")]
        public int TimeCanReceivedId { get; set; }
        public int HowManyDaysOfConsume { get; set; }
        public int HowManyWeightOfConsume { get; set; }
        public DateTime NextShipDate { get; set; }
        [MaxLength(50)]
        public string Satisfied { get; set; }
        [MaxLength(50)]
        public string Unsatisfied { get; set; }
        [MaxLength(1000)]
        public string ReasonNotSatisfied { get; set; }
        
    }
}

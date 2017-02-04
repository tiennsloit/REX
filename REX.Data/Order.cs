using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public decimal Weight { get; set; }
        public RiceType RiceType { get; set; }
        [ForeignKey("RiceType")]
        public int RiceType1Id { get; set; }
        public decimal Price { get; set; }
        public decimal Surcharge { get; set; }
        public decimal AmountToReceived { get; set; }
        public decimal CoverPrice { get; set; }
        public decimal PromoPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShipFee { get; set; }
        public decimal OtherFee { get; set; }
        public decimal Profit { get; set; }
        public decimal Paid { get; set; }
        public decimal Received { get; set; }
        public DateTime DateShipped { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}

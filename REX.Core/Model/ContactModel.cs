using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Model
{
    public class ContactModel
    {
        public double Id { get; set; }
        public string Name { get; set; }
        public string FaceBookName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string RiceType1 { get; set; }
        public string RiceType2 { get; set; }
        public decimal Weight { get; set; }
        public decimal Price1 { get; set; }
        public decimal Surcharge { get; set; }
        public decimal AmountToReceived { get; set; }
        public DateTime DateShipped { get; set; }
        public decimal TimeCanReceived { get; set; }
        public int HowManyDaysOfConsume { get; set; }
        public int HowManyWeightOfConsume { get; set; }
        public DateTime NextShipDate { get; set; }
        public string Satisfied { get; set; }
        public string Unsatisfied { get; set; }
        public string ReasonNotSatisfied { get; set; }
        public decimal Price2 { get; set; }
        public decimal CoverPrice { get; set; }
        public decimal PromoPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShipFee { get; set; }
        public decimal OtherFee { get; set; }
        public decimal Profit { get; set; }
        public decimal Paid { get; set; }
        public decimal Received { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Data
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
    }
}

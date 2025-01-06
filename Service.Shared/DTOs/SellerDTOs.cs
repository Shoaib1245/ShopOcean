using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.DTOs
{
    public class SellerDTOs
    {
        //public int Id { get; set; }

        //public string Name { get; set; }

        //public string Cnic { get; set; }
        //public string Contact { get; set; }
        //public string DOB { get; set; }
        //public string Address { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        //public string? Image { get; set; }
        //public int? Status { get; set; }

        //public bool IsDeleted { get; set; }
        //public int ZipCode { get; set; }
    }
}

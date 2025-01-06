using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Shared.DTOs
{
    public class JsondataDTOs
    {
        public IFormFile? file { get; set; }
        public string? jsondata { get; set; }
    }
}

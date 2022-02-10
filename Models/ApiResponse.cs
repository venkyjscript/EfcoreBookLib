using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.Models
{
    public class ApiResponse
    {
        //public string code { get; set; }
        public string message { get; set; }
        public object Data { get; set; }
    }
}

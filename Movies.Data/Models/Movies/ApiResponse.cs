using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Framework.Models.Movies
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = "success";
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }

}

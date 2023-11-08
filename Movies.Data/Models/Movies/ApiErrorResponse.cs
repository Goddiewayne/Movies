using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Framework.Models.Movies
{
    public class ApiErrorResponse
    {
        public string Status { get; set; } = "error";
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }

}

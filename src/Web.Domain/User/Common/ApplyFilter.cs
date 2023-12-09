using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Web.Domain.User.Common
{
    public class ApplyFilter
    {
        public string? username { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
    }
}

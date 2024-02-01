using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class User
    {
        [Key]

        public int id { get; set; }
        public string? login { get; set; }
        public string? password { get; set; }
        public string? userName { get; set; } = null!;
        public string? passport { get; set; } = null!;
        public bool? blFirst { get; set; }
        public bool? blLast { get; set;}
    }
}

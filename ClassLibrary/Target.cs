using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Target
    {
        [Key]
        public int idTarget {  get; set; }
        public string info { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Employees
    {
        [Key]
        public int id_employee { get; set; }
        public string firstName { get; set; }
        public string name {  get; set; }
        public string lastName { get; set; }
        public string division { get; set; }
        public string department { get; set; }

    }
}

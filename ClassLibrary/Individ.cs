using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Individ
    {
        [Key]
        public int id { get; set; }
        public DateTime beginDate { get; set; }
        public DateTime endDate { get; set; }
        public string firstName { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string organization { get; set; }
        public string note { get; set; }
        public DateTime birthday { get; set; }
        public string passport { get; set; }
        public byte[]? pdf { get; set; }
        public byte[]? photo { get; set; }
        public string target { get; set; }
        public string division { get; set; }
        public string employee { get; set; }
        public string? status { get; set; }
        public string? comment { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.Model
{
    public class Rakjegyzek
    {
        public int Id { get; set; }
        public string? Rakjegy { get; set; }
        public bool Lezarva { get; set; }
        public bool Feladva { get; set; }
        public bool Felrakva { get; set; }
    }
}

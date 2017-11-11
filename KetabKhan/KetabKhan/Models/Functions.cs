using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhan.Models
{
    public class Functions
    {
        public int arabictonum(string arabicnumstr)
        {
            int num = 0, c = 0;
            for (var i = 0; i < arabicnumstr.Count(); i++)
            {
                c = arabicnumstr[i];
                num += c - 1776;
                num *= 10;
            }
            return num / 10;
        }
        public string[] choice(string c)
        {
            string[] s = c.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                      .Where(x => x.Length != 1 || x[0] != '\0').ToArray();
            return s;
        }
    }
}

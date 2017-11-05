using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KetabKhan.Models
{
    public class Person
    {
        public string State;
        public long ChatID;
        public string Text;
        public int Qnum = 0;
        public int Cnum = 0;
        public int cntQ = 0;
        public int cntC = 0;
        public long ExamID;
        public long NowQuestionID;
        public List<long> ListOfQuestion = new List<long>();
    }
}

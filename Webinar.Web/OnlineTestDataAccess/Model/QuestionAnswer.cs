using OnlineTestDataAssess.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.Model
{
    public class QuestionAnswer:Question
    {
        public string CategoryName { get; set; }
        public List<Answer> answerList { get; set; }

        public int AnswerId { get; set; }
    }
}

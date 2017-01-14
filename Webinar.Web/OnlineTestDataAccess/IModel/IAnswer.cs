using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
   public interface IAnswer
    {
        long AnswerId { get; set; }
        Nullable<long> QuestionId { get; set; }
        string Answer1 { get; set; }

        bool IsActive { get; set; }
    }
}

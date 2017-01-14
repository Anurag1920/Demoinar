using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
   public interface IResult
    {
        long ResultId { get; set; }
        string UserId { get; set; }
        Nullable<long> QuestionId { get; set; }
        Nullable<long> AnswerId { get; set; }
        Nullable<int> CategoryId { get; set; }
        Nullable<long> TestId { get; set; }
        Nullable<bool> IsCorrect { get; set; }
    }
}

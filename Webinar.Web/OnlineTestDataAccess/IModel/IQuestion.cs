using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
    public interface IQuestion
    {
        long QuestionId { get; set; }
        string Question1 { get; set; }
        Nullable<int> CategoryId { get; set; }
        Nullable<bool> IsActive { get; set; }
        Nullable<long> CorrectAnswerId { get; set; }
        string CorrectAnswer { get; set; }
    }
}

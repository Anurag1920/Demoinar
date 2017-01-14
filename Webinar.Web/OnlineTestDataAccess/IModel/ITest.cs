using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
    public interface ITest
    {
        long TestId { get; set; }
        string UserId { get; set; }
        bool IsActive { get; set; }
        Nullable<int> TotalQuestion { get; set; }
        Nullable<int> Attempted { get; set; }
        Nullable<int> Correct { get; set; }
    }
}

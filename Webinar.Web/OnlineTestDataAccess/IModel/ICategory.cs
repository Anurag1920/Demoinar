using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess.IModel
{
    public interface ICategory
    {
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        bool IsActive { get; set; }


    }
}

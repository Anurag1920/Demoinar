using OnlineTestBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess
{
    public class ResultManager
    {
        public ReturnedResult<List<Test>> GetAllResult()
        {
            ReturnedResult<List<Test>> test = new ReturnedResult<List<Test>>();
            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {
                test.Data = dbContext.Tests.Where(x => x.IsActive == true).ToList();
            }

            return test;

        }
    }
}

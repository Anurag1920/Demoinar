using OnlineTestBll;
using OnlineTestDataAssess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess
{
    public class TestManager
    {
        public ReturnedResult<List<QuestionAnswer>> GetTest()
        {
            ReturnedResult<List<QuestionAnswer>> test = new ReturnedResult<List<QuestionAnswer>>();
            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {
                test.Data = dbContext.Questions.Where(x => x.IsActive == true).Select(x => new QuestionAnswer() { QuestionId = x.QuestionId, Question1 = x.Question1, CategoryId = x.CategoryId }).ToList();

                var allCategory = dbContext.Categories.ToList();
                foreach (var questionAnswer in test.Data)
                {
                    questionAnswer.answerList = dbContext.Answers.Where(x => x.QuestionId == questionAnswer.QuestionId && x.IsActive == true).ToList();
                    questionAnswer.CategoryName = allCategory.FirstOrDefault(x => x.CategoryId == questionAnswer.CategoryId).CategoryName;
                }
            }

            return test;

        }

        public ReturnedResult<List<QuestionAnswer>> SubmitTest(List<Result> aResults)
        {
            Test test = new Test();
            test.IsActive = true;
            test.UserId = aResults.FirstOrDefault().UserId;
            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {


                dbContext.Tests.Add(test);

                dbContext.SaveChanges();
                foreach (var result in aResults)
                {
                    result.TestId = test.TestId;
                    var question = dbContext.Questions.SingleOrDefault(x => x.QuestionId == result.QuestionId && x.CorrectAnswerId != null && x.CorrectAnswerId == result.AnswerId);
                    result.IsCorrect = question != null;
                }
                dbContext.Results.AddRange(aResults);
                dbContext.SaveChanges();

                test.TotalQuestion = aResults.Count;
                test.Attempted = aResults.Count(x => x.AnswerId != 0);
                test.Correct = aResults.Count(x => x.IsCorrect == true);
                dbContext.SaveChanges();
            }
            var totalQuestion = test.TotalQuestion;
            var attempted = test.Attempted;
            var correct = test.Correct;

            var testResult = GetTest();
            testResult.Message = string.Format("Total Question:{0}, Attempted:{1}, Correct:{2}", totalQuestion, attempted, correct);
            return testResult;
        }
    }
}

using OnlineTestBll;
using OnlineTestDataAssess.IModel;
using OnlineTestDataAssess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestDataAssess
{
   public class QuestionAnswerManager
    {
        private const string mError = "Error";
        private const string mSuccess = "Success";
        private const byte mSuccessfull = 1;
        private const byte mUnsuccessfull = 0;

        public ReturnedResult<List<IQuestion>> GetAllQuestion()
        {
            ReturnedResult<List<IQuestion>> resultManager = new ReturnedResult<List<IQuestion>>();
            resultManager.Message = mError;
            resultManager.Result = mUnsuccessfull;
            List<IQuestion> questionList = new List<IQuestion>();
            try
            {
                using (OnlineTestEntities dbContext = new OnlineTestEntities())
                {
                    questionList = dbContext.Questions.ToList<IQuestion>();
                    resultManager.Message = mSuccess;
                    resultManager.Result = mSuccessfull;
                }
            }
            catch
            {

                //ignore
            }

            resultManager.Data = questionList;

            return resultManager;
        }
        public ReturnedResult<List<QuestionAnswer>> AddQuestionAnswer(int aCategoryId, string aQuestion, List<string> aOptions, string aCorrectOption)
        {
            Question question = new Question();
            question.CategoryId = aCategoryId;
            question.Question1 = aQuestion;
            question.IsActive = true;
            question.CorrectAnswer = aCorrectOption;

            try
            {
                using (OnlineTestEntities dbContext = new OnlineTestEntities())
                {
                    dbContext.Questions.Add(question);

                    dbContext.SaveChanges();
                    List<Answer> optionList = new List<Answer>();
                    foreach (var option in aOptions)
                    {
                        Answer answer = new Answer();
                        answer.Answer1 = option;
                        answer.QuestionId = question.QuestionId;
                        answer.IsActive = true;
                        optionList.Add(answer);
                    }
                    dbContext.Answers.AddRange(optionList);
                    dbContext.SaveChanges();
                    question.CorrectAnswerId = optionList.FirstOrDefault(x=>x.Answer1==aCorrectOption).AnswerId;
                    dbContext.SaveChanges();
                }
            }
            catch
            {

                //ignore
            }

            return GetAllQuestionAnswer();
        }
        public ReturnedResult<List<QuestionAnswer>> UpdateQuestionAnswer(QuestionAnswer aQuestionAnswer)
        {

            try
            {
                using (OnlineTestEntities dbContext = new OnlineTestEntities())
                {
                    var question = dbContext.Questions.FirstOrDefault(x => x.QuestionId == aQuestionAnswer.QuestionId);
                    question.Question1 = aQuestionAnswer.Question1;
                    question.CategoryId = aQuestionAnswer.CategoryId;
                    question.CorrectAnswer = aQuestionAnswer.CorrectAnswer;
                    var allOptions = dbContext.Answers.Where(x => x.QuestionId == aQuestionAnswer.QuestionId).ToList();

                    var optionToUpdate = allOptions.Intersect<Answer>(aQuestionAnswer.answerList, new KeyEqualityComparer<Answer>(s => s.AnswerId));
                    var optionToDelete = allOptions.Except<Answer>(aQuestionAnswer.answerList, new KeyEqualityComparer<Answer>(s => s.AnswerId));
                    var optionToAdd = aQuestionAnswer.answerList.Where(x => x.AnswerId == 0);
                    optionToAdd.ToLookup(x => x.IsActive = true);

                    foreach (var answer in aQuestionAnswer.answerList.Where(x => x.AnswerId != 0))
                    {
                        optionToUpdate.FirstOrDefault(x => x.AnswerId == answer.AnswerId).Answer1 = answer.Answer1;

                    }
                    optionToDelete.ToLookup(x => x.IsActive = false);
                    //dbContext.Answers.RemoveRange(optionToDelete);
                    dbContext.Answers.AddRange(optionToAdd);
                    dbContext.SaveChanges();
                    question.CorrectAnswerId = dbContext.Answers.FirstOrDefault(x =>x.QuestionId==aQuestionAnswer.QuestionId && x.Answer1.Equals(aQuestionAnswer.CorrectAnswer)).AnswerId;
                    dbContext.SaveChanges();
                }
            }
            catch
            {

                //ignore
            }

            return GetAllQuestionAnswer();
        }
        public ReturnedResult<List<QuestionAnswer>> DeleteQuestionAnswer(QuestionAnswer aQuestionAnswer)
        {

            try
            {
                using (OnlineTestEntities dbContext = new OnlineTestEntities())
                {
                    var question = dbContext.Questions.FirstOrDefault(x => x.QuestionId == aQuestionAnswer.QuestionId);

                    var allOptions = dbContext.Answers.Where(x => x.QuestionId == aQuestionAnswer.QuestionId).ToList();

                    allOptions.ToLookup(x => x.IsActive = false);
                    question.IsActive = false;
                    //dbContext.Answers.RemoveRange(allOptions);
                  //  dbContext.Questions.Remove(question);
                    dbContext.SaveChanges();

                }
            }
            catch
            {

                //ignore
            }

            return GetAllQuestionAnswer();
        }
        public ReturnedResult<List<QuestionAnswer>> GetAllQuestionAnswer()
        {
            ReturnedResult<List<QuestionAnswer>> allQuestionAnswer = new ReturnedResult<List<QuestionAnswer>>();

            List<QuestionAnswer> questionAnswerList = new List<QuestionAnswer>();
            using (var dbContext = new OnlineTestEntities())
            {
                questionAnswerList = dbContext.Questions.Where(x=>x.IsActive==true).Select(x => new QuestionAnswer { CategoryId = x.CategoryId, Question1 = x.Question1, IsActive = x.IsActive, QuestionId = x.QuestionId ,CorrectAnswerId=x.CorrectAnswerId,CorrectAnswer=x.CorrectAnswer}).OrderByDescending(x=>x.QuestionId).ToList();
                var allCategory = dbContext.Categories.ToList();
                foreach (var questionAnswer in questionAnswerList)
                {
                    questionAnswer.answerList = dbContext.Answers.Where(x => x.QuestionId == questionAnswer.QuestionId && x.IsActive==true).ToList();
                    questionAnswer.CategoryName = allCategory.FirstOrDefault(x => x.CategoryId == questionAnswer.CategoryId).CategoryName;
                }
            }
            allQuestionAnswer.Data = questionAnswerList;

            return allQuestionAnswer;
        }
        public class KeyEqualityComparer<T> : IEqualityComparer<T>
        {
            private readonly Func<T, object> keyExtractor;

            public KeyEqualityComparer(Func<T, object> keyExtractor)
            {
                this.keyExtractor = keyExtractor;
            }

            public bool Equals(T x, T y)
            {
                return this.keyExtractor(x).Equals(this.keyExtractor(y));
            }

            public int GetHashCode(T obj)
            {
                return this.keyExtractor(obj).GetHashCode();
            }
        }
    }
}

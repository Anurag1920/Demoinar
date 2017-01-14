using OnlineTestBll;
using OnlineTestDataAssess;
using OnlineTestDataAssess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webinar.Web.Models;
using Microsoft.AspNet.Identity;

namespace Webinar.Web.Controllers
{
    public class OnlineTestController : Controller
    {
        private const string mAdmin = "admin";
        private const string mStudent = "student";
        [Authorize(Roles = "admin,student")]
        public ActionResult Index()
        {
            if (User.IsInRole(mStudent))
            {
                return RedirectToAction("Result");
            }

            return RedirectToAction("DashBoard");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult DashBoard()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                dbContext.Users.Where(s => s.Roles.Any(x => x.Role.Name == "student")).ToList();
            }
            return View();
        }
        [Authorize(Roles = "admin")]
        public JsonResult GetAllUser()
        {
            List<ApplicationUser> appusers = new List<ApplicationUser>();
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                foreach (var user in dbContext.Users.Where(s => s.Roles.Any(x => x.Role.Name == "student")).AsEnumerable<ApplicationUser>())
                {
                    ApplicationUser appUser = new ApplicationUser();
                    appUser.UserName = user.UserName;
                    appUser.Email = user.Email;
                    appusers.Add(appUser);
                }

                // allUser.Data = appusers;

            }
            return Json(appusers, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AllCategory()
        {


            return View();
        }
        [Authorize(Roles = "admin")]
        public JsonResult GetAllCategory()
        {
            CategoryManager categoryManager = new CategoryManager();
            var allCategory = categoryManager.GetAllCategory();

            return Json(allCategory, JsonRequestBehavior.AllowGet);
            //var result = allCategory.Data.ToList<ICategory>();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public JsonResult AddCategory(string aCategoryName)
        {
            CategoryManager categoryManager = new CategoryManager();
            var allCategory = categoryManager.AddCategory(aCategoryName);

            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public JsonResult UpdateCategory(Category aCategory)
        {
            CategoryManager categoryManager = new CategoryManager();
            var allCategory = categoryManager.UpdateCategory(aCategory);

            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public JsonResult DeleteCategory(Category aCategory)
        {
            CategoryManager categoryManager = new CategoryManager();
            var allCategory = categoryManager.DeleteCategory(aCategory);

            return Json(allCategory, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Question()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public JsonResult GetAllQuestion()
        {
            QuestionAnswerManager questionManager = new QuestionAnswerManager();
            var allCategory = questionManager.GetAllQuestion();

            return Json(allCategory, JsonRequestBehavior.AllowGet);
            //var result = allCategory.Data.ToList<ICategory>();
        }
        [Authorize(Roles = "admin")]
        public JsonResult GetAllQuestionAnswer()
        {
            QuestionAnswerManager questionManager = new QuestionAnswerManager();
            var allQuestionAnswer = questionManager.GetAllQuestionAnswer();

            return Json(allQuestionAnswer, JsonRequestBehavior.AllowGet);
            //var result = allCategory.Data.ToList<ICategory>();
        }
        [Authorize(Roles = "admin")]
        public JsonResult AddQuestionAnswer(int aCategoryId, string aQuestion, List<string> aOptions, string aCorrectOption)
        {
            Question question = new Question();
            question.CategoryId = aCategoryId;
            question.Question1 = aQuestion;
            question.IsActive = true;
            QuestionAnswerManager questionManager = new QuestionAnswerManager();

            var allQuestion = questionManager.AddQuestionAnswer(aCategoryId, aQuestion, aOptions, aCorrectOption);

            return Json(allQuestion, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public JsonResult UpdateQuestionAnswer(QuestionAnswer aQuestionAnswer)
        {
            Question question = new Question();
            question.CategoryId = aQuestionAnswer.CategoryId;
            question.Question1 = aQuestionAnswer.Question1;
            question.IsActive = true;
            QuestionAnswerManager questionManager = new QuestionAnswerManager();

            var allQuestionAnswer = questionManager.UpdateQuestionAnswer(aQuestionAnswer);




            return Json(allQuestionAnswer, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public JsonResult DeleteQuestionAnswer(QuestionAnswer aQuestionAnswer)
        {
            Question question = new Question();
            question.CategoryId = aQuestionAnswer.CategoryId;
            question.Question1 = aQuestionAnswer.Question1;
            question.IsActive = true;
            QuestionAnswerManager questionManager = new QuestionAnswerManager();
            var allQuestionAnswer = questionManager.DeleteQuestionAnswer(aQuestionAnswer);
            return Json(allQuestionAnswer, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "student")]
        public ActionResult Test()
        {
            return View();
        }
        [Authorize(Roles = "student")]
        public JsonResult GetTest()
        {
            TestManager questionManager = new TestManager();
            var test = questionManager.GetTest();

            return Json(test, JsonRequestBehavior.AllowGet);
            //var result = allCategory.Data.ToList<ICategory>();
        }

        [Authorize(Roles = "student")]
        public JsonResult SubmitTest(List<Result> aResults)
        {
            var currentUser = User.Identity.GetUserId();
            aResults.ToLookup(x => x.UserId = currentUser);

            TestManager questionManager = new TestManager();
            var test = questionManager.SubmitTest(aResults);



            return Json(test, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "student")]
        public ActionResult Result()
        {
            return View();
        }
        [Authorize(Roles = "student")]
        public JsonResult GetAllResult()
        {
            ResultManager resultManager = new ResultManager();
            var allResult = resultManager.GetAllResult();

            return Json(allResult, JsonRequestBehavior.AllowGet);

        }
    }
}
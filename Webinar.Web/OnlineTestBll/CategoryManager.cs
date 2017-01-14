using OnlineTestDataAssess.IModel;
using OnlineTestDataAssess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTestBll
{
    public class CategoryManager
    {
        private const string mError = "Error";
        private const string mSuccess = "Success";
        private const byte mSuccessfull = 1;
        private const byte mUnsuccessfull = 0;
        public ReturnedResult<List<ICategory>> GetAllCategory()
        {
            ReturnedResult<List<ICategory>> resultManager = new ReturnedResult<List<ICategory>>();
            resultManager.Message = mError;
            resultManager.Result = mUnsuccessfull;
            List<ICategory> categoryList = new List<ICategory>();
            try
            {
                using (OnlineTestEntities dbContext = new OnlineTestEntities())
                {
                    categoryList = dbContext.Categories.ToList<ICategory>();
                    resultManager.Message = mSuccess;
                    resultManager.Result = mSuccessfull;
                }
            }
            catch
            {

                //ignore
            }

            resultManager.Data = categoryList;

            return resultManager;
        }

     
        public ReturnedResult<List<ICategory>> AddCategory(string aCategoryName)
        {
            Category category;

            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {
                category = dbContext.Categories.FirstOrDefault(x => x.CategoryName == aCategoryName);
                if (category == null)
                {
                    category = new Category();
                    category.IsActive = true;
                    category.CategoryName = aCategoryName;
                    dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                }
                return GetAllCategory();
            }
        }

        public ReturnedResult<List<ICategory>> UpdateCategory(ICategory aCategory)
        {
            Category category;

            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {
                category = dbContext.Categories.FirstOrDefault(x => x.CategoryId==aCategory.CategoryId);
                if (category != null)
                {

                    category.IsActive = aCategory.IsActive;
                    category.CategoryName = aCategory.CategoryName;
                    dbContext.SaveChanges();
                }
                return GetAllCategory();
            }
        }
        public ReturnedResult<List<ICategory>> DeleteCategory(ICategory aCategory)
        {
            Category category;

            using (OnlineTestEntities dbContext = new OnlineTestEntities())
            {
                category = dbContext.Categories.FirstOrDefault(x => x.CategoryId == aCategory.CategoryId);
                if (category != null)
                {

                    dbContext.Categories.Remove(category);
                    dbContext.SaveChanges();
                }
                return GetAllCategory();
            }
        } 
        
    }
}

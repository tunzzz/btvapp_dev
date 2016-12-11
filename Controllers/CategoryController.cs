using DAL.LINQ;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTVAPP.Controllers
{
    public class CategoryController : ApiController
    {
        public bool success;
        public CategoryRepository cbDC;
        public CategoryController()
        {
            cbDC = new CategoryRepository();
        }
        ~CategoryController()
        {
            cbDC = null;
        }
        #region Category
        [HttpGet]
        public HttpResponseMessage GetAllCategory()
        {
            try
            {
                List<rtnCategory> list = new List<rtnCategory>();
                IEnumerable<CategoryBranch> cbList = cbDC.GetList();
                if (cbList.Count() > 0)
                {
                    foreach (var item in cbList)
                    {
                        rtnCategory add = new rtnCategory();
                        add.category_id = item.Category_ID;
                        add.name = item.Name;
                        list.Add(add);
                    }
                    success = true;
                }
                else
                {
                    success = false;
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { success, list });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCategoryByID(int id)
        {
            try
            {
                CategoryBranch cb = cbDC.GetList().Where(s => s.Category_ID == id).SingleOrDefault();
                if (cb != null)
                {
                    int category_id = cb.Category_ID;
                    string name = cb.Name;
                    success = true;
                    return Request.CreateResponse(HttpStatusCode.OK, new { success, category_id, name });
                }
                else
                {
                    success = false;
                    return Request.CreateResponse(HttpStatusCode.OK, new { success });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddCategory(CategoryBranch obj)
        {
            try
            {
                CategoryBranch add = new CategoryBranch();
                add.Name = obj.Name;
                bool result = cbDC.Add(add);
                success = result;
                return Request.CreateResponse(HttpStatusCode.OK, success);
            }
            catch (Exception ex)
            {
                success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success, ex.Message });
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateCategory(int id, CategoryBranch value)
        {
            try
            {
                value.Category_ID = id;
                bool result = cbDC.Update(value);
                success = result;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success, ex.Message });
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                bool result = cbDC.Delete(id);
                success = result;
                return Request.CreateResponse(HttpStatusCode.OK, success);
            }
            catch (Exception ex)
            {
                success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success, ex.Message });
            }
        }
        #region CategoryBranchModel
        public class rtnCategory
        {
            public int category_id { get; set; }
            public string name { get; set; }
        }
        #endregion
        #endregion
    }
}

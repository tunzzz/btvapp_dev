using DAL.LINQ;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BTVAPP.Controllers
{
    public class BranchController : ApiController
    {
        public bool success;
        public BranchRepository bDC;
        public BranchController()
        {
            bDC = new BranchRepository();
        }
        ~BranchController()
        {
            bDC = null;
        }
        

        #region Branch
        [HttpGet]
        public HttpResponseMessage GetAllBranch()
        {
            try
            {
                List<rtnBranch> list = new List<rtnBranch>();
                IEnumerable<Branch> bList = bDC.GetList();
                if (bList.Count() > 0)
                {
                    foreach (var item in bList)
                    {
                        rtnBranch add = new rtnBranch();
                        add.branch_id = item.Branch_ID;
                        add.category_id = item.Category_ID.Value;
                        add.name = item.Name;
                        add.tel = item.Tel;
                        add.location = item.Location;
                        add.category_name = item.Category_Name;
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
        public HttpResponseMessage GetBranchByID(int id)
        {
            try
            {
                Branch b = bDC.GetList().Where(s => s.Branch_ID == id).SingleOrDefault();
                if (b != null)
                {
                    int branch_id = b.Branch_ID;
                    string name = b.Name;
                    string tel = b.Tel;
                    string location = b.Location;
                    int category_id = b.Category_ID.Value;
                    string category_name = b.Category_Name;
                    success = true;
                    return Request.CreateResponse(HttpStatusCode.OK, new { success, branch_id, name, tel, location, category_id, category_name });
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
        [HttpGet]
        public HttpResponseMessage GetBranchListByCategoryID(int category_id)
        {
            try
            {
                List<rtnBranch> list = new List<rtnBranch>();
                IEnumerable<Branch> bList = bDC.GetList().Where(s => s.Category_ID == category_id);
                if (bList.Count() > 0)
                {
                    foreach (var item in bList)
                    {
                        rtnBranch add = new rtnBranch();
                        add.branch_id = item.Branch_ID;
                        add.category_id = item.Category_ID.Value;
                        add.name = item.Name;
                        add.tel = item.Tel;
                        add.location = item.Location;
                        add.category_name = item.Category_Name;
                        list.Add(add);
                    }
                    success = true;
                    return Request.CreateResponse(HttpStatusCode.OK, new { success, bList });
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
        [HttpGet]
        public HttpResponseMessage GetBranchListByLocation(string location)
        {
            try
            {
                List<rtnBranch> list = new List<rtnBranch>();
                IEnumerable<Branch> bList = bDC.GetListByLocation(location);
                if (bList.Count() > 0)
                {
                    foreach (var item in bList)
                    {
                        rtnBranch add = new rtnBranch();
                        add.branch_id = item.Branch_ID;
                        add.category_id = item.Category_ID.Value;
                        add.name = item.Name;
                        add.tel = item.Tel;
                        add.location = item.Location;
                        add.category_name = item.Category_Name;
                        list.Add(add);
                    }
                    success = true;
                    return Request.CreateResponse(HttpStatusCode.OK, new { success, bList });
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
        public HttpResponseMessage AddBranch(Branch obj)
        {
            try
            {
                Branch add = new Branch();
                add.Category_ID = obj.Category_ID;
                add.Name = obj.Name;
                add.Tel = obj.Tel;
                add.Location = obj.Location;
                bool result = bDC.Add(add);
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
        public HttpResponseMessage UpdateBranch(int id, Branch value)
        {
            try
            {
                value.Branch_ID = id;
                bool result = bDC.Update(value);
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
        public HttpResponseMessage DeleteBranch(int id)
        {
            try
            {
                bool result = bDC.Delete(id);
                success = result;
                return Request.CreateResponse(HttpStatusCode.OK, success);
            }
            catch (Exception ex)
            {
                success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { success, ex.Message });
            }
        }
        #region BranchModel
        public class rtnBranch
        {
            public int branch_id { get; set; }
            public string name { get; set; }
            public string tel { get; set; }
            public string location { get; set; }
            public int category_id { get; set; }
            public string category_name { get; set; }
        }
        #endregion
        #endregion
    }
}

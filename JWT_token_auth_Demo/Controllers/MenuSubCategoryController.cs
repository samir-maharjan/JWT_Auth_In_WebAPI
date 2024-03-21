using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;
using JWT_token_auth_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;

namespace JWT_token_auth_Demo.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class MenuSubCategoryController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public MenuSubCategoryController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateSubMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateSubMenuCategory(MenuSubCatVM menuCatVM)
        {
            try
            {
                if (menuCatVM !=null)
                {
                    cat02menu_sub_category category = new cat02menu_sub_category();
                    category.cat02uin = Guid.NewGuid().ToString();
                    category.cat02cat01uin = menuCatVM.CategoryId;
                    category.cat02sub_category_code = menuCatVM.CatDetails.CategoryCode;
                    category.cat02sub_category_title = menuCatVM.CatDetails.CategoryName;
                    category.cat02status = menuCatVM.CatDetails.Status;
                    category.cat02deleted = menuCatVM.CatDetails.Deleted;
                    category.cat02created_name = "Admin";
                    category.cat02updated_name = "Admin";
                    category.cat02created_date = DateTime.Now;
                    category.cat02updated_date = DateTime.Now;

                    _dbcontext.cat02menu_sub_category.Add(category);
                    _dbcontext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };

        }

        [HttpGet("SubCategoryList")]
        public async Task<IEnumerable<SubCategoryResponseVM>> SubCategoryList()
        {
            try
            {
                List<cat02menu_sub_category> res = await _dbcontext.cat02menu_sub_category.Where(x => !x.cat02deleted).Include(x=>x.cat01menu_category).ToListAsync();
                IList<SubCategoryResponseVM> resList = new List<SubCategoryResponseVM>();
                foreach (var item in res)
                {
                    SubCategoryResponseVM res1 = new SubCategoryResponseVM()
                    {
                        ID = item.cat02uin,
                        CategoryID = item.cat02cat01uin,
                        CategoryTitle = item.cat01menu_category.cat01category_title,
                        SubCategoryTitle = item.cat02sub_category_title,
                        SubCategoryCode = item.cat02sub_category_code,
                        Status = item.cat02status,
                        Deleted = item.cat02deleted
                    };
                    resList.Add(res1);
                }

                return resList;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }

           
        }

        [HttpGet("UpdateSubMenuCategory")]
        public async Task<SubCategoryResponseVM> UpdateSubMenuCategory(string id)
        {
            try
            {
                cat02menu_sub_category res = _dbcontext.cat02menu_sub_category.Where(x => x.cat02uin == id).Include(x => x.cat01menu_category).FirstOrDefault();
                if (res == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                SubCategoryResponseVM res1 = new SubCategoryResponseVM()
                {
                    ID = res.cat02uin,
                    SubCategoryCode = res.cat02sub_category_code,
                    SubCategoryTitle = res.cat02sub_category_title,
                    CategoryID = res.cat02cat01uin,
                    CategoryTitle = res.cat01menu_category.cat01category_title,
                    Status = res.cat02status,
                    Deleted = res.cat02deleted
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateSubCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateSubCategory(SubCategoryVM res)
        {
            try
            {
                cat02menu_sub_category catDetails = _dbcontext.cat02menu_sub_category.Where(x => x.cat02uin == res.ID).Include(x=>x.cat01menu_category).FirstOrDefault();
                if (catDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                catDetails.cat02sub_category_code = res.SubCategoryCode;
                catDetails.cat02sub_category_title = res.SubCategoryTitle;
                catDetails.cat02cat01uin = res.CategoryID;
                catDetails.cat02status = res.Status;
                catDetails.cat02deleted = res.Deleted;
                catDetails.cat02updated_date = DateTime.Now;
                catDetails.cat02updated_name = "admin";

                _dbcontext.cat02menu_sub_category.Update(catDetails);
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new AfterSavedResponseVM { status = false, error_msg = ex.ToString() };
                throw new Exception("Error:", ex);
            }
            return new AfterSavedResponseVM { status = true };
        }
    }
}

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
    public class MenuCategoryController : ControllerBase
    {

        private readonly AppDbContext _dbcontext;
        public MenuCategoryController(AppDbContext appDbContext)
        {
            _dbcontext = appDbContext;
        }

        [HttpPost("CreateMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> CreateMenuCategory(MenuCatVM menuCatVM)
        {
            try
            {
                if (menuCatVM !=null)
                {
                    cat01menu_category category = new cat01menu_category();
                    category.cat01uin = Guid.NewGuid().ToString();
                    category.cat01category_code = menuCatVM.CategoryCode;
                    category.cat01category_title = menuCatVM.CategoryName;
                    category.cat01status = menuCatVM.Status;
                    category.cat01deleted = menuCatVM.Deleted;
                    category.cat01created_name = "Admin";
                    category.cat01updated_name = "Admin";
                    category.cat01created_date = DateTime.Now;
                    category.cat01updated_date = DateTime.Now;

                    _dbcontext.cat01menu_category.Add(category);
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

        [HttpGet("CategoryList")]
        public async Task<IEnumerable<CategoryResponseVM>> CategoryList()
        {
            try
            {
                List<cat01menu_category> res = await _dbcontext.cat01menu_category.Where(x => !x.cat01deleted).ToListAsync();
                IList<CategoryResponseVM> resList = new List<CategoryResponseVM>();
                foreach (var item in res)
                {
                    CategoryResponseVM res1 = new CategoryResponseVM()
                    {
                        ID = item.cat01uin,
                        CategoryTitle = item.cat01category_title,
                        CategoryCode = item.cat01category_code,
                        Status = item.cat01status,
                        Deleted = item.cat01deleted
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



        [HttpGet("UpdateMenuCategory")]
        public async Task<CategoryResponseVM> UpdateMenuCategory(string id)
        {
            try
            {
                cat01menu_category imgDetails = _dbcontext.cat01menu_category.Where(x => x.cat01uin == id).FirstOrDefault();
                if (imgDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                CategoryResponseVM res1 = new CategoryResponseVM()
                {
                    ID = imgDetails.cat01uin,
                    CategoryCode = imgDetails.cat01category_code,
                    CategoryTitle = imgDetails.cat01category_title,
                    Status = imgDetails.cat01status,
                    Deleted = imgDetails.cat01deleted
                };
                return res1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error:", ex);
            }
        }


        [HttpPost("UpdateMenuCategory")]
        public async Task<ActionResult<AfterSavedResponseVM>> UpdateCategory(CategoryResponseVM res)
        {
            try
            {
                cat01menu_category catDetails = _dbcontext.cat01menu_category.Where(x => x.cat01uin == res.ID).FirstOrDefault();
                if (catDetails == null)
                {
                    throw new Exception("Error:Data Not Found!");

                }
                catDetails.cat01status = res.Status;
                catDetails.cat01category_code = res.CategoryCode;
                catDetails.cat01category_title = res.CategoryTitle;
                catDetails.cat01deleted = res.Deleted;
                catDetails.cat01updated_date = DateTime.Now;
                catDetails.cat01updated_name = "admin";

                _dbcontext.cat01menu_category.Update(catDetails);
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

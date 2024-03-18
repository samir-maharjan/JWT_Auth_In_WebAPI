using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using JWT_token_auth_Demo.StaticHelper;
using JWT_token_auth_Demo.Data;
using Microsoft.AspNetCore.Identity;
using JWT_token_auth_Demo.Models;

namespace OnlineFormAPI.Controllers
{
    [EnableCors("MyPolicy")]
    public abstract class _AbsBaseAPIController<T> : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly ILogger<T> _logger;
        protected readonly AppDbContext _MainDBContext;
        protected readonly GeneralAppConfig _GeneralAppConfig;
        protected _AbsBaseAPIController(ILogger<T> logger, UserManager<ApplicationUser> userManager, AppDbContext MainDBContext, GeneralAppConfig generalAppConfig)
        {
            _logger = logger;
            _MainDBContext = MainDBContext;
            _GeneralAppConfig = generalAppConfig;
            _userManager = userManager;

        }
    }
}
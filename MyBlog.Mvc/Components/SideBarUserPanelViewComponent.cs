using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Components
{
    public class SideBarUserPanelViewComponent : ViewComponent
    {
        private IUserService _userService;
        public SideBarUserPanelViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userInfo = _userService.GetUserInformationForUserPanel(User.Identity.Name);
            
            return await Task.FromResult((IViewComponentResult)View("SideBarUserPanel" , userInfo));
        }
    }
}

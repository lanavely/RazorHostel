using Auto.Data.Entities;
using Auto.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Auto.Pages.Users
{
    [Authorize(Roles = Consts.AdminInstructor)]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public IndexModel(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public List<DetailsUserModel> Users { get;set; } = default!;

        public bool IsAdmin;

        public async Task OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            
            Users = users.Select(_mapper.Map<DetailsUserModel>).ToList();
            foreach (var u in Users)
            {
                u.RoleName = (await _userManager.GetRolesAsync(users.First(x => x.Id == u.Id))).FirstOrDefault();
            }

            var user = await _userManager.GetUserAsync(User);
            IsAdmin = await _userManager.IsInRoleAsync(user, Consts.Admin);
        }
    }
}

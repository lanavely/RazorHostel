using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Auto.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auto.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;

        public IndexModel(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public IList<UserEntity> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }
    }
}

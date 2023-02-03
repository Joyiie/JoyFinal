using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Roles
{
    public class Create : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Create(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(View.Name))
            {
                ModelState.AddModelError("", "Role name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Description))
            {
                ModelState.AddModelError("", "Description cannot be blank.");
                return Page();
            }

            var existingRole = _context?.Roles?.FirstOrDefault(a => a.Name.ToLower() == View.Name.ToLower());
            if (existingRole != null)
            {
                ModelState.AddModelError("", "Role is already existing.");
                return Page();
            }

            Role role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = View.Name,
                Description = View.Description,
                Abbreviation = View.Abbreviation
            };

            _context?.Roles?.Add(role);
            _context?.SaveChanges();

            return RedirectPermanent("~/manage/roles");
        }

        public class ViewModel : Role
        {

        }
    }
}
using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Roles
{
    public class Update : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Update(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(Guid? id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _context?.Roles?.Where(a => a.Id == id)
                                   .Select(a => new ViewModel()
                                   {
                                       Abbreviation = a.Abbreviation,
                                       Description = a.Description,
                                       Id = a.Id,
                                       Name = a.Name
                                   }).FirstOrDefault();

            if (role == null)
            {
                return NotFound();
            }

            View = role;
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

            var existingRole = _context?.Roles?.FirstOrDefault(a =>
                    a.Id != View.Id &&
                    a.Name.ToLower() == View.Name.ToLower()
            );

            if (existingRole != null)
            {
                ModelState.AddModelError("", "Role is already existing.");
                return Page();
            }

            var role = _context?.Roles?.FirstOrDefault(a => a.Id == View.Id);

            if (role != null)
            {
                role.Abbreviation = View.Abbreviation;
                role.Description = View.Description;
                role.Name = View.Name;

                _context?.Roles?.Update(role);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/roles");
            }

            return Page();

        }

        public class ViewModel : Role
        {

        }
    }
}

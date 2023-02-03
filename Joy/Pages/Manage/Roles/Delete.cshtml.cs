using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Roles
{
    public class Delete : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Delete(DefaultDbContext context, ILogger<Index> logger)
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
            if (View.Id == null)
            {
                return NotFound();
            }

            var role = _context?.Roles?.FirstOrDefault(a => a.Id == View.Id);

            if (role != null)
            {
                _context?.Roles?.Remove(role);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/roles");
            }

            return NotFound();

        }

        public class ViewModel : Role
        {

        }
    }
}

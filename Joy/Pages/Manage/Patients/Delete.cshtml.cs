using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Patients
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

            var patient = _context?.Patients?.Where(a => a.PatientId == id)
                                   .Select(a => new ViewModel()
                                   {
                                       PatientId = a.PatientId,
                                       FullName = a.FullName,
                                       Code = a.Code,
                                       DateOfBirth = a.DateOfBirth,
                                       Type = a.Type
                                   }).FirstOrDefault();

            if (patient == null)
            {
                return NotFound();
            }

            View = patient;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (View.PatientId == null)
            {
                return NotFound();
            }

            var patient = _context?.Patients?.FirstOrDefault(a => a.PatientId == View.PatientId);

            if (patient != null)
            {
                _context?.Patients?.Remove(patient);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/patients");
            }

            return NotFound();

        }

        public class ViewModel : Patient
        {

        }
    }
}

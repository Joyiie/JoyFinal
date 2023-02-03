using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Patients
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

            var role = _context?.Patients?.Where(a => a.PatientId == id)
                                   .Select(a => new ViewModel()
                                   {
                                       
                                       PatientId = a.PatientId,
                                       FullName = a.FullName,
                                       Code  = a.Code,
                                       DateOfBirth = a.DateOfBirth,
                                       Type = a.Type
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
            if (string.IsNullOrEmpty(View.FullName))
            {
                ModelState.AddModelError("", "name cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Code))
            {
                ModelState.AddModelError("", " cannot be blank.");
                return Page();
            }

            var existingPatient = _context?.Patients?.FirstOrDefault(a =>
                    a.PatientId != View.PatientId &&
                    a.FullName.ToLower() == View.FullName.ToLower()
            );

            if (existingPatient != null)
            {
                ModelState.AddModelError("", "Patient is already existing.");
                return Page();
            }

            var patient = _context?.Patients?.FirstOrDefault(a => a.PatientId == View.PatientId);

            if (patient != null)
            {
                patient.Code = View.Code;
                patient.FullName = View.FullName;
                patient.DateOfBirth = View.DateOfBirth;
                patient.Type = View.Type;

               

                _context?.Patients?.Update(patient);
                _context?.SaveChanges();

                return RedirectPermanent("~/manage/Patient");
            }

            return Page();

        }

        public class ViewModel : Patient
        {

        }
    }
}

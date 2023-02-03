using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Joy.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Joy.Pages.Manage.Patients
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
            if (string.IsNullOrEmpty(View.FullName))
            {
                ModelState.AddModelError("", " cannot be blank.");
                return Page();
            }

            if (string.IsNullOrEmpty(View.Code))
            {
                ModelState.AddModelError("", " be blank.");
                return Page();
            }
            if (!Enum.IsDefined(typeof(Enum), View.Type))
            {
                ModelState.AddModelError("", "Sex name cannot be blank.");
                return Page();
            }
            if (DateTime.MinValue >= View.DateOfBirth)
            {
                ModelState.AddModelError("", "Birthdate name cannot be blank.");
                return Page();
            }

            var existingPatient = _context?.Patients?.FirstOrDefault(a => a.FullName.ToLower() == View.FullName.ToLower());
            if (existingPatient != null)
            {
                ModelState.AddModelError("", "is already existing.");
                return Page();
            }
            Guid PPGuid = Guid.NewGuid();

            Patient patient = new Patient()
            { 
                Code  =View.Code,
                PatientId = PPGuid,
                FullName = View.FullName,
                DateOfBirth =View.DateOfBirth,
                Type = View.Type

          
            };
       

            _context?.Patients?.Add(patient);
           
            _context?.SaveChanges();

            return RedirectPermanent("~/manage/Patient");
        }

        public class ViewModel : Patient
        {

        }
    }
}

using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joy.Pages.Manage.Patients
{
    [Authorize(Roles = "Admin")]
    public class Index : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public IActionResult OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "")
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.FullName != null && a.FullName.ToLower().Contains(keyword.ToLower())
                        || a.Code != null && a.Code.ToLower().Contains(keyword.ToLower())
                      
                );
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "fullname" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.FullName);
                }
                else if (sortBy.ToLower() == "fullname" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.FullName);
                }
                else if (sortBy.ToLower() == "code" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Code);
                }
                else if (sortBy.ToLower() == "code" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Code);
                }
              
            }

            var patients = query
                            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            View.Patients = new Paged<Patient>()
            {
                Items = patients,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword
            };

            return Page();
        }

        public class ViewModel
        {
            public Paged<Patient>? Patients { get; set; }
        }
    }
}

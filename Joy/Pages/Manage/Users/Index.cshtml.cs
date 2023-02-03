using Joy.Infrastructure.Domain;
using Joy.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Joy.Pages.Manage.Users
{
    public class Index : PageModel
    {
        private DefaultDbContext _context;
        private ILogger<Index> _logger;

        [BindProperty]
        public ViewModel View { get; set; }

        public Index(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public void OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "", Guid? roleId = null)
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = _context.Users
                                .Include(a => a.Role)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Name != null && a.Name.ToLower().Contains(keyword.ToLower())
                        || a.EmailAddress != null && a.EmailAddress.ToLower().Contains(keyword.ToLower())
                );
            }

            if (roleId != null)
            {
                query = query.Where(a => a.RoleId == roleId);
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Name);
                }
                else if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Name);
                }
                else if (sortBy.ToLower() == "emailaddress" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.EmailAddress);
                }
                else if (sortBy.ToLower() == "emailaddress" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.EmailAddress);
                }
            }

            var users = query
                            .Skip(skip)
                            .Take((int)pageSize)
                            .ToList();

            if (roleId != null)
            {
                View.RoleId = roleId;
                View.RoleName = users.FirstOrDefault()?.Role?.Name;
            }

            View.Users = new Paged<User>()
            {
                Items = users,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder,
                Keyword = keyword,
            };
        }

        public JsonResult? OnGetRolesLookup(int pageIndex = 1, string? keyword = "", int pageSize = 10)
        {

            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Name != null && a.Name.ToLower().StartsWith(keyword.ToLower())
                );
            }

            return new JsonResult(query?.Select(a => new LookupDto.Result()
            {
                Id = a.Id.ToString(),
                Text = a.Name
            })
            .OrderBy(a => a.Text)
            .GetLookupPaged(pageIndex, pageSize));
        }

        public class ViewModel
        {
            public Paged<User>? Users { get; set; }
            public Guid? RoleId { get; set; }
            public string? RoleName { get; set; }
        }


    }
}

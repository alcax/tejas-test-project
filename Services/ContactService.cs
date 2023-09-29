using Microsoft.EntityFrameworkCore;
using SampleProject.Common;
using SampleProject.Database;
using SampleProject.Database.Entity;
using SampleProject.Interface;
using SampleProject.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace SampleProject.Services
{
    public class ContactService : IContactService
    {
        private ApplicationDbContext _context;
        public ContactService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionOutput> AddContact(AddContactModel model)
        {
            await _context.Contact.AddAsync(new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Body = model.Body,
                Email = model.Email,
                Gender = model.Gender,
                PhoneNumber = model.PhoneNumber,
                Reason = model.Reason,
            });
            await _context.SaveChangesAsync();
            return new ActionOutput
            {
                Message = "Contact details has been saved.",
                Status = ActionStatus.Successfull
            };
        }

        public async Task<PagingResult<ContactListingModel>> GetContactPagedlist(PagingModel model)
        {
            var result = new PagingResult<ContactListingModel>
            {
                Message = "list",
                Status = ActionStatus.Successfull,
            };

            var query = _context.Contact.OrderBy(model.SortBy + " " + model.SortOrder).AsQueryable();
            if (!string.IsNullOrEmpty(model.Search))
            {
                model.Search = model.Search.Trim();
                query = query.Where(f => f.FirstName.ToLower().Trim().Contains(model.Search)
                || f.LastName.ToLower().Trim().Contains(model.Search)
                 || f.Gender.ToLower().Trim().Contains(model.Search)
                || f.Email.ToLower().Trim().Contains(model.Search)
                || f.Reason.ToLower().Trim().Contains(model.Search)
                ).AsQueryable();
            }

            result.TotalCount = await query.CountAsync();
            result.List = await query
                .Skip((model.PageNo - 1) * model.RecordsPerPage)
                .Take(model.RecordsPerPage)
                .Select(x => new ContactListingModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Gender = x.Gender,
                    Reason = x.Reason,
                    Body = x.Body,
                    PhoneNumber = x.PhoneNumber
                }).ToListAsync();
            return result;
        }

        public async Task<ContactDetailsModel> GetContactDetailsById(long id)
        {
            return await _context.Contact.Where(x => x.Id == id)
                .Select(x => new ContactDetailsModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Gender = x.Gender,
                    Reason = x.Reason,
                    Body = x.Body,
                    PhoneNumber = x.PhoneNumber
                }).FirstOrDefaultAsync();
        }
    }
}

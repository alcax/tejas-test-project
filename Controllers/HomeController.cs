using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleProject.Common;
using SampleProject.Interface;
using SampleProject.Models;
using System.Diagnostics;
namespace SampleProject.Controllers;
public class HomeController : BaseController
{
    //Dependency Injection
    private readonly IContactService _contactService;
    public HomeController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public IActionResult Contact()
    {
        // Converting the Gender Enum into the SelectListItem
        ViewBag.Gender = Utilities.EnumToList(typeof(GenderEnum)).Select(x => new SelectListItem
        {
            Text = x.Text,
            Value = x.Text
        }).ToList();

        // Converting the Reason Enum into the SelectListItem
        ViewBag.Reason = Utilities.EnumToList(typeof(ReasonEnum)).Select(x => new SelectListItem
        {
            Text = x.Text,
            Value = x.Text
        }).ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddContact(AddContactModel model)
    {
        var result = await _contactService.AddContact(model);
        if (result.Status == ActionStatus.Successfull)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetContacts()
    {
        try
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            // Getting data from service 
            var result = await _contactService.GetContactPagedlist(new PagingModel
            {
                Search = searchValue,
                PageNo = (skip > 0 ? skip / pageSize : 0) + 1,
                RecordsPerPage = pageSize,
                SortBy = sortColumn,
                SortOrder = sortColumnDirection
            });
            var jsonData = new { draw = draw, recordsFiltered = result.TotalCount, recordsTotal = result.TotalCount, data = result.List };
            return Ok(jsonData);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetContactDetailsById(long id)
    {
        var details = await _contactService.GetContactDetailsById(id);
        if (details != null)
            return Ok(details);
        else
        {
            return BadRequest(new ActionOutput
            {
                Message = "Contact detail doesn't exist.",
                Status = ActionStatus.Error
            });
        }

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using Microsoft.AspNetCore.Mvc;
using static SampleProject.Common.Attribute;

namespace SampleProject.Controllers
{
    [TypeFilter(typeof(WebValidateModelAttribute))]
    public class BaseController : Controller
    {

    }
}

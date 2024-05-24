
using Business.Services.Abstracts;
using ExamMvc.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _service;

        public HomeController(ITeamService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var teams= _service.GetAllTeam(x=>x.DeletedDate==null);

            if (teams is null)
            {
                return NotFound();
            }

            HomeVm vm = new HomeVm()
            {
                Teams = teams

            };
            return View(vm);
        }

    }
}

using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }


        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex<=0)
            {
                pageIndex = 1;
            }
            else if (pageSize<=0)
            {
                pageSize = 2;
            }
            var paginatedTeams = await _teamService.GetPagedTeamsAsync(pageIndex, pageSize);
            return View(paginatedTeams);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _teamService.CreateTeamAsync(team);
            }
            catch (FileExtensionsException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileRequiredException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _teamService.DeleteTeam(id);
            }
            catch (EntityNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FileDoesNotExsistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        public IActionResult Update(int id)
        {
            var existTeam = _teamService.GetTeam(x => x.Id == id && x.DeletedDate == null);
            if (existTeam is null)
            {
                return NotFound();
            }
            return View(existTeam);
        }
        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (team is null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _teamService.UpdateTeam(team.Id, team);
            }
            catch (FileExtensionsException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileRequiredException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileDoesNotExsistException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (EntityNullException ex) { 
                 return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");

        }

    }
}

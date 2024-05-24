using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Pronia.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SettingController : Controller
{
    private readonly ISettingService _settingService;

    public SettingController(SettingService settingService)
    {
        _settingService = settingService;
    }

    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 2)
    {
        if (pageIndex <= 0)
        {
            pageIndex = 1;
        }
        else if (pageSize <= 0)
        {
            pageSize = 2;
        }

        var settings = await _settingService.GetPagedSettingsAsync(pageIndex,pageSize);

        return View(settings);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Setting setting)
    {
        await _settingService.CreateSettingAsync(setting);

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _settingService.DeleteSetting(id);
        }
        catch (EntityNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }
    public IActionResult Update(int id)
    {
        var existSetting = _settingService.GetSetting(x => x.Id == id && x.DeletedDate == null);
        if (existSetting is null)
        {
            return NotFound();
        }
        return View(existSetting);
    }
    [HttpPost]
    public IActionResult Update(Setting setting)
    {
        if (setting is null)
        {
            return NotFound();
        }
        else if (!ModelState.IsValid)
        {
            return View();
        }
        try
        {
            _settingService.UpdateSetting(setting.Id, setting);
        }
        catch (EntityNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


        return RedirectToAction("Index");

    }
}

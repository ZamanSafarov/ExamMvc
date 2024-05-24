using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]

public class SettingController : Controller
{
    private readonly SettingService _settingService;

    public SettingController(SettingService settingService)
    {
        _settingService = settingService;
    }

    public IActionResult Index()
    {
        var settings = _settingService.GetAllSetting(x => x.DeletedDate == null);
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
}

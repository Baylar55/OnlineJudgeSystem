﻿namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SubscriptionsController : BaseMVCController
{
    [HttpGet]
    public async Task<IActionResult> Index(GetAllSubscriptionsQuery query)
    {
        return View(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<IActionResult> Details(GetSubscriptionByIdQuery query)
    {
        return View(await Mediator.Send(query));
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.SubscriptionType = EnumHelper.GetEnumSelectList<SubscriptionType>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(GetSubscriptionByIdQuery query)
    {
        ViewBag.SubscriptionType = EnumHelper.GetEnumSelectList<SubscriptionType>();

        return View(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSubscriptionCommand command)
    {
        if (!ModelState.IsValid) return View(command);

        var validationResult = await Mediator.Send(command);

        if (validationResult.Errors.Count != 0)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(DeleteSubscriptionCommand command)
    {
        await Mediator.Send(command);

        return RedirectToAction(nameof(Index));
    }
}

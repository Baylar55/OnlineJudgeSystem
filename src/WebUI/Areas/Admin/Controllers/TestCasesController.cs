﻿namespace AlgoCode.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
[Route("admin/testcases")]
public class TestCasesController : BaseMVCController
{
    [HttpGet("[action]")]
    public async Task<IActionResult> Index(int page)
    {
        if (page < 1)
            return RedirectToAction(nameof(Index), new { page = 1 });
        var model = await Mediator.Send(new GetTestCasesWithPaginationQuery()
        {
            PageNumber = page,
        });
        return View(model);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Create()
    {
        var problems = await Mediator.Send(new GetProblemsWithPaginationQuery());
        ViewBag.SelectItems = new SelectList(problems.Problems, "Id", "Title");
        return View();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateTestCaseCommand command)
    {
        var validationResult = await Mediator.Send(command);
        if (!validationResult.IsValid)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }
        return RedirectToAction("Index");
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Update(int id)
    {
        var testCase = await Mediator.Send(new GetTestCaseByIdQuery(id));
        var problems = await Mediator.Send(new GetProblemsWithPaginationQuery());
        ViewBag.SelectItems = new SelectList(problems.Problems, "Id", "Title");

        var mainModel = testCase.Adapt<UpdateTestCaseCommand>();

        return View(mainModel);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UpdateTestCaseCommand command)
    {
        var validationResult = await Mediator.Send(command);
        if (!validationResult.IsValid)
        {
            ErrorHandlingService.AddErrorsToModelState(validationResult);
            return View(command);
        }
        return RedirectToAction("Index");
    }

}

using AlgoCode.Application.Features.Problem.Commands.CompileProblem;

namespace AlgoCode.WebUI.Controllers
{
    public class ProblemsController : BaseMVCController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompileAndRun([FromBody] CompileProblemCommand command)
        {
            var result = await Mediator.Send(command);
            return Content(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace SuperHeroAPI.Filters;

public class ActionFilter : IActionFilter
{
    private readonly ILogger<ActionFilter> _logger;

    public ActionFilter(ILogger<ActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }
}
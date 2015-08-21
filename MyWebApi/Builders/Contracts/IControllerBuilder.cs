namespace MyWebApi.Builders.Contracts
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;

    public interface IControllerBuilder<TController>
        where TController : ApiController
    {
        IActionResultBuilder<TAction> Calling<TAction>(Expression<Func<TController, TAction>> actionCall);

        IActionResultBuilder<TAction> Calling<TAction>(Expression<Func<TController, Task<TAction>>> actionCall);
    }
}

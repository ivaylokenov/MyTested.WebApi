namespace MyWebApi.Builders.Contracts.And
{
    using System.Web.Http;

    using Actions;

    public interface IAndContinuityTestBuilder<out TActionResult> : IActionResultTestBuilder<TActionResult>
    {
        TActionResult ProvideTheActionResult();
    }
}

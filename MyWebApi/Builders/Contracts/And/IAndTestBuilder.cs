namespace MyWebApi.Builders.Contracts.And
{
    using Actions;

    public interface IAndTestBuilder<out TActionResult>
    {
        IActionResultTestBuilder<TActionResult> And();
    }
}

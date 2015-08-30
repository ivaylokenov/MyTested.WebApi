namespace MyWebApi.Builders.Contracts.And
{
    public interface IAndTestBuilder<out TActionResult>
    {
        IAndContinuityTestBuilder<TActionResult> And();
    }
}

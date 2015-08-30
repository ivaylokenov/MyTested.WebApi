namespace MyWebApi.Builders.Contracts.And
{
    public interface IAndTestBuilder<out TActionResult>
    {
        void And();
    }
}

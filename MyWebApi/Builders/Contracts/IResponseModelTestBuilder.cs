namespace MyWebApi.Builders.Contracts
{
    public interface IResponseModelTestBuilder<TActionResult>
    {
        void WithResponseModel<TResponseData>();
    }
}

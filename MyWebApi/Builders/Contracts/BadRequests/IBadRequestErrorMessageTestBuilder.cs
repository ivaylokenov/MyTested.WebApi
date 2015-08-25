namespace MyWebApi.Builders.Contracts.BadRequests
{
    public interface IBadRequestErrorMessageTestBuilder
    {
        void ThatEquals(string errorMessage);

        void BeginningWith(string beginMessage);

        void EndingWith(string endMessage);

        void Containing(string containsMessage);
    }
}

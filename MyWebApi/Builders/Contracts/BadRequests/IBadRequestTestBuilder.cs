namespace MyWebApi.Builders.Contracts.BadRequests
{
    using System.Web.Http.ModelBinding;

    using Models;

    public interface IBadRequestTestBuilder
    {
        IBadRequestErrorMessageTestBuilder WithErrorMessage();

        void WithErrorMessage(string message);

        void WithModelState(ModelStateDictionary modelState);

        IModelErrorTestBuilder<TRequestModel> WithModelStateFor<TRequestModel>();
    }
}

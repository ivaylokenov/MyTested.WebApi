namespace MyWebApi.Builders.Contracts.Base
{
    public interface IBaseTestBuilderWithModel<out TModel> : IBaseTestBuilder
    {
        TModel AndProvideTheModel();
    }
}

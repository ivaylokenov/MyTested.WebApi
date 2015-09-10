namespace MyWebApi.Builders.Contracts.Created
{
    public interface IAndFormattersBuilder : IFormattersBuilder
    {
        IFormattersBuilder AndAlso();
    }
}

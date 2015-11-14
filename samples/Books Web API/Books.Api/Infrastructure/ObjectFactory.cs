namespace Books.Api.Infrastructure
{
    using Ninject;

    public static class ObjectFactory
    {
        public static IKernel standartKernel;

        public static void Initialize(IKernel kernel)
        {
            standartKernel = kernel;
        }

        public static T TryGetInstance<T>()
        {
            return standartKernel.TryGet<T>();
        }
    }
}
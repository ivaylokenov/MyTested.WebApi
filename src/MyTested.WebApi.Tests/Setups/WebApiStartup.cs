namespace MyTested.WebApi.Tests.Setups
{
    using System.Web.Http;
    using Owin;

    public class WebApiStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                if (context.Request.CallCancelled.IsCancellationRequested)
                {
                    context.Response.StatusCode = 500;
                    context.Response.Write("Canceled");
                }

                return next();
            });

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}

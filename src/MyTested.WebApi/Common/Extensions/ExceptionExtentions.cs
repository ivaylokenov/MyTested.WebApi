namespace MyTested.WebApi.Common.Extensions
{
    using System;

    public static class ExceptionExtentions
    {
        public static bool IsRouteConstraintRelatedException(this Exception ex)
        {
            return ex.Message.Contains("was unable to resolve the following inline constraint") || ex.TargetSite.Name == "GetInlineConstraint";
        }
    }
}

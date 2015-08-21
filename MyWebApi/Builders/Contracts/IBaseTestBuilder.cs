namespace MyWebApi.Builders.Contracts
{
    public interface IBaseTestBuilder<out TActionResult>
    {
        /// <summary>
        /// Gets the action name which will be tested.
        /// </summary>
        /// <value>Action name to be tested.</value>
        string ActionName { get; }

        /// <summary>
        /// Gets the action result which will be tested.
        /// </summary>
        /// <value>Action result to be tested.</value>
        TActionResult ActionResult { get; }
    }
}

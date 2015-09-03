namespace MyWebApi.Builders.Contracts.Actions
{
    using And;
    using Base;
    using Models;

    /// <summary>
    /// Used for building the action result which will be tested.
    /// </summary>
    /// <typeparam name="TActionResult">Type of action result to be tested.</typeparam>
    public interface IActionResultTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        IShouldHaveTestBuilder<TActionResult> ShouldHave();

        IShouldThrowTestBuilder ShouldThrow();

        IShouldReturnTestBuilder<TActionResult> ShouldReturn();
    }
}

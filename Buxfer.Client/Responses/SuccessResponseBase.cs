namespace Buxfer.Client.Responses
{
    /// <summary>
    ///     Base class form success responses.
    /// </summary>
    public abstract class SuccessResponseBase
    {
        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public ResponseStatus Status { get; set; }
    }
}
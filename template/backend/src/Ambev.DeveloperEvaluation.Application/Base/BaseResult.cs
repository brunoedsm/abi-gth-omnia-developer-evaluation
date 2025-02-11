namespace Ambev.DeveloperEvaluation.Application.Base
{
    /// <summary>
    /// Base Command Result class
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// Indicate if Handler Operation had success or not
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// List of errors when exists
        /// </summary>
        public List<string> Errors { get; set; }  = [];
    }
}

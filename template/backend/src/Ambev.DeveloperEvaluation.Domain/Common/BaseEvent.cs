using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    /// <summary>
    /// Abstraction of message broker events with base attributes
    /// </summary>
    public class BaseEvent
    {
        /// <summary>
        /// Event identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Event content
        /// </summary>
        public string? Data { get; set; }

        /// <summary>
        /// Event default constructor
        /// </summary>
        /// <param name="obj"></param>
        public BaseEvent(object obj)
        {
            Id = Guid.NewGuid();
            Data = JsonSerializer.Serialize(obj);
        }
    }
}

using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    /// <summary>
    /// Generic Handler for event broadcast (message broker)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEventHandler<T> where T : BaseEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public string Notify(BaseEvent @event)
        {
            try
            {
                var message = JsonSerializer.Serialize(new
                {
                    Event = nameof(@event),
                    Data = JsonSerializer.Serialize(@event),
                    Timestamp = DateTime.UtcNow
                });

                // Publish simulation
                Console.WriteLine(message);
                // Output for DataDog, Kibana, Etc...
                return $"Event {nameof(@event)} ID: {@event.Id}, Content {@event.Data} to Message Broker made with sucess.";
            }
            catch (Exception ex)
            {

                // Output for DataDog, Kibana, Etc...
                return $"Publishing event {nameof(@event)} ID: {@event.Id}, Content {@event.Data} returned error:{ex.Message}";
            }
        }
    }
}

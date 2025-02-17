using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public abstract class NotificationService<T> where T : BaseEvent
    {
        /// <summary>
        /// Generic Notification Service for message publish
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public virtual string Notify(BaseEvent @event)
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

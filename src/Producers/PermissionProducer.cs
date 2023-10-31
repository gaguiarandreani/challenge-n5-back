using Confluent.Kafka;
using Interfaces.Producers;
using System.Text.Json;

namespace Producers
{
    public class PermissionProducer : IPermissionProducer
    {
        private readonly ProducerConfig _config;
        private readonly string _topic;

        public PermissionProducer()
        {
            _config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _topic = "permissions-registry";
        }

        public async Task<bool> ProduceAsync(PermissionProducerRegistry registry)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                registry.Id = Guid.NewGuid();

                var messageValue = JsonSerializer.Serialize(registry);
                var result = false;

                producer.Produce(_topic, new Message<Null, string> { Value = messageValue }, (deliveryReport) =>
                {
                    if (deliveryReport.Error?.IsError == true)
                    {
                        Console.WriteLine($"Delivery Error: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine($"Produced message: {messageValue}");

                        result = true;
                    }
                });

                return result;
            }
        }
    }
}
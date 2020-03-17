using System;
using System.Threading;
using Confluent.Kafka;

namespace Messangee.Sample.Producer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start producer process");
            var conf = new ProducerConfig
            {
                BootstrapServers = "omnibus-01.srvs.cloudkafka.com:9094,omnibus-02.srvs.cloudkafka.com:9094,omnibus-03.srvs.cloudkafka.com:9094",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "0sx3x3f8",
                SaslPassword = "DB6Neo2DPii-qn2IfWhIrh7E-3qxO2-5",
            };

            Action<DeliveryReport<Null, string>> handler = r =>
            {
                var msg = !r.Error.IsError
                    ? $"Delivered message to {r.TopicPartitionOffset}"
                    : $"Delivery Error: {r.Error.Reason}";
                Console.WriteLine(msg);
            };

            using (var p = new ProducerBuilder<Null, string>(conf).Build())
            {
                for (int i = 0; i < 100; ++i)
                {
                    Console.WriteLine("Producing message to 0sx3x3f8-default");
                    Thread.Sleep(3000);
                    p.Produce("0sx3x3f8-test", new Message<Null, string> { Value = i.ToString() }, handler);
                }

                // wait for up to 10 seconds for any inflight messages to be delivered.
                p.Flush(TimeSpan.FromSeconds(10));
            }
            Console.WriteLine("Exit producer process");
        }
    }
}

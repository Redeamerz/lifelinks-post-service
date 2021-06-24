using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Post_Service
{
	public class KafkaConsumerHandler : IHostedService
	{
		private readonly string topic = "gdpr_topic";
		public Task StartAsync(CancellationToken cancellationToken)
		{
			var conf = new ConsumerConfig
			{
				GroupId = "st_comsumer_group",
				BootstrapServers = "localhost:9092",
				AutoOffsetReset = AutoOffsetReset.Earliest
			};

			using(var builder = new ConsumerBuilder<Ignore, string>(conf).Build())
			{
				builder.Subscribe(topic);
				var cancelToken = new CancellationTokenSource();
				try
				{
					while (true)
					{
						var consuner = builder.Consume(cancelToken.Token);
						Console.WriteLine($"Message: {consuner.Message.Value} received from {consuner.TopicPartitionOffset}");
					}
				}
				catch (Exception)
				{
					builder.Close();
				}
			}
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}

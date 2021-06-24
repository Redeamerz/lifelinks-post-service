using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Post_Service.Logic;

namespace Post_Service
{
	public class KafkaConsumerHandler : IHostedService
	{
		private readonly string topic = "gdpr_topic";
		private readonly PostHandler postHandler;
		public KafkaConsumerHandler(PostHandler postHandler)
		{
			this.postHandler = postHandler;
		}
		public Task StartAsync(CancellationToken cancellationToken)
		{
			var conf = new ConsumerConfig
			{
				GroupId = "post_comsumer_group",
				BootstrapServers = "kafka",
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
						var consumer = builder.Consume(cancelToken.Token);
						Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
						postHandler.DeleteAllPostsByUser(consumer.Message.Value);
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

﻿using System;
using RawRabbit.Core.Configuration.Exchange;
using RawRabbit.Core.Configuration.Queue;

namespace RawRabbit.Core.Configuration.Subscribe
{
	public class SubscriptionConfigurationBuilder : ISubscriptionConfigurationBuilder
	{
		public SubscriptionConfiguration Configuration => new SubscriptionConfiguration
		{
			Queue = _queueBuilder.Configuration,
			Exchange = _exchangeBuilder.Configuration,
			RoutingKey = _routingKey ?? _queueBuilder.Configuration.QueueName,
			NoAck = _noAck,
			PrefetchCount = _prefetchCount == 0 ? (ushort)50 : _prefetchCount
		};

		private readonly ExchangeConfigurationBuilder _exchangeBuilder;
		private readonly QueueConfigurationBuilder _queueBuilder;
		private string _routingKey;
		private ushort _prefetchCount;
		private bool _noAck;

		public SubscriptionConfigurationBuilder() : this(null, null)
		{ }

		public SubscriptionConfigurationBuilder(QueueConfiguration initialQueue, ExchangeConfiguration initialExchange)
		{
			_exchangeBuilder = new ExchangeConfigurationBuilder(initialExchange);
			_queueBuilder = new QueueConfigurationBuilder(initialQueue);
		}

		public ISubscriptionConfigurationBuilder WithRoutingKey(string routingKey)
		{
			_routingKey = routingKey;
			return this;
		}

		public ISubscriptionConfigurationBuilder WithPrefetchCount(ushort prefetchCount)
		{
			_prefetchCount = prefetchCount;
			return this;
		}

		public ISubscriptionConfigurationBuilder WithNoAck(bool noAck = true)
		{
			_noAck = noAck;
			return this;
		}

		public ISubscriptionConfigurationBuilder WithExchange(Action<IExchangeConfigurationBuilder> exchange)
		{
			exchange(_exchangeBuilder);
			return this;
		}

		public ISubscriptionConfigurationBuilder WithQueue(Action<IQueueConfigurationBuilder> queue)
		{
			queue(_queueBuilder);
			return this;
		}
	}
}

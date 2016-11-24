﻿using System;
using System.Threading.Tasks;
using RawRabbit.Common;
using RawRabbit.Configuration.Legacy.Publish;
using RawRabbit.Configuration.Legacy.Request;
using RawRabbit.Configuration.Legacy.Respond;
using RawRabbit.Configuration.Legacy.Subscribe;
using RawRabbit.Context;

namespace RawRabbit
{
	public interface ILegacyBusClient<out TMessageContext> : IShutdown where TMessageContext : IMessageContext
	{
		ISubscription SubscribeAsync<T>(Func<T, TMessageContext, Task> subscribeMethod, Action<ISubscriptionConfigurationBuilder> configuration = null);

		Task PublishAsync<T>(T message = default(T), Guid globalMessageId = new Guid(), Action<IPublishConfigurationBuilder> configuration = null);

		ISubscription RespondAsync<TRequest, TResponse>(Func<TRequest, TMessageContext, Task<TResponse>> onMessage, Action<IResponderConfigurationBuilder> configuration = null);

		Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message = default(TRequest), Guid globalMessageId = new Guid(), Action<IRequestConfigurationBuilder> configuration = null);
	}
}
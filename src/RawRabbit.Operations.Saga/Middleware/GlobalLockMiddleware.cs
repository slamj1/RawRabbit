﻿using System;
using System.Threading;
using System.Threading.Tasks;
using RawRabbit.Operations.Saga.Repository;
using RawRabbit.Pipe;

namespace RawRabbit.Operations.Saga.Middleware
{
	public class GlobalLockMiddleware : Pipe.Middleware.Middleware
	{
		private readonly IGlobalLock _globalLock;

		public GlobalLockMiddleware(IGlobalLock globalLock)
		{
			_globalLock = globalLock;
		}

		public override Task InvokeAsync(IPipeContext context, CancellationToken token)
		{
			return _globalLock.ExecuteAsync(context.Get<Guid>(SagaKey.SagaId), () => Next.InvokeAsync(context, token), token);
		}
	}
}

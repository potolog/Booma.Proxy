﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Handler that implements try semantics in attempting to handle a provided message.
	/// It can indicate if the message is consumed/consumable.
	/// </summary>
	/// <typeparam name="TPayloadBaseType"></typeparam>
	public abstract class TrySemanticsClientMessageHandler<TPayloadType, TPayloadBaseType> : IClientMessageHandler<TPayloadBaseType>
		where TPayloadBaseType : class
		where TPayloadType : class, TPayloadBaseType
	{
		/// <summary>
		/// Decorated payload handler that can handle
		/// payloads of type <typeparamref name="TPayloadType"/>.
		/// </summary>
		private IClientPayloadSpecificMessageHandler<TPayloadType, TPayloadBaseType> DecoratedPayloadHandler { get; }

		/// <inheritdoc />
		protected TrySemanticsClientMessageHandler(IClientPayloadSpecificMessageHandler<TPayloadType, TPayloadBaseType> decoratedPayloadHandler)
		{
			if(decoratedPayloadHandler == null) throw new ArgumentNullException(nameof(decoratedPayloadHandler));

			DecoratedPayloadHandler = decoratedPayloadHandler;
		}

		public async Task<bool> TryHandleMessage(IClientMessageContext<TPayloadBaseType> context, PSOBBNetworkIncomingMessage<TPayloadBaseType> message)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(message == null) throw new ArgumentNullException(nameof(message));

			if(message.Payload is TPayloadType payload)
			{
				//No matter what happens in the handler we should indicate that it's consumed
				await DecoratedPayloadHandler.HandleMessage(context, payload);
				return true;
			}

			//Default semantics is a handler can only handle a specific type
			//So we just indicate that we can't handle the message and the caller
			//will hopefully find someone else to handle it.
			return false;
		}
	}
}

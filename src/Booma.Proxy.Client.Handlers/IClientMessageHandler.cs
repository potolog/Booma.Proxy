﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for types that provide message handling logic.
	/// </summary>
	/// <typeparam name="TPayloadBaseType"></typeparam>
	public interface IClientMessageHandler<TPayloadBaseType>
		where TPayloadBaseType : class
	{
		/// <summary>
		/// Tries to handle the provided <see cref="message"/>
		/// and indicates if the message has been consumed.
		/// </summary>
		/// <param name="message">The message to try to handle.</param>
		/// <param name="context">The context for the message.</param>
		/// <returns>
		/// True indicates that the message was handled and consumed. 
		/// False indicates that the handler couldn't handle the message.
		/// </returns>
		Task<bool> TryHandleMessage(IClientMessageContext<TPayloadBaseType> context, PSOBBNetworkIncomingMessage<TPayloadBaseType> message);
	}
}

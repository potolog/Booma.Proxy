﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using SceneJect.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// The component that manages the login network client.
	/// </summary>
	[Injectee]
	public sealed class LoginNetworkClient : SerializedMonoBehaviour
	{
		//TODO: Get external domain name or ip instead of defining it in editor/engine
		/// <summary>
		/// The IPAddress for the remote login server.
		/// </summary>
		[Required("Must provide an Login endpoint IP.")]
		[OdinSerialize]
		public string IpAddress { get; private set; }

		/// <summary>
		/// The port for the remote login server.
		/// </summary>
		[MaxValue(short.MaxValue)]
		[MinValue(1)]
		[OdinSerialize]
		public int Port { get; private set; } = 12000; //default port

		/// <summary>
		/// The login client.
		/// </summary>
		[Inject]
		public IManagedNetworkClient<PSOBBLoginPacketPayloadClient, PSOBBLoginPacketPayloadServer> Client { get; }

		[Inject]
		public ILog Logger { get; }

		//TODO: Is it safe or ok to await in start without ever completing?
		private async Task Start()
		{
			try
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug("Starting login client");

				//As soon as we start we should attempt to connect to the login server.
				bool result = await Client.ConnectAsync(IpAddress, Port);

				if(!result)
					throw new InvalidOperationException($"Failed to connect to Login: {IpAddress} Port: {Port}");

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Connected client. isConnected: {Client.isConnected}");

				while(Client.isConnected)
				{
					if(Logger.IsDebugEnabled)
						Logger.Debug("Reading message.");

					PSOBBNetworkIncomingMessage<PSOBBLoginPacketPayloadServer> message = await Client.ReadMessageAsync();

					Debug.Log(message.Payload.GetType().Name);
				}
			}
			catch(Exception e)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");

				throw;
			}
		}

		public void OnApplicationQuit()
		{
			Client.Disconnect();
		}
	}
}
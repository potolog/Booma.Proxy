﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class ShipSelectionWelcomePayloadHandler : SharedWelcomePayloadHandler
	{
		//This is to keep track of which service we're connected to
		public int welcomeCount = 0;

		/// <inheritdoc />
		protected override PSOBBGamePacketPayloadClient BuildLoginPacket()
		{
			//If we've done this already then we're talking to the block now
			SharedLoginRequest93Payload.ServerType serverType = welcomeCount == 0 ? SharedLoginRequest93Payload.ServerType.Login : SharedLoginRequest93Payload.ServerType.Ship;

			var payload = new SharedLoginRequest93Payload(0x41, SessionDetails.SessionId, SessionDetails.GuildCardNumber, LoginDetails.Username, LoginDetails.Password, new ClientVerificationData(0x41, SessionDetails.SessionVerificationData), serverType);

			welcomeCount++;
			return payload;
		}
	}
}

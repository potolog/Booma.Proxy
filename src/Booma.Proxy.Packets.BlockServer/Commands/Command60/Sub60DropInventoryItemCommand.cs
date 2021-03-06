﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Command sent by players to the server when the character is entering an operation
	/// that requires it to freeze or has a start and end part of the operation.
	/// Such as dropping an item, dying or chatting with an NPC.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.DropInventoryItem)]
	public sealed class Sub60DropInventoryItemCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: What is this? Probably client id
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		//TODO: What is this?
		[WireMember(2)]
		public byte Unknown1 { get; }

		//TODO: What is this?
		[WireMember(3)]
		public short Unknown2 { get; }

		//TODO: Create an enum? If it possible
		/// <summary>
		/// ID for the zone the character is in.
		/// </summary>
		[WireMember(4)]
		public short ZoneId { get; }

		/// <summary>
		/// The ID of the item being dropped.
		/// </summary>
		[WireMember(5)]
		public int ItemId { get; }

		/// <summary>
		/// The position of the item being dropped.
		/// </summary>
		[WireMember(6)]
		public Vector3<float> Position { get; }

		//Serializer ctor

		private Sub60DropInventoryItemCommand()
		{
			
		}
	}
}

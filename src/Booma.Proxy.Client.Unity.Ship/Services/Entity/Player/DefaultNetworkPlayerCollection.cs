﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Simple implementation of the <see cref="INetworkPlayerCollection"/>.
	/// </summary>
	public sealed class DefaultNetworkPlayerCollection : INetworkPlayerCollection, INetworkEntityRegistery<INetworkPlayer>
	{
		/// <inheritdoc />
		public INetworkPlayer Local => ManagedPlayerMap[SlotModel.SlotSelected];

		/// <inheritdoc />
		public IEnumerable<INetworkPlayer> Entities => this;

		/// <inheritdoc />
		public IEnumerable<INetworkPlayer> ExcludingLocal => Entities.Where(p => p.Identity.EntityId != SlotModel.SlotSelected);

		/// <inheritdoc />
		public INetworkPlayer this[int id] => ManagedPlayerMap[id];

		/// <summary>
		/// Internally managed player map that provided quick lookup for client id to player.
		/// </summary>
		private IDictionary<int, INetworkPlayer> ManagedPlayerMap { get; }

		/// <summary>
		/// The slot model that indicates the local player is associated with.
		/// </summary>
		private ICharacterSlotSelectedModel SlotModel { get; }

		public DefaultNetworkPlayerCollection(ICharacterSlotSelectedModel slotModel)
		{
			if(slotModel == null) throw new ArgumentNullException(nameof(slotModel));

			SlotModel = slotModel;
			//TODO: Do we need thread safety?
			ManagedPlayerMap = new Dictionary<int, INetworkPlayer>(15);
		}

		/// <inheritdoc />
		public bool ContainsId(int id)
		{
			return ManagedPlayerMap.ContainsKey(id);
		}

		/// <inheritdoc />
		public IEnumerator<INetworkPlayer> GetEnumerator()
		{
			return ManagedPlayerMap.Values.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <inheritdoc />
		public void AddEntity(int id, INetworkPlayer player)
		{
			if(ManagedPlayerMap.ContainsKey(id))
				throw new InvalidOperationException($"Tried to add player with Id: {id} but that id is already associated. Details: {player}");

			ManagedPlayerMap.Add(id, player);
		}

		/// <inheritdoc />
		public INetworkPlayer RemoveEntity(int id)
		{
			if(!ManagedPlayerMap.ContainsKey(id))
				return null;

			INetworkPlayer player = ManagedPlayerMap[id];

			ManagedPlayerMap.Remove(id);

			return player;
		}
	}
}

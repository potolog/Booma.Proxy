﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Just a simple referenceable Type for reflection purposes.
	/// </summary>
	public static class PacketGameStubMetadataMarker
	{
		public static IEnumerable<Type> SerializableTypes { get; } = typeof(PacketGameStubMetadataMarker)
			.Assembly
			.GetTypes()
			.Where(t => t.GetCustomAttribute(typeof(WireDataContractBaseLinkAttribute)) != null)
			.ToList();
	}
}

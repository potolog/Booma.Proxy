﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public class Vector3<T> : Vector2<T>
	{
		/// <summary>
		/// X value.
		/// </summary>
		[WireMember(1)]
		public T Z { get; }

		public Vector3(T x, T y, T z) 
			: base(x, y)
		{
			Z = z;
		}

		protected Vector3()
		{
			
		}
	}
}

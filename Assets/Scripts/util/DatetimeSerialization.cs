using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
	public static class DatetimeSerialization
	{
		public static long Serialize(this DateTime time)
		{
			return time.ToTimestamp();
		}

		public static DateTime DeSerialize(this long time)
		{
			return time.FromTimestamp();
		}

		public static long ToTimestamp(this DateTime time)
		{
			return new DateTimeOffset(time).ToUnixTimeSeconds();
		}
		public static DateTime FromTimestamp(this long timestamp)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(timestamp);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Logging;

namespace BrennanHatton.Networking.Events
{
	public class SendFullEventLog : MonoBehaviour
	{
		public ActionLogger logger;
		
		public void SendFullLog()
		{
			SendLoggingEventManager.SendFullLogEvent(logger.GetString());
		}
	}
}
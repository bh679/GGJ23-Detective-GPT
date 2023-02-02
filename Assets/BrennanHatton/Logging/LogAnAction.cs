using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Logging
{
	public class LogAnAction : MonoBehaviour
	{
		public LogAction log = new LogAction(false);
		
		public bool onStart;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(onStart)
			    Log();
	    }
	    
		public void Log()
		{
			if(this.enabled)
				ActionLogger.Instance.Add(log);
		}
	}
}
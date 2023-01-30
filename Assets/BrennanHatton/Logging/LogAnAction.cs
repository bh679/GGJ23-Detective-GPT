using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Logging
{
	public class LogAnAction : MonoBehaviour
	{
		public LogAction log;
		
		public bool onStart;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(onStart)
			    Log();
	    }
	    
		public void Log()
		{
			ActionLogger.Instance.Add(log);
		}
	}
}
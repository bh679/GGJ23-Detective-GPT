using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.Logging
{
	public class LogToText : MonoBehaviour
	{
		public TMP_Text text;
		int length = 0;
		
		void Reset()
		{
			text = this.GetComponent<TMP_Text>();
		}
		
	    // Update is called once per frame
	    void Update()
		{
			if(length != ActionLogger.Instance.actions.Count)
			{
				text.text = ActionLogger.Instance.output;
				length = ActionLogger.Instance.actions.Count;
			}
	    }
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.Logging
{

	/// <summary>
	/// A UnityEvent with a float as a parameter
	/// </summary>
	[System.Serializable]
	public class LogEvent : UnityEvent<LogAction> { }
	
	[System.Serializable]
	public enum Importance
	{
		Critical = 10,
		VeryImportant = 9,
		QuitImportant = 8,
		Important = 7,
		AlmostImportant = 6,
		Approaching = 5,
		SlightlyImportant = 4,
		NotVeryImportant = 3,
		Unimportant = 2,
		NotImportant = 1,
		NotAtAllImportant = 0,
	}
	
	[System.Serializable]
	public class LogAction
	{
		public const string defaultName = "Player";
		public string who = defaultName, did, what, with, when;
		public Importance importance = Importance.SlightlyImportant;
		
		public LogAction(float time)
		{
			//Debug.Log("time");
			when = time.ToString();//Time.time.ToString();
			when = when.Substring(0, when.LastIndexOf(".")+2);

		}
		public LogAction(bool withTime)
		{
			if(withTime)
			{
				when = Time.time.ToString();
				when = when.Substring(0, when.LastIndexOf(".")+2);
			}
		}
		
		public string GetString()
		{
			return "("+(int)importance +") "+ who + " " + did + " " + what + (with==""?"":" with " + with) + " at " + when;
		}
	}

	public class ActionLogger : MonoBehaviour
	{
		public List<LogAction> actions;
		public string output;
		
		//Singlton
		public static ActionLogger Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
		    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		public void Add(LogAction log)
		{
			actions.Add(log);
			output += log.GetString() + "\n";
		}
		
		public string GetString(bool reset = false)
		{
			string retval = output;
			
			if(reset)
				output = "";
			
			return retval;/*
			
			string returnVal = "";
			for(int i = 0; i < actions.Count; i++)
				returnVal += actions[i].GetString() + "\n then ";
			return returnVal;*/
		}
		
		public void Clear()
		{
			output = "";
			actions = new List<LogAction>();
		}
	}
}
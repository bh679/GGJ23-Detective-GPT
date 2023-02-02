using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DetectiveGPT;

namespace BrennanHatton.Logging
{
	[System.Serializable]
	public class LogGroups
	{
		public string name;
		public LogAnAction[] logActions = new LogAnAction[0];
		public GrabberLogger[] grabbers = new GrabberLogger[0];
		public RaycastWeaponLog[] weaponLogs = new RaycastWeaponLog[0];
		public PlayerMovementLogger[] movementLogs = new PlayerMovementLogger[0];
		public GameObject[] gameObjects = new GameObject[0];
		public QuetionData questionType;
		
		public void Enable(bool enabled)
		{
			for(int i =0 ;i < logActions.Length; i++)
				logActions[i].enabled = enabled;
			
			for(int i =0 ;i < grabbers.Length; i++)
				grabbers[i].enabled = enabled;
				
			for(int i =0 ;i < weaponLogs.Length; i++)
				weaponLogs[i].enabled = enabled;
				
			for(int i =0 ;i < movementLogs.Length; i++)
				movementLogs[i].enabled = enabled;
		
			for(int i =0 ;i < gameObjects.Length; i++)
				gameObjects[i].SetActive(enabled);
			
		}
		
	}
	
	public class LogManager : MonoBehaviour
	{
		public LogGroups[] logGroups = new LogGroups[1];
		
		
		
		void Reset()
		{
			logGroups = new LogGroups[4];;
			logGroups[0] = new LogGroups();
			logGroups[0].logActions = GameObject.FindObjectsOfType<LogAnAction>();
			logGroups[0].grabbers = GameObject.FindObjectsOfType<GrabberLogger>();
			logGroups[0].weaponLogs = GameObject.FindObjectsOfType<RaycastWeaponLog>();
			logGroups[0].movementLogs = GameObject.FindObjectsOfType<PlayerMovementLogger>();
			logGroups[0].questionType = QuetionData.General;
			logGroups[0].name = "General";
			
			logGroups[1] = new LogGroups();
			logGroups[1].questionType = QuetionData.Object;
			logGroups[1].name = "Object";
			logGroups[1].grabbers = logGroups[0].grabbers;
			logGroups[1].weaponLogs = logGroups[0].weaponLogs;
			
			logGroups[2] = new LogGroups();
			logGroups[2].questionType = QuetionData.Location;
			logGroups[2].name = "Location";
			List<LogAnAction> locationLogs = new List<LogAnAction>();
			List<GameObject> locationGameObjects = new List<GameObject>();
			for(int i  =0; i < logGroups[0].logActions.Length; i++)
			{
				if(logGroups[0].logActions[i].transform.parent.name.Contains("Location"))
				{
					locationLogs.Add(logGroups[0].logActions[i]);
					locationGameObjects.Add(logGroups[0].logActions[i].gameObject);
				}
				
			}
			logGroups[2].logActions = locationLogs.ToArray();
			logGroups[2].gameObjects = locationGameObjects.ToArray();
			logGroups[0].gameObjects = locationGameObjects.ToArray();
			
			logGroups[3] = new LogGroups();
			logGroups[3].questionType = QuetionData.Point;
			logGroups[3].name = "Point";
			
			
		}
		
		public void DisableAll()
		{
			for(int i = 0; i < logGroups.Length; i++)
				logGroups[i].Enable(false);
		}
		
		public void SetLoggersFromQuestion(QuetionData type)
		{
			logGroups[0].Enable(type == QuetionData.General);
			
			for(int i = 0; i < logGroups.Length; i++)
			{
				if(logGroups[i].questionType == type)
					logGroups[i].Enable(true);
			}
			
			
		}
	}
}
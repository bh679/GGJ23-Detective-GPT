using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using CognitiveServicesTTS;
using Microsoft.Unity;*/
using UnityEngine.Networking;

public class VoicePIcker : MonoBehaviour
{
	public SpeechManager manager;
	string path = "https://southeastasia.tts.speech.microsoft.com/cognitiveservices/voices/list";
	//https://YOUR_RESOURCE_REGION.tts.speech.microsoft.com/cognitiveservices/voices/list' \
	//--header ': '
	
	private void Start() {
		StartCoroutine(MakeRequests());
	}

	private IEnumerator MakeRequests() {
		/*Debug.Log("MAKING REQUEST");
		RequestType type = RequestType.GET;
		
		var request = new UnityWebRequest(path, type.ToString());

		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Ocp-Apim-Subscription-Key", manager.SpeechAPIKey.text);
		Debug.Log(manager.SpeechAPIKey.text);
		request.SetRequestHeader("Content-Type", "application/json");


		yield return request.Send();
		Debug.LogError(request.downloadHandler.text);*/
		//WWWForm form = new WWWForm();
		//form.AddField("Ocp-Apim-Subscription-Key", manager.SpeechAPIKey.text);

		using (UnityWebRequest www = UnityWebRequest.Get(path + "?Ocp-Apim-Subscription-Key="+manager.SpeechAPIKey.text))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				Debug.Log("Form upload complete!");
				Debug.Log(www.downloadHandler.text);
			}
		}
	}


	/*private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null) {
		var request = new UnityWebRequest(path, type.ToString());

		if (data != null) {
			var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
			request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		}

		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		return request;
	}

	private void AttachHeader(UnityWebRequest request,string key,string value)
	{
		request.SetRequestHeader(key, value);
	}*/
}
public enum RequestType {
	GET = 0,
	POST = 1,
	PUT = 2
}
	/*


	public class Todo {
		// Ensure no getters / setters
		// Typecase has to match exactly
		public int userId;
		public int id;
		public string title;
		public bool completed;
	}

	[Serializable]
	public class PostData {
		public string Hero;
		public int PowerLevel;
	}

	public class PostResult
	{
		public string success { get; set; }
	}*/


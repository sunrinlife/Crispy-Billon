using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

public class JsonTest : MonoBehaviour
{

	void Start()
	{
		StartCoroutine(enumerator());
	}

	IEnumerator enumerator()
	{
		string serverUrl = "https://unitaemin.run.goorm.io/hellfight/swagger.json";

		UnityWebRequest request = UnityWebRequest.Get(serverUrl);

		yield return request.SendWebRequest();

		if (request.isNetworkError)
		{
			Debug.Log(request.error);
		}
		else
		{
			Debug.Log(request.downloadHandler.text);
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
	public InputField id;
	public InputField password;
	public InputField email;
	public InputField username;
	public Text Message;

	public void TryRegister_Fun()
	{
		StartCoroutine(TryRegister());
	}

	IEnumerator TryRegister()
	{
		string serverUrl = "https://unitaemin.run.goorm.io/sunrinthon/auth/signup/";

		WWWForm form = new WWWForm();
		form.AddField("id", id.text);
		form.AddField("password", password.text);
		form.AddField("email", email.text);
		form.AddField("username", username.text);

		using (UnityWebRequest www = UnityWebRequest.Post(serverUrl, form))
		{
			yield return www.SendWebRequest();

			if (www.isNetworkError)
			{
				Debug.Log(www.error);
			}
			else if (www.isHttpError)
			{
				Debug.Log(www.downloadHandler.text);
				Message.text = JSON.Parse(www.downloadHandler.text)["mes"];
			}
			else
			{
				Debug.Log(www.downloadHandler.text);
				Message.text = JSON.Parse(www.downloadHandler.text)["mes"];

				id.text = "";
				password.text = "";
				email.text = "";
				username.text = "";
			}
		}
	}
}
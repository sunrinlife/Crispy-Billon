using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
	public InputField id;
	public InputField password;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			password.Select();
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			TryLogin_Fun();
		}
	}

	public void TryLogin_Fun()
	{
		StartCoroutine(TryLogin());
	}

	IEnumerator TryLogin()
	{
		string serverUrl = "https://unitaemin.run.goorm.io/sunrinthon/auth/signin/";

		WWWForm form = new WWWForm();
		form.AddField("id", id.text);
		form.AddField("password", password.text);

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
			}
			else
			{
				Debug.Log(www.downloadHandler.text);
				JSONNode received_data = JSON.Parse(www.downloadHandler.text);

				id.text = "";
				password.text = "";

				if (File.Exists("token"))
				{
					File.Create("token").Close();
				}
				StreamWriter write = new StreamWriter("token", false);
				write.WriteLine(received_data["accessToken"].ToString());
				write.Close();
			}
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
using Newtonsoft.Json;

public class TestObject : MonoBehaviour
{
	private Socket socket;

	void Start()
	{
		socket = IO.Socket("https://sunrinthonsocket.run.goorm.io:80");

		socket.On(Socket.EVENT_CONNECT, () =>
		{
			Debug.Log("Connected");
		});

		socket.On("SendClient", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString())["text"]);
		});
	}

	private void FixedUpdate()
	{
		var send_data = new Dictionary<string, string>
		{
			{"text","test" }
		};

		socket.Emit("SendServer", JsonConvert.SerializeObject(send_data));
		Debug.Log("Sended");
	}

	private void OnDestroy()
	{
		socket.Disconnect();
	}
}
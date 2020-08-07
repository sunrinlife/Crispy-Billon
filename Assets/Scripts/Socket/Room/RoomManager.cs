using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;
using UnityEngine.UI;
using Newtonsoft.Json;

public class RoomManager : MonoBehaviour
{
	private Socket socket;

	public InputField join_room_id;
	public InputField join_username;

	public InputField create_username;

	public InputField auto_username;

	private void Start()
	{
		socket = IO.Socket("http://192.168.0.219:51235/");

		socket.On(Socket.EVENT_CONNECT, () =>
		{
			Debug.Log("Connected");
		});

		socket.On("CreateR", (data) =>
		{
			 Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("CreateFail", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("GameStart", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("JoinFail", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("Matched", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});
	}

	public void RoomCreate()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("nickname", create_username.text);

		socket.Emit("Create", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));
	}

	public void RoomJoin()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("room_id", join_room_id.text);
		send_data.Add("nickname", join_username.text);

		socket.Emit("Join", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));
	}

	public void AutoMatching()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("nickname", auto_username.text);

		socket.Emit("Mathing", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));
	}
}

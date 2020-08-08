using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using SimpleJSON;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;
using UnityEditor;
using UnityEngine.Assertions.Must;

public class RoomManager : MonoBehaviour
{
	private Socket socket;

	public string my_nickname = "";

	public InputField join_room_id;
	public InputField join_username;

	public InputField create_username;

	public InputField auto_username;

	public GameObject matching;

	public Text create_username_text;

	private bool startgame = false;
	private bool first_start = false;
	private bool started = false;
	private bool roomcreated = false;

	JSONNode receive_data;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		socket = IO.Socket("https://speed.run.goorm.io/");

		socket.On(Socket.EVENT_CONNECT, () =>
		{
			Debug.Log("Connected");
		});

		socket.On("CreateR", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString())["room_id"]);
			receive_data = JSON.Parse(data.ToString());
			roomcreated = true;
		});

		socket.On("CreateFail", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("GameStart", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
			startgame = true;

			receive_data = JSON.Parse(data.ToString());
		});

		socket.On("JoinFail", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("Matched", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
			startgame = true;

			receive_data = JSON.Parse(data.ToString());
		});

		socket.On("CancelMatchingR", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());
		});

		socket.On("Receive", (data) =>
		{
			Debug.Log(JSON.Parse(data.ToString()).ToString());

			receive_data = JSON.Parse(data.ToString());
		});
	}

	float time = 0;

	private void Update()
	{
		if (started)
		{
			if (first_start) {
				GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().master = receive_data["player"][0]["master"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().master = receive_data["player"][1]["master"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().nickname = receive_data["player"][0]["nickname"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().nickname = receive_data["player"][1]["nickname"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().missed = receive_data["player"][0]["missed"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().missed = receive_data["player"][1]["missed"];
				GameObject.Find("GameManager").GetComponent<IngameManager>().room_id = receive_data["room_id"];
				first_start = false;
			}

			if (receive_data["alarm"] != null)
			{
				if (receive_data["alarm"]["spam"])
				{
					GameObject.Find("GameManager").GetComponent<IngameManager>().SpawnSpam();
				}
				else
				{
					GameObject.Find("GameManager").GetComponent<IngameManager>().SpawnAlram();
				}
			}

			if (GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().master == true)
			{
				try
				{
					Hashtable player1 = new Hashtable();
					player1.Add("name", GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().nickname);
					player1.Add("missed", GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().missed);
					Hashtable player2 = new Hashtable();
					player2.Add("name", GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().nickname);
					player2.Add("missed", GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().missed);

					List<Hashtable> players_list = new List<Hashtable>() { player1, player2 };

					Hashtable players = new Hashtable();
					players.Add("players", players_list);

					Hashtable alarm = new Hashtable();
					alarm.Add("spam", UnityEngine.Random.Range(0, 2) == 0 ? true : false);

					Hashtable send_data = new Hashtable();
					send_data.Add("room_id", GameObject.Find("GameManager").GetComponent<IngameManager>().room_id);
					send_data.Add("player", players);
					if (Time.time - time >= 0.8f)
					{
						send_data.Add("alarm", alarm);
						time = Time.time;
					}


					socket.Emit("Get", JsonConvert.SerializeObject(send_data));

					if(receive_data["player"][0]["missed"] || receive_data["player"][1]["missed"])
					{
						started = false;

					}
				}
				catch(Exception e)
				{
					Debug.Log(e);
				}
			}
		}
		if (startgame)
		{
			SceneManager.LoadScene(3);
			startgame = false;
			first_start = true;
			started = true;
		}
		if (roomcreated)
		{
				create_username_text.text = receive_data["room_id"];
				roomcreated = false;
		}
	}

	public void RoomCreate()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("nickname", create_username.text);
		my_nickname = create_username.text;

		socket.Emit("Create", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));
	}

	public void RoomJoin()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("room_id", join_room_id.text);
		send_data.Add("nickname", join_username.text);
		my_nickname = join_username.text;

		socket.Emit("Join", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));
	}

	public void AutoMatching()
	{
		Hashtable send_data = new Hashtable();
		send_data.Add("nickname", auto_username.text);
		my_nickname = auto_username.text;

		socket.Emit("Matching", JsonConvert.SerializeObject(send_data));
		Debug.Log(JsonConvert.SerializeObject(send_data));

		matching.SetActive(true);
	}

	public void CancelMatching()
	{
		matching.SetActive(false);
		socket.Emit("CancelMatching");
		Debug.Log("Matching Canceled");
	}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
	public float move_speed = -0.1f;

	public Text winner;

	private void Start()
	{
		winner = GameObject.Find("Text").GetComponent<Text>();
	}

	private void FixedUpdate()
	{
		transform.position += new Vector3(0, move_speed, 0);
	}

	public void Dis() 
	{
		Destroy(gameObject);
	}

	public void Cli()
	{
		if(GameObject.Find("GameManager").GetComponent<IngameManager>().player1.name == GameObject.Find("Manager").GetComponent<RoomManager>().my_nickname)
		{
			GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().missed = true;

			winner.text += GameObject.Find("GameManager").GetComponent<IngameManager>().player1.GetComponent<Player>().nickname;
		}
		else
		{
			GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().missed = true;

			winner.text += GameObject.Find("GameManager").GetComponent<IngameManager>().player2.GetComponent<Player>().nickname;
		}
		winner.text += "님이 승리하셨습니다!";
	}
}

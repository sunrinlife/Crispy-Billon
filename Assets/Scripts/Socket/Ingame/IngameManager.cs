using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
	public string room_id = "";
	
	public GameObject player1;
	public GameObject player2;

	public GameObject alarm;
	public GameObject spam;

	public void SpawnAlram()
	{
		GameObject tmp = Instantiate(alarm);
		tmp.transform.position = new Vector3(-10, tmp.transform.position.y, tmp.transform.position.z);

		tmp = Instantiate(alarm);
		tmp.transform.position = new Vector3(10, tmp.transform.position.y, tmp.transform.position.z);
	}

	public void SpawnSpam()
	{
		GameObject tmp = Instantiate(spam);
		tmp.transform.position = new Vector3(-10, tmp.transform.position.y, tmp.transform.position.z);

		tmp = Instantiate(spam);
		tmp.transform.position = new Vector3(10, tmp.transform.position.y, tmp.transform.position.z);
	}
}

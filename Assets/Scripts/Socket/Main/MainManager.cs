using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
	public GameObject msgbox;

	private void Start()
	{
		GameObject tmp = Instantiate(msgbox);

		tmp.transform.GetChild(0).GetComponent<Text>().text = "환영합니다!\n혼자서 플레이 하실건가요? 다른 친구와 함께 플레이 하실건가요 ?";
		tmp.transform.parent = GameObject.Find("Content").transform;
	}
}

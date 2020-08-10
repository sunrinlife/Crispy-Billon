using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
	public GameObject msgbox;

	public GameObject create_input;
	public GameObject join_input;
	public GameObject auto_input;
	public GameObject selectplay;
	public GameObject selectmult;

	private void Start()
	{
		GameObject tmp = Instantiate(msgbox);

		tmp.transform.parent = GameObject.Find("Content").transform;
		tmp.transform.localScale = new Vector3(1, 1, 1);
		tmp.transform.GetChild(0).GetComponent<Text>().text = "환영합니다!\n혼자서 플레이 하실건가요? \n다른 친구와 함께 플레이 하실건가요 ?";

		selectplay.SetActive(true);
	}

	public void Single_Fun()
	{
		StartCoroutine(Single());	
	}

	IEnumerator Single()
	{
		GameObject tmp = Instantiate(msgbox);

		tmp.transform.SetParent(GameObject.Find("Content").transform);
		tmp.transform.localScale = new Vector3(1, 1, 1);
		tmp.transform.GetChild(0).GetComponent<Text>().text = "게임을 시작합니다!";

		yield return new WaitForSeconds(2);
	}
}

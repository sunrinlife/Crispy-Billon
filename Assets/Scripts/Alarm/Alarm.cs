using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
	public float move_speed = -0.1f;

	private void FixedUpdate()
	{
		transform.position += new Vector3(0, move_speed, 0);
	}
}

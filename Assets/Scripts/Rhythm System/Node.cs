using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	public float down_speed = -0.1f;

	private void FixedUpdate()
	{
		transform.position += new Vector3(0, down_speed, 0);
	}
}

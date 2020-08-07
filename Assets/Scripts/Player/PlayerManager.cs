using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float speed = 10f;

    private void Update()
    {
        Vector3 Movement = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
     
        gameObject.transform.position += Movement * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // TODO: player hp decrease
            Debug.Log("enemy와 충돌함");
        }
    }
}

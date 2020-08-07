using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 10f;
    
    private void Start()
    {
    }

    private void Update()
    {
        Vector3 Movement = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
     
        gameObject.transform.position += Movement * (speed * Time.deltaTime);
    }
}

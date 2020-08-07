using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float HP;

    private float speed;
    private GameObject target;
    
    void Start()
    {
        HP = 100;
        speed = 1;
        target = GameObject.Find("player");
    }

    void Update()
    {
        var position = target.transform.position;
        Vector3 dir = position - gameObject.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, position, speed / 100f);

        // 타겟 방향으로 따라감
        //transform.position += dir * (speed * Time.deltaTime);

        // 타겟 방향으로 회전함
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HP -= 10;
        }
    }
}

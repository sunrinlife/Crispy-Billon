using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private GameObject target;
    private float speed;
    
    void Start()
    {
        target = GameObject.Find("player");
        speed = 1;
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
}

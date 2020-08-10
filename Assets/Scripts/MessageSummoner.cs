using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MessageSummoner : MonoBehaviour
{
    public GameObject plainMessage, spamMessage;

    private GameObject[] messages = new GameObject[2];
    private List<GameObject> spawned = new List<GameObject>();
    private Vector2 _summonPos = new Vector2(0, 2.15f);    // 메시지 생성 위치

    private float coolTime = 0.5f, leftTime = 2f;
    
    void Start()
    {
        messages[0] = plainMessage;
        messages[1] = spamMessage;
    }

    void Update()
    {
        if (leftTime > 0f)
        {
            leftTime -= Time.deltaTime;
        }else
        {
            // 쿨타임 끝남
            leftTime = coolTime;
            int messageIndex = Random.Range(0, 2);
            spawned.Add(messages[messageIndex]);
            Instantiate(spawned[spawned.Count - 1], _summonPos, spawned[spawned.Count - 1].transform.rotation);

            if (spawned.Count > 1)
                StartCoroutine(MessagesMove());
        }
    }

    private IEnumerator MessagesMove()
    {
        // -1.5f
        for (int i = 0; i < spawned.Count; i++)
        {
        }
        
        yield return null;
    }
}

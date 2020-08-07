using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public GameObject enemy;

    private Vector3 offset;
    
    private Canvas canvas;
    private Camera hpCamera;
    private RectTransform rectParent;
    private RectTransform rectHP;
    private Slider m_Slider;

    private EnemyManager enemyManager;

    void Start()
    {
        
        offset = new Vector3(0, 1, 0);
        canvas = GetComponentInParent<Canvas>();
        hpCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = GetComponent<RectTransform>();

        enemyManager = enemy.GetComponent<EnemyManager>();
        m_Slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        m_Slider.value = enemyManager.HP / 100f;
    }

    // HP바가 enemy를 쫓아가는 코드
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position + offset); // 적의 월드 3d좌표를 스크린좌표로 변환
       
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, hpCamera, out localPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환

        rectHP.localPosition = localPos; // 체력바 위치조정
    }
}

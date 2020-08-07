using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    public GameObject enemy;
    
    public Vector3 offset;
    
    private Canvas canvas;
    private Camera hpCamera;
    private RectTransform rectParent;
    private RectTransform rectHP;


    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        hpCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = GetComponent<RectTransform>();
        
        offset = new Vector3(0, 1, 0);
    }

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

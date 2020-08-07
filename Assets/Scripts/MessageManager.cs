using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum MsgState
{
    Plain, Spam
}

public class MessageManager : MonoBehaviour
{
    // 이 메시지가 일반 메시지인지 스팸 메시지인지 인스펙터 창에서 결정해줌.
    public MsgState msgState;

    public GameObject message;
    public Text NameText, ValueText;
    public Button confirmButton;

    private Camera _camera;
    
    public Vector3 nameTextOffset, valueTextOffset, confirmButtonOffset;

    private Canvas canvas;
    private Camera msgCamera;
    private RectTransform rectParent;
    private RectTransform rectNameText, rectValueText, rectConfirmButton;
    

    void Start()
    {
        _camera = Camera.main;

        nameTextOffset = new Vector3(-1.3f, 0.35f);
        valueTextOffset = new Vector3(-1.3f, -0.12f);
        confirmButtonOffset = new Vector3(2.5f, 0);
        
        canvas = GetComponent<Canvas>();
        msgCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        
        rectNameText = NameText.GetComponent<RectTransform>();
        rectValueText = ValueText.GetComponent<RectTransform>();
        rectConfirmButton = confirmButton.GetComponent<RectTransform>();
    }
    
    // UI가 message를 따라가는 코드
    private void LateUpdate()
    {
        var messagePos = message.transform.position;
        var screenNameTextPos = _camera.WorldToScreenPoint(messagePos + nameTextOffset); // 적의 월드 3d좌표를 스크린좌표로 변환
        var screenValueTextPos = _camera.WorldToScreenPoint(messagePos + valueTextOffset);
        var screenConfirmButtonPos = _camera.WorldToScreenPoint(messagePos + confirmButtonOffset);

        Vector2 nameTextPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenNameTextPos, msgCamera, out nameTextPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환
        rectNameText.localPosition = nameTextPos;
        
        Vector2 valueTextPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenValueTextPos, msgCamera, out valueTextPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환
        rectValueText.localPosition = valueTextPos;
        
        Vector2 confirmButtonPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenConfirmButtonPos, msgCamera, out confirmButtonPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환
        rectConfirmButton.localPosition = confirmButtonPos; // 체력바 위치조정

    }

    public void OnConfirmButtonClicked()
    {
        if (msgState == MsgState.Spam)
        {
            Debug.Log("스팸 클릭함");
        }else if (msgState == MsgState.Plain)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

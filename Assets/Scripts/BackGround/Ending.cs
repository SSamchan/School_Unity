using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public Text creditsText;
    public float scrollSpeed = 10.0f; // 크래딧 스크롤 속도
    private RectTransform creditsRect;
    private Vector3 startPos;
    private float contentHeight;

    private void Start()
    {
        creditsRect = creditsText.GetComponent<RectTransform>();
        startPos = creditsRect.position;
        contentHeight = creditsText.preferredHeight;
        ResetCreditsPosition();
    }

    private void Update()
    {
        ScrollCredits();
    }

    private void ResetCreditsPosition()
    {
        creditsRect.position = startPos;
    }

    private void ScrollCredits()
    {
        Vector3 pos = creditsRect.position;
        pos.y += scrollSpeed * Time.deltaTime;

        if (pos.y >= startPos.y + contentHeight)
        {
            // 크래딧이 화면을 벗어나면 다시 초기 위치로 이동
            ResetCreditsPosition();
        }
        else
        {
            creditsRect.position = pos;
        }
    }
}
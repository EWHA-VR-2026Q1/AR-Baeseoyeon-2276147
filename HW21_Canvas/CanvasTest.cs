using UnityEngine;
using TMPro; // TextMeshPro 지원

public class CanvasTest : MonoBehaviour
{
    // 3개 캔버스의 텍스트를 각각 담을 변수
    public TextMeshProUGUI targetTextOverlay; 
    public TextMeshProUGUI targetTextCamera;
    public TextMeshProUGUI targetTextWorld;

    // 1번 Overlay 버튼이 누를 함수
    public void OnOverlayButtonClick()
    {
        if (targetTextOverlay != null) targetTextOverlay.text = "Clicked!";
    }

    // 2번 Camera 버튼이 누를 함수
    public void OnCameraButtonClick()
    {
        if (targetTextCamera != null) targetTextCamera.text = "Clicked!";
    }

    // 3번 World 버튼이 누를 함수
    public void OnWorldButtonClick()
    {
        if (targetTextWorld != null) targetTextWorld.text = "Clicked!";
    }
}
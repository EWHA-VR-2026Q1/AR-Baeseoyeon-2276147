using UnityEngine;
using UnityEngine.InputSystem;

public class ARInteraction_NewInput2 : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 startPosition;   // Snap 위치
    private Vector3 targetPosition;  // Lerp 위치
    private float zDistance;

    [Header("고무줄 탄성 속도")]
    public float lerpSpeed = 4f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
      
    }

    private void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 screenPos = pointer.position.ReadValue();

        // 1. Tap
        if (pointer.press.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform) 
                {
                    isDragging = true;
                    
                    // [핵심 수정] 마우스로 공을 딱 건드린 '현재의 AR 타겟 위 위치'를 제자리로 기억합니다!
                    startPosition = transform.position; 
                    
                    zDistance = mainCamera.WorldToScreenPoint(transform.position).z;
                }
            }
        }

        // 2. Snap
        if (pointer.press.wasReleasedThisFrame)
        {
            if (isDragging) // 내가 드래그하고 있었을 때만 작동하도록 안전장치
            {
                isDragging = false;
                targetPosition = startPosition; 
            }
        }

        // 3. Drag
        if (isDragging)
        {
            Vector3 mousePoint = new Vector3(screenPos.x, screenPos.y, zDistance);
            targetPosition = mainCamera.ScreenToWorldPoint(mousePoint);
        }

        // 4. Lerp (드래그 중이거나, 손을 떼서 복귀할 때만 움직이도록 설정)
        if (isDragging || targetPosition != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
        }
    }
}
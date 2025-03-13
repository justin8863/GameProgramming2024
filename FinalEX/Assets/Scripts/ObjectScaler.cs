using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Vector3 scaleIncreasePerSecond = new Vector3(0.1f, 0.1f, 0.1f); // 초당 증가할 스케일
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f); // 최대 스케일

    private void Update()
    {
        // 현재 스케일을 가져옴
        Vector3 currentScale = transform.localScale;

        // 초당 증가량을 프레임 시간에 맞게 조정
        Vector3 newScale = currentScale + scaleIncreasePerSecond * Time.deltaTime;

        // 새로운 스케일이 최대 스케일을 초과하지 않도록 제한
        newScale = Vector3.Min(newScale, maxScale);

        // 오브젝트에 새로운 스케일 적용
        transform.localScale = newScale;

        // 최대 크기에 도달했으면 업데이트 중지
        if (transform.localScale == maxScale)
        {
            enabled = false; // 스크립트 비활성화
        }
    }
}

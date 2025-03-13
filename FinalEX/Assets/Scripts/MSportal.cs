using UnityEngine;

public class MSportal : MonoBehaviour
{
    // 포탈과 충돌 시 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 플레이어인지 확인
        if (other.CompareTag("Player"))
        {
            // 플레이어의 위치를 (0, 0, 12)로 이동
            other.transform.position = new Vector3(6, 1, 18);
        }
    }
}

using UnityEngine;

public class MSportal : MonoBehaviour
{
    // ��Ż�� �浹 �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� ��ġ�� (0, 0, 12)�� �̵�
            other.transform.position = new Vector3(6, 1, 18);
        }
    }
}

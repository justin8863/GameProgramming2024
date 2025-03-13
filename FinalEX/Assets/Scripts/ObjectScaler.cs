using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField] private Vector3 scaleIncreasePerSecond = new Vector3(0.1f, 0.1f, 0.1f); // �ʴ� ������ ������
    [SerializeField] private Vector3 maxScale = new Vector3(3f, 3f, 3f); // �ִ� ������

    private void Update()
    {
        // ���� �������� ������
        Vector3 currentScale = transform.localScale;

        // �ʴ� �������� ������ �ð��� �°� ����
        Vector3 newScale = currentScale + scaleIncreasePerSecond * Time.deltaTime;

        // ���ο� �������� �ִ� �������� �ʰ����� �ʵ��� ����
        newScale = Vector3.Min(newScale, maxScale);

        // ������Ʈ�� ���ο� ������ ����
        transform.localScale = newScale;

        // �ִ� ũ�⿡ ���������� ������Ʈ ����
        if (transform.localScale == maxScale)
        {
            enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
        }
    }
}

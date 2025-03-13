using System;
using UnityEngine;
using System.Collections.Generic;

public class Superliminal : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // The target object we picked up for scaling

    [Header("Parameters")]
    public LayerMask targetMask;        // The layer mask used to hit only potential targets with a raycast
    public LayerMask ignoreTargetMask;  // The layer mask used to ignore the player and target objects while raycasting
    public float offsetFactor;          // The offset amount for positioning the object so it doesn't clip into walls

    float originalDistance;             // The original distance between the player camera and the target
    float originalScale;                // The original scale of the target objects prior to being resized
    Vector3 targetScale;                // The scale we want our object to be set to each frame


    // ���� ������Ʈ�� ������ ����Ʈ
    public List<Transform> usedObjects = new List<Transform>();

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleInput();
        ResizeTarget();
        RotateTarget();
    }

    void HandleInput()
    {
        // ���콺 ���� Ŭ�� üũ
        if (Input.GetMouseButtonDown(0))
        {
            // ���� Ÿ���� ���� ���
            if (target == null)
            {
                // ����ĳ��Ʈ�� �߻��Ͽ� Ÿ���� ã��
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // ��Ʈ�� ������Ʈ�� �̹� ���� ������Ʈ ����Ʈ�� �ִ��� Ȯ��
                    if (usedObjects.Contains(hit.transform))
                    {
                        // �̹� ���� ������Ʈ��� �������� ����
                        Debug.Log("�̹� ���� ������Ʈ�Դϴ�.");
                        return;
                    }

                    // Ÿ�� ����
                    target = hit.transform;

                    // ���� ��Ȱ��ȭ
                    Rigidbody rb = target.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    // ī�޶�� Ÿ�� ���� �Ÿ� ���
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // Ÿ���� ���� ������ ����
                    originalScale = target.localScale.x;

                    // Ÿ�� ������ ����
                    targetScale = target.localScale;
                }
            }
            else
            {
                // Ÿ���� ���� ��
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }

                // ���� ������Ʈ ����Ʈ�� �߰�
                usedObjects.Add(target);

                // Ÿ�� ����
                target = null;
            }
        }
    }

    void ResizeTarget()
    {
        if (target == null)
        {
            return;
        }

        // ����ĳ��Ʈ�� �߻��Ͽ� ������ �浹 ������ ã��
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            // ���ο� ��ġ ����
            target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

            // ���� �Ÿ� ���
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // �Ÿ� ���� ���
            float s = currentDistance / originalDistance;

            // ������ ���� ����
            targetScale.x = targetScale.y = targetScale.z = s;

            // Ÿ�� ������ ����
            target.localScale = targetScale * originalScale;
        }
    }

    void RotateTarget()
    {
        if (target == null)
        {
            return;
        }

        // ȸ�� �ӵ� ����
        float rotationSpeed = 50f;

        // 'Q' Ű�� ������ �ð���� ȸ��
        if (Input.GetKey(KeyCode.Q))
        {
            target.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        // 'E' Ű�� ������ �ݽð���� ȸ��
        if (Input.GetKey(KeyCode.E))
        {
            target.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}

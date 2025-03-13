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


    // 사용된 오브젝트를 저장할 리스트
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
        // 마우스 왼쪽 클릭 체크
        if (Input.GetMouseButtonDown(0))
        {
            // 현재 타겟이 없는 경우
            if (target == null)
            {
                // 레이캐스트를 발사하여 타겟을 찾음
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // 히트된 오브젝트가 이미 사용된 오브젝트 리스트에 있는지 확인
                    if (usedObjects.Contains(hit.transform))
                    {
                        // 이미 사용된 오브젝트라면 선택하지 않음
                        Debug.Log("이미 사용된 오브젝트입니다.");
                        return;
                    }

                    // 타겟 설정
                    target = hit.transform;

                    // 물리 비활성화
                    Rigidbody rb = target.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }

                    // 카메라와 타겟 간의 거리 계산
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // 타겟의 원래 스케일 저장
                    originalScale = target.localScale.x;

                    // 타겟 스케일 설정
                    targetScale = target.localScale;
                }
            }
            else
            {
                // 타겟을 놓을 때
                Rigidbody rb = target.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }

                // 사용된 오브젝트 리스트에 추가
                usedObjects.Add(target);

                // 타겟 해제
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

        // 레이캐스트를 발사하여 벽과의 충돌 지점을 찾음
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            // 새로운 위치 설정
            target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

            // 현재 거리 계산
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // 거리 비율 계산
            float s = currentDistance / originalDistance;

            // 스케일 비율 설정
            targetScale.x = targetScale.y = targetScale.z = s;

            // 타겟 스케일 적용
            target.localScale = targetScale * originalScale;
        }
    }

    void RotateTarget()
    {
        if (target == null)
        {
            return;
        }

        // 회전 속도 설정
        float rotationSpeed = 50f;

        // 'Q' 키를 누르면 시계방향 회전
        if (Input.GetKey(KeyCode.Q))
        {
            target.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        // 'E' 키를 누르면 반시계방향 회전
        if (Input.GetKey(KeyCode.E))
        {
            target.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}

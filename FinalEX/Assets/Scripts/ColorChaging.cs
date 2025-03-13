using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChaging : MonoBehaviour
{
    //public Material postProcessingMaterial; // 연결할 매터리얼
    //public float colorChangeSpeed = 1.0f; // 색상 변경 속도

    //private Color startColor = Color.red;    // 시작 색상 (빨강)
    //private Color endColor = new Color(0.5f, 0f, 0.5f, 1f); // 종료 색상 (보라)
    //private float t = 0.0f;
    //private bool isIncreasing = true;

    //void Update()
    //{
    //    if (postProcessingMaterial == null) return;

    //    // `t` 값 계산 (0에서 1 사이 반복)
    //    t += (isIncreasing ? 1 : -1) * Time.deltaTime * colorChangeSpeed;

    //    if (t > 1.0f)
    //    {
    //        t = 1.0f;
    //        isIncreasing = false; // 방향 전환
    //    }
    //    else if (t < 0.0f)
    //    {
    //        t = 0.0f;
    //        isIncreasing = true; // 방향 전환
    //    }

    //    // 색상 선형 보간 (빨간색 -> 보라색)
    //    Color currentColor = Color.Lerp(startColor, endColor, t);

    //    // 매터리얼의 ForegroundColor 설정
    //    postProcessingMaterial.SetColor("_ForeCol", currentColor);
    //}
    public Material postProcessingMaterial; // 연결할 매터리얼
    public float colorChangeSpeed = 1.0f; // 색상 변경 속도

    private float r = 0, g = 0, b = 0; // RGB 값
    private int phase = 0; // 현재 단계 (0~5)
    private bool isIncreasing = true; // 값이 증가하는 중인지 여부

    void Update()
    {
        if (postProcessingMaterial == null) return;

        float changeAmount = Time.deltaTime * colorChangeSpeed * 255;

        // 단계에 따라 RGB 값을 변경
        switch (phase)
        {
            case 0: // R 증가
                r += changeAmount;
                if (r >= 255)
                {
                    r = 255;
                    phase++;
                }
                break;

            case 1: // G 증가
                g += changeAmount;
                if (g >= 255)
                {
                    g = 255;
                    phase++;
                }
                break;

            case 2: // B 증가
                b += changeAmount;
                if (b >= 255)
                {
                    b = 255;
                    phase++;
                    isIncreasing = false; // 감소로 전환
                }
                break;

            case 3: // R 감소
                r -= changeAmount;
                if (r <= 0)
                {
                    r = 0;
                    phase++;
                }
                break;

            case 4: // G 감소
                g -= changeAmount;
                if (g <= 0)
                {
                    g = 0;
                    phase++;
                }
                break;

            case 5: // B 감소
                b -= changeAmount;
                if (b <= 0)
                {
                    b = 0;
                    phase = 0; // 다시 R 증가로 전환
                    isIncreasing = true;
                }
                break;
        }

        // 매터리얼의 색상 변경
        postProcessingMaterial.SetColor("_ForeCol", new Color(r / 255f, g / 255f, b / 255f, 1));
    }
}

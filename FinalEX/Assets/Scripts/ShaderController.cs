using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShaderController : MonoBehaviour
{
    public MonoBehaviour postProcessingScript; // PostProcessing 스크립트 제어
    public Material targetMaterial;           // Shader에 연결된 Material
    public Image targetImage;                 // 이미지를 변경할 UI Image
    public Image gameoverImage;               // gameover 이미지
    public Sprite[] sprites;                  // 색상에 대응하는 Sprite 배열

    public float fadeDuration = 1f;

    private float timer = 0.0f;            //timer
    private bool isFadingIn = false;

    private Color[] colors = new Color[]      // 색상 배열
    {
        Color.white,
        Color.yellow,
        Color.green,
        Color.blue,
        Color.red,
        Color.black,
    };

    private int colorIndex = 0;               // 현재 색상 인덱스
    private bool isFirstActive = false;       // 첫 번째 활성화 여부

    private float keyPressTime = 0.0f;        // 키 누른 시간
    private int nextChangeThreshold = 3;     // 색상 전환 임계값 (초)

    void Start()
    {
        Color color = gameoverImage.color;
        color.a = 0;
        gameoverImage.color = color;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            keyPressTime += Time.deltaTime; // T키 누르는 시간 계산

            CheckAndChangeColor();
        }

        if (isFadingIn)
        {
            timer += Time.deltaTime;
            if (timer >= 2f) // 2초 후 씬 전환
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName); // 현재 씬 로드
            }
        }
    }

    void ActivateShader()
    {
        postProcessingScript.enabled = true; // PostProcessing 스크립트 활성화

        // 첫 번째 활성화 시 초기 색상과 이미지 설정
        targetMaterial.SetColor("_ForeCol", colors[0]);
        targetImage.sprite = sprites[0];


    }

    private System.Collections.IEnumerator FadeInCoroutine() //페이드인 
    {
        float elapsedTime = 0f;
        Color color = gameoverImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            gameoverImage.color = color;
            yield return null;
        }

        // 페이드인 완료 후 알파 값 1로 설정
        color.a = 1;
        gameoverImage.color = color;

        isFadingIn = true;
    }

    void CheckAndChangeColor()
    {
        if (colors.Length == 0 || sprites.Length == 0) return; // 배열이 비어있으면 반환

        // 누적 시간 초과 시 색상과 이미지 변경
        if (keyPressTime >= nextChangeThreshold)
        {
            if (!isFirstActive)
            {
                // 첫 번째 Shader 활성화
                ActivateShader();
                isFirstActive = true;
            }
            else
            {
                // 색상과 이미지 인덱스 변경
                if (colorIndex + 1 < colors.Length)
                {
                    colorIndex += 1;

                    // Material 색상 변경
                    targetMaterial.SetColor("_ForeCol", colors[colorIndex]);

                    // Image의 Sprite 변경
                    if (colorIndex < sprites.Length)
                    {
                        targetImage.sprite = sprites[colorIndex];
                    }

                    Debug.Log($"Shader Color Changed to: {colors[colorIndex]} and Image Changed to: {sprites[colorIndex].name}");
                }
                if (colorIndex == 5)
                {
                    StartCoroutine(FadeInCoroutine());  //페이드인 추가

                }
            }

            keyPressTime = 0.0f; // 누적 시간 초기화
        }
    }

}

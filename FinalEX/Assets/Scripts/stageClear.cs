using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageClear : MonoBehaviour
{
    public Image gameclearImage;
    public GameObject backToTitle;
    public float fadeDuration = 1f;
    private bool isFadingIn = false;

    void Start()
    {
        Color color = gameclearImage.color;
        color.a = 0;
        gameclearImage.color = color;

        backToTitle.gameObject.SetActive(false);
    }
    private System.Collections.IEnumerator FadeInCoroutine() //페이드인 
    {
        float elapsedTime = 0f;
        Color color = gameclearImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            gameclearImage.color = color;
            yield return null;
        }

        // 페이드인 완료 후 알파 값 1로 설정
        color.a = 1;
        gameclearImage.color = color;

        backToTitle.gameObject.SetActive(true);
        Cursor.visible = true;

        isFadingIn = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeInCoroutine());  //페이드인 추가
            
        }
    }
    public void GoToEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}

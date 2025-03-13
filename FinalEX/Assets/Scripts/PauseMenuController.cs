using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;         // Pause 메뉴 UI (비활성화 상태로 시작)
    public Slider audioSlider;           // 오디오 볼륨 조절 슬라이더
    private bool isPaused = false;       // 현재 게임이 일시정지 상태인지 확인

    void Start()
    {
        // 초기 상태: 메뉴 비활성화
        pauseMenu.SetActive(false);

        // 슬라이더 초기값을 현재 오디오 볼륨으로 설정
        if (audioSlider != null)
        {
            audioSlider.value = AudioListener.volume;
        }
    }

    void Update()
    {
        // ESC 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ResumeGame();

            }
            else
            {
                PauseGame();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);      // 메뉴 활성화
        Time.timeScale = 0;            // 게임 시간 멈춤
        AudioListener.pause = true;    // 오디오도 일시정지
    }

    void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);    // 메뉴 비활성화
        Time.timeScale = 1;            // 게임 시간 정상화
        AudioListener.pause = false;   // 오디오 재개
    }

    public void OnAudioSliderChanged()
    {
        // 슬라이더 값으로 오디오 볼륨 조정
        AudioListener.volume = audioSlider.value;
    }

    public void OnSaveButtonPressed()
    {
        Debug.Log("Save functionality will be implemented later.");
        // 이후 저장 기능 구현
    }

    public void BackToTitle()
    {
        Time.timeScale = 1;             // 게임 시간 정상화 (중요)
        SceneManager.LoadScene("start"); // Start 씬으로 이동
    }
}

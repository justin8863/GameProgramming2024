using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    public void BackToTitle()
    {
        Time.timeScale = 1;             // 게임 시간 정상화 (중요)
        SceneManager.LoadScene("start"); // Start 씬으로 이동
    }
}

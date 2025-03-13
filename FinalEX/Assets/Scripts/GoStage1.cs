using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoStage1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToStage1()
    {
        SceneManager.LoadScene("stage 1");
    }
}

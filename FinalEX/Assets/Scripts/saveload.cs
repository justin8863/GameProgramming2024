using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveload : MonoBehaviour
{
    public TextAsset dataFile;

    public void LoadStage()
    {
        if (dataFile != null)
        {
            string stageData = dataFile.text.Trim();
            int stageNumber;
            if (int.TryParse(stageData, out stageNumber))
            {
                string sceneName = "stage " + stageNumber;
                Debug.Log("Loading Scene: " + sceneName);

                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("Invalid stage data in file.");
            }
        }
        else
        {
            Debug.LogError("Data file is not assigned in the inspector.");
        }
    }
}

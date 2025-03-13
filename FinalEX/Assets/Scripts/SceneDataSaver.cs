using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataSaver : MonoBehaviour
{
    public TextAsset dataFile;

    private void Start()
    {
        if (dataFile != null)
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;

            string filePath = Path.Combine(Application.persistentDataPath, dataFile.name);

            File.WriteAllText(filePath, sceneNumber.ToString());
            Debug.Log("Scene " + sceneNumber + " saved to " + filePath);
        }
        else
        {
            Debug.LogError("Data file not assigned in the inspector.");
        }
    }
}

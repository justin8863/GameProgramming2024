using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject door;

    // Ʈ���� �浹 ���� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (door != null)
            {
                Destroy(door);
            }
            SceneManager.LoadScene("stage 2");
        }
    }
}

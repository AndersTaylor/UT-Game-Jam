using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public void Awake()
    {
       Destroy(FindFirstObjectByType<GameManager>().gameObject);
       Destroy(FindFirstObjectByType<AudioManager>().gameObject);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score;
    
    public void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
        
        score = ScoreManager.Instance.totalScore;
        scoreText.text = "Score: " + score;
        
        Destroy(FindFirstObjectByType<GameManager>().gameObject);
        Destroy(FindFirstObjectByType<AudioManager>().gameObject);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

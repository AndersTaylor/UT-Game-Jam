using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Events")] public GameEvent onScoreChanged;
    
    // Set Singleton
    private static ScoreManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] private int totalScore = 0;
    public TMP_Text scoreText;

    void Update()
    {
        // test input
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeScore(30);
        }
    }

    public void ChangeScore(int scoreToAdd)
    {
        onScoreChanged.Raise(this, totalScore);
        totalScore += scoreToAdd;
        scoreText.text = totalScore.ToString();
    }
}
using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
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

    [Header("Events")] public GameEvent onScoreChanged, onMultChanged;

    public TMP_Text scoreText;
    public TMP_Text multText;
    public int totalScore = 0;
    public float totalMult = 1f;

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
        totalScore += scoreToAdd;
        scoreText.text = totalScore.ToString();
        
        onScoreChanged.Raise(this, totalScore);
    }

    public void ChangeMult(float multToAdd)
    {
        totalMult += multToAdd;
        multText.text = totalMult.ToString();
        
        onMultChanged.Raise(this, totalMult);
    }
}
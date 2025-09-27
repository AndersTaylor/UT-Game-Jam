using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Set Singleton
    private static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
    }
    
    [Header("Events")] public GameEvent onLoopCompleted, onLifeLost, onGameOver;
    
    public int lives;
    public int shields;
    public float timeRemaining;
    public int loopsCompleted;

    private ScoreManager scoreManager;
    private void Initialize()
    {
        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
        }
    }

    private void Update()
    {
        // test input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CompleteLoop();
        }
    }

    public void CompleteLoop()
    {
        int scoreToAdd = Mathf.RoundToInt(timeRemaining * scoreManager.totalMult);
        scoreManager.ChangeScore(scoreToAdd);
        loopsCompleted++;
        
        onLoopCompleted.Raise(this, null);
    }

    // Called when game manager hears mini game failed event raised
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
            return;
        }
        
        onLifeLost.Raise(this, lives);
    }

    public void GameOver()
    {
        onGameOver.Raise(this, null);
    }
}

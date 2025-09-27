using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Events")] 
    public GameEvent onLoopCompleted;
    public GameEvent onLifeLost;
    public GameEvent onShieldLost; 
    public GameEvent onGameOver;
    
    [Header("Other")]
    
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

    public void CompleteLoop()
    {
        int scoreToAdd = Mathf.RoundToInt(timeRemaining * scoreManager.totalMult);
        scoreManager.ChangeScore(scoreToAdd);
        loopsCompleted++;
        
        onLoopCompleted.Raise(this, null);
    }
    
    // Called when game manager hears MiniGameFailed event raised
    public void MiniGameFailed()
    {
        if (shields > 0)
        {
            shields--;
            onShieldLost.Raise(this, shields);
            return;
        }
        
        lives--;
        onLifeLost.Raise(this, lives);
        
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        onGameOver.Raise(this, null);
        SceneManager.LoadScene("GameOver");
    }
}

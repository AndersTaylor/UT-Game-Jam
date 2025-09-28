using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    [Header("Other")]
    
    public int lives;
    public int shields;
    public float timeRemaining;
    public int loopsCompleted;
    public GameObject clickParticles;


    private void OnEnable()
    {
        MiniGameEventBus.OnMiniGameComplete += MiniGameCompleted;
        MiniGameEventBus.onLoopCompleted += CompleteLoop;
    }
    void OnDisable()
    {
        MiniGameEventBus.OnMiniGameComplete -= MiniGameCompleted;
        MiniGameEventBus.onLoopCompleted -= CompleteLoop;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickJuice();
        }
    }

    private void ClickJuice()
    {
        //click.Play();
        //click.pitch = Random.Range(1.3f, 2f);
    }

    public void CompleteLoop()
    {
        int scoreToAdd = Mathf.RoundToInt(timeRemaining * ScoreManager.Instance.totalMult);
        ScoreManager.Instance.ChangeScore(scoreToAdd);
        loopsCompleted++;
        
    }

    public void MiniGameCompleted(MiniGameEventBus.Result result)
    {
        if (result.success)
        {
            MiniGameTimer.Instance.ResetTimer(MiniGameManager.Instance.timeToCompleteGame());
            MiniGameManager.Instance.OnMiniGameComplete();
            Debug.Log( "Game Complete" + result.miniGameName + " Score Gained: " + result.scoreGained);

            if (result.scoreGained != 0)
            {
                MiniGameTimer.Instance.shouldDecrementTimer = false;
                ScoreManager.Instance.ChangeScore(result.scoreGained);
            }
        }
        else
        {
            MiniGameManager.Instance.OnMiniGameComplete();
            ScoreManager.Instance.ChangeScore(0);
            
            if (shields > 0)
            {
                shields--;
            }
            else
            {
                lives--;
                ScoreManager.Instance.onMultiplierLost();
                Debug.Log("lives: " + lives);
                Debug.Log( "Game failed: " + result.miniGameName);

                if (lives <= 0)
                {
                    GameOver();
                }
            }
        }
    }
    
    public void addShield()
    {
        shields++;
    }

    private void LoopManager()
    {
        int gamesThisLoop = 3 * loopsCompleted;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    
}

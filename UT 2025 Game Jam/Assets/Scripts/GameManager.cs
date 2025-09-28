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
    
    public int lives = 3;
    public int shields;
    public float timeRemaining;
    public int loopsCompleted;
    public GameObject clickParticles;
    
    private AudioSource click;
    private ScoreManager scoreManager;
    private void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
            click = GetComponent<AudioSource>();
        }
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
        click.Play();
        click.pitch = Random.Range(1.3f, 2f);
    }

    public void CompleteLoop()
    {
        int scoreToAdd = Mathf.RoundToInt(timeRemaining * scoreManager.totalMult);
        scoreManager.ChangeScore(scoreToAdd);
        loopsCompleted++;
    }
    
    public void MiniGameFailed()
    {
        if (shields > 0)
        {
            shields--;
        }
        else
        {
            lives--;
            Debug.Log("lives: " + lives);
        
            if (lives <= 0)
            {
                GameOver();
            }
        }
        
        MiniGameManager.Instance.OnMiniGameComplete();
    }

    public void MiniGameSuccess()
    {
        
        MiniGameManager.Instance.OnMiniGameComplete();
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}

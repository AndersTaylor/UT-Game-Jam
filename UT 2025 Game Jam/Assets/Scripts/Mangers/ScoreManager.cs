using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
     // Set Singleton
    public static ScoreManager Instance { get; private set; }
   
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public TMP_Text scoreText;
    public TMP_Text multText;
    public int totalScore = 0;
    public float totalMult = 1f;
    
    private AudioSource audioSource;
    private bool shouldIncrementScore;
    private int tempScore;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (shouldIncrementScore)
        {
            IncrementScore();
        }
    }

    public void ChangeScore(int scoreToAdd)
    {
        tempScore = totalScore;
        totalScore += scoreToAdd;
        shouldIncrementScore = true;
        
        scoreText.gameObject.SetActive(true);
    }

    private void IncrementScore()
    {
        if (tempScore < totalScore)
        {
            tempScore++;
            scoreText.text = "Chrono Stabilization: " + tempScore;

            audioSource.pitch = Random.Range(1f, 1.5f);
            audioSource.Play();
            
        }
        else
        {
            shouldIncrementScore = false;
            scoreText.gameObject.SetActive(false);
        }
    }

    public void ChangeMult(float multToAdd)
    {
        totalMult += multToAdd;
        multText.text = totalMult.ToString();
    }
}
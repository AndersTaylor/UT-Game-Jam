using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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
        // test input
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeScore(30);
        }

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
    }

    private void IncrementScore()
    {
        if (tempScore < totalScore)
        {
            tempScore++;
            scoreText.text = tempScore.ToString();

            audioSource.pitch = Random.Range(1f, 1.5f);
            audioSource.Play();
            
        }
        else
        {
            shouldIncrementScore = false;
        }
    }

    public void ChangeMult(float multToAdd)
    {
        totalMult += multToAdd;
        multText.text = totalMult.ToString();
    }
}
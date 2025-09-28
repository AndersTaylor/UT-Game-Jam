using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
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

    [SerializeField] private int streak = 0;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float basePoints = 1f;

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

    public void addMiniGameWonMultiplier()
    {
        streak++;
        if (streak < 3)
            multiplier = 1f;
        else
            multiplier = math.min(5f, 2f + 0.5f * (streak - 3));
        
        if(multiplier == 2f || multiplier == 3f || multiplier == 4f || multiplier == 5f)
        {
            GameManager.Instance.addShield();

            //play sound
            audioSource.pitch = Random.Range(0.7f, 1.2f);
            audioSource.Play();
        }

        int gained = (int)Math.Round(basePoints * multiplier);
        ChangeMult(gained);
        
    }

    void OnMultiplierLost()
    { 
        streak = 0;
        multiplier = 1f;
        ChangeMult(-totalMult + 1f);
    }
}
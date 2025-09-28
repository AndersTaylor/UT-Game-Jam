using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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

    [SerializeField] private int streak = 0;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float basepoint = 1f;
    

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
        Debug.Log("Score: " + totalScore);
        addMiniGameWinMultiplier();
        Debug.Log("Multiplier: " + multiplier + " Streak: " + streak);
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
            StartCoroutine(WaitThenLoad(3));
        }
    }

    public void addMiniGameWinMultiplier()
    {

        streak++;
        if (streak >= 3)
            multiplier = 1f;
        else
            multiplier = math.min(5f, 2f + 0.5f * (streak - 3));

        if (multiplier == 2f || multiplier == 3f || multiplier == 4f || multiplier == 5f)
            GameManager.Instance.addShield();

        int MultGained = Mathf.RoundToInt(basepoint * multiplier);
        ChangeMult(MultGained);
    }
    
    public void onMultiplierLost()
    {
        streak = 0;
        multiplier = 1f;
        ChangeMult(-totalMult + 1f); 
    }
    
    public void addMult(float multToAdd)
    {
        totalMult += multToAdd;
        multText.text = totalMult.ToString();
    } 

    public void ChangeMult(float multToAdd)
    {
        totalMult += multToAdd;
        multText.text =  "Streak Multiplier: " + totalMult;
    }

    private IEnumerator WaitThenLoad(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        scoreText.gameObject.SetActive(false);

        MiniGameTimer.Instance.shouldDecrementTimer = true;
        MiniGameManager.Instance.StartNextMiniGame();
    }
}
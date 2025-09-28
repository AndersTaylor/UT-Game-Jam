using System;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    public static MiniGameTimer Instance { get; private set; }
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
    
    public GameObject timerObject;

    public float totalTime;
    public bool shouldDecrementTimer = true;
    public float timeRemaining;

    private void Start() 
    {
        timeRemaining = totalTime;
    }

    private void Update()
    {
        if (shouldDecrementTimer)
            DecrementTimer();
    }

    public void ResetTimer(float newTotalTime)
    {
        totalTime = newTotalTime;
        timeRemaining = totalTime;
        
        Debug.Log("time remaining" + timeRemaining);
    }

    private void DecrementTimer()
    {
        timeRemaining -= Time.deltaTime;

        var a = timeRemaining / totalTime;
        var newRot = Vector3.Lerp(new Vector3(0, 0, -5), new Vector3(0, 0, 95), a);

        timerObject.transform.rotation = Quaternion.Euler(newRot);

        if (timeRemaining <= 0)
        {
            ResetTimer(MiniGameManager.Instance.timeToCompleteGame());
            
            shouldDecrementTimer = false;
            
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Timer", false));
        }
    }
}

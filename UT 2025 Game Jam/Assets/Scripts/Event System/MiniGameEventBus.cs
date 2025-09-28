using UnityEngine;
using System;

public static class MiniGameEventBus 
{
    public struct Result
    {
        public string miniGameName;
        public bool success;
        public int scoreGained;

        public Result(string name, bool success, int score = 0)
        {
            this.miniGameName = name;
            this.success = success;
            scoreGained = score;
        }

    }

    public static event Action<Result> OnMiniGameComplete;

    public static void RaiseOnMiniGameComplete(Result result)
    => OnMiniGameComplete?.Invoke(result);

    public static event Action onLoopCompleted;
    public static void RaiseOnLoopCompleted()
    => onLoopCompleted?.Invoke();

    
}

using UnityEngine;

/// <summary>
/// Example class demonstrating how to subscribe to the game completion event
/// Other systems can use this pattern to respond to game completion
/// </summary>
public class GameCompletionSubscriber : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool enableDebugLogs = true;
    
    private void OnEnable()
    {
        // Subscribe to the game completion event
       // MiniGameEventBus.OnMiniGameComplete += OnGameCompleted;
    }
    
    private void OnDisable()
    {
        // Unsubscribe from the event to prevent memory leaks
       // MiniGameEventBus.OnMiniGameComplete -= OnGameCompleted;
    }
    
    /// <summary>
    /// This method will be called when the game is completed
    /// Add your custom logic here for what should happen when the game ends
    /// </summary>
    private void OnGameCompleted()
    {
        if (enableDebugLogs)
        {
            Debug.Log("Game completed! Subscriber received notification.");
        }
        
        // Example actions that could be performed:
        // - Save high scores
        // - Update statistics
        // - Show completion UI
        // - Play completion sound effects
        // - Send analytics data
        // - Unlock achievements
        
        // Add your custom game completion logic here
        HandleGameCompletion();
    }
    
    /// <summary>
    /// Custom method to handle game completion logic
    /// Override this or modify as needed for your specific system
    /// </summary>
    private void HandleGameCompletion()
    {
        // Example: Save final score
        // Example: Update player statistics
        // Example: Show completion screen
        // Example: Play victory sound
        
        Debug.Log("Handling game completion in subscriber...");
    }
}

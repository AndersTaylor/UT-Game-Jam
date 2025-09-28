using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class ShapePusherGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float timer = 10f;
    [SerializeField] ShapePusherSensor[] shapePusherSensors;
    [SerializeField] TextMeshProUGUI timerText;

    void Awake()
    {
        // assigns specific shape to specific sensor
        shapePusherSensors[0].setSensor("Triangle");
        shapePusherSensors[1].setSensor("Circle");
        shapePusherSensors[2].setSensor("Square");
    }

    // Update is called once per frame
    void Update()
    {
        // checks if all sensors are filled
        if (CheckSensors())
        {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/ShapePush/ShapePusherGameManager.cs
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Shape Pusher", true, 1));
            
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/ShapePusherGameManager.cs
            timerText.text = "Success!";
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
        }
        else
        {
            // timer countds down
            timer -= Time.deltaTime;
            timerText.text = "Time: " + ((int)timer).ToString();
        }
        // fail state
        if (timer <= 0)
        {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/ShapePush/ShapePusherGameManager.cs
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Shape Pusher", false));
            
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/ShapePusherGameManager.cs
            timerText.text = "Failure!";
            FindFirstObjectByType<GameManager>().MiniGameFailed();
        }
    }

    bool CheckSensors()
    {
        foreach (ShapePusherSensor s in  shapePusherSensors)
        {
            if (!s.filled)
            {
                return false;
            }
        }
        return true;
    }
}

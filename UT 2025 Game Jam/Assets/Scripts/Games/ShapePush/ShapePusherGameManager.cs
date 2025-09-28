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
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Shape Pusher", true, 1));
            
            timerText.text = "Success!";
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
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Shape Pusher", false));
            
            timerText.text = "Failure!";
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

using UnityEngine;
using TMPro;
using UnityEngine.XR;
public class LavaRisingGameManager : MonoBehaviour
{
    public static LavaRisingGameManager Instance { get; private set; }

    [SerializeField] float time = 10f;
    [SerializeField] AnimationCurve risingCurve;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject lava;

    GameObject[] numNodes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            numNodes = GameObject.FindGameObjectsWithTag("HexNode");
            //DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // checks if nodes are repaired
        if (CheckNodes())
        {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/LavaRising/LavaRisingGameManager.cs
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/LavaRising/LavaRisingGameManager.cs
           MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("LavaRising", true, 1));
=======
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
>>>>>>> parent of e654fca (Added Event Bus system to game manger system):UT 2025 Game Jam/Assets/Scripts/LavaRisingGameManager.cs
            
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/LavaRisingGameManager.cs
            timerText.text = "Success!";
            lava.GetComponent<LavaController>().isGrow = false;
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
        }
        else
        {
            // counts down timer, has fail state
            time -= Time.deltaTime;
            if (time <= 0 || !lava.GetComponent<LavaController>().isGrow)
            {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/LavaRising/LavaRisingGameManager.cs
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/LavaRising/LavaRisingGameManager.cs
                MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("LavaRising", false));
=======
                FindFirstObjectByType<GameManager>().MiniGameFailed();
>>>>>>> parent of e654fca (Added Event Bus system to game manger system):UT 2025 Game Jam/Assets/Scripts/LavaRisingGameManager.cs
                
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/LavaRisingGameManager.cs
                timerText.text = "Failure!";
                lava.GetComponent<LavaController>().isGrow = false;
                FindFirstObjectByType<GameManager>().MiniGameFailed();
            }
            else
            {
                timerText.text = "Time: " + (int)time;
                // makes lava rise
                if (lava.GetComponent<LavaController>().isGrow)
                    lava.transform.position = new Vector3(-0.2369f, risingCurve.Evaluate(time), 8.111722f);
            }
        }
    }

    bool CheckNodes()
    {
        foreach (GameObject node in numNodes)
        {
            if (node.GetComponent<RepairNode>().repaired)
                continue;
            else
                return false;
        }

        return true;
    }
}

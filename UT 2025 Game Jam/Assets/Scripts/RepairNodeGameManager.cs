using UnityEngine;
using TMPro;
public class RepairNodeGameManager : MonoBehaviour
{
    public static RepairNodeGameManager Instance { get; private set; }

    [SerializeField] float time = 10f;
    [SerializeField] TextMeshProUGUI timerText;

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
        // counts down timer, has fail state
        time -= Time.deltaTime;
        if (time <= 0 && !CheckNodes())
        {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/RepairNode/RepairNodeGameManager.cs
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/RepairNode/RepairNodeGameManager.cs
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("RepairNode", false));
=======
            FindFirstObjectByType<GameManager>().MiniGameFailed();
>>>>>>> parent of e654fca (Added Event Bus system to game manger system):UT 2025 Game Jam/Assets/Scripts/RepairNodeGameManager.cs
            
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/RepairNodeGameManager.cs
            timerText.text = "Failure!";
            FindFirstObjectByType<GameManager>().MiniGameFailed();
        }
        else
        {
            timerText.text = "Time: " + (int)time;
        }

        // checks if all nodes are activated
        if (CheckNodes())
        {
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/RepairNode/RepairNodeGameManager.cs
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/RepairNode/RepairNodeGameManager.cs
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("RepairNode", true, 1));
=======
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
>>>>>>> parent of e654fca (Added Event Bus system to game manger system):UT 2025 Game Jam/Assets/Scripts/RepairNodeGameManager.cs
            
=======
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/RepairNodeGameManager.cs
            timerText.text = "Success!";
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
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

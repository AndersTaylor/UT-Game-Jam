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
        if (time <= 0)
        {
            FindFirstObjectByType<GameManager>().MiniGameFailed();
            
            timerText.text = "Failure!";
        }
        else
        {
            timerText.text = "Time: " + (int)time;
        }

        // checks if all nodes are activated
        if (CheckNodes())
        {
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
            
            timerText.text = "Success!";
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

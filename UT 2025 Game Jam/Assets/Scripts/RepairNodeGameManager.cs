using UnityEngine;
using TMPro;
public class RepairNodeGameManager : MonoBehaviour
{
    public static RepairNodeGameManager Instance { get; private set; }

    [SerializeField] float time = 10f;
    [SerializeField] TextMeshProUGUI timerText;

    int numNodes;
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
            numNodes = GameObject.FindGameObjectsWithTag("HexNode").Length;
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
            timerText.text = "Failure!";
        }
        else
        {
            timerText.text = "Time: " + (int)time;
        }

        if (numNodes == 0)
        {
            timerText.text = "Success!";
        }
    }

    public void repairNode()
    {
        numNodes--;
    }
}

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
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
            
            timerText.text = "Success!";
            lava.GetComponent<LavaController>().isGrow = false;
        }
        else
        {
            // counts down timer, has fail state
            time -= Time.deltaTime;
            if (time <= 0 || !lava.GetComponent<LavaController>().isGrow)
            {
                FindFirstObjectByType<GameManager>().MiniGameFailed();
                
                timerText.text = "Failure!";
                lava.GetComponent<LavaController>().isGrow = false;
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

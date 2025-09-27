using UnityEngine;
using TMPro;
using UnityEngine.XR;
public class LavaRisingGameManager : MonoBehaviour
{
    public static LavaRisingGameManager Instance { get; private set; }

    [SerializeField] float time = 10f;
    [SerializeField] float risingSpeed = 2f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject lava;

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
            lava.GetComponent<LavaController>().isGrow = false;
        }
        else
        {
            timerText.text = "Time: " + (int)time;
            // makes lava rise
            if (lava.GetComponent<LavaController>().isGrow)
                lava.transform.localScale += new Vector3(0, Time.deltaTime * risingSpeed, 0);
        }

        if (numNodes == 0)
        {
            timerText.text = "Success!";
            lava.GetComponent<LavaController>().isGrow = false;
        }
    }

    public void repairNode()
    {
        // tracks nodes
        numNodes--;
    }
}

using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyCoreManager : MonoBehaviour
{
    [SerializeField] Transform[] path;
    [SerializeField] GameObject spark;
    [SerializeField] float sparkSpeed = 0.2f;
    [SerializeField] float timer = 10f;
    [SerializeField] TextMeshProUGUI timerText;

    int cur = 0;
    bool failed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (!failed)
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length == 0)
                timerText.text = "Success!";
            else
                timerText.text = "Time: " + (int)timer;
        }
        else if (failed)
            timerText.text = "Failure!";

    }

    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length != 0)
        {
            if (spark.transform.position != path[cur].position)
            {
                Vector2 p = Vector2.MoveTowards(spark.transform.position,
                                                path[cur].position,
                                                sparkSpeed);
                spark.GetComponent<Rigidbody2D>().MovePosition(p);
            }
            else if ((cur + 1) % path.Length == 0 || timer <= 0)
            {
                failed = true;
            }
            else cur = (cur + 1) % path.Length;
        }
        else
        {
            timerText.text = "Success!";
        }
    }

}

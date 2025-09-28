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
    [SerializeField] Sprite explodedCore;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip repaired;
    int cur = 0;
    bool failed = false;
    bool success = false;
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
            {
                timerText.text = "Success!";
                
                FindFirstObjectByType<GameManager>().MiniGameSuccess();
            }
            else
                timerText.text = "Time: " + (int)timer;
        }
        else if (failed)
        {
            timerText.text = "Failure!";
            
            FindFirstObjectByType<GameManager>().MiniGameFailed();
        }

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
            else if (((cur + 1) % path.Length == 0 || timer <= 0) && !failed)
            {
                failed = true;
                GameObject core = GameObject.FindGameObjectWithTag("Finish");
                core.GetComponent<SpriteRenderer>().sprite = explodedCore;
                source.Stop();
                source.PlayOneShot(explosion);
                spark.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
            else cur = (cur + 1) % path.Length;
        }
        else if (!success)
        {
            timerText.text = "Success!";
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/EnergyCore/EnergyCoreManager.cs
<<<<<<< HEAD:UT 2025 Game Jam/Assets/Scripts/Games/EnergyCore/EnergyCoreManager.cs

            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("EnergyCore", true, 1));
=======
            success = true;
            source.Stop();
            source.PlayOneShot(repaired);
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
>>>>>>> d1debe63e70dc5f1434182d4034bfdc2c47f1e04:UT 2025 Game Jam/Assets/Scripts/EnergyCoreManager.cs
=======
            
            FindFirstObjectByType<GameManager>().MiniGameSuccess();
>>>>>>> parent of e654fca (Added Event Bus system to game manger system):UT 2025 Game Jam/Assets/Scripts/EnergyCoreManager.cs
        }
    }

}

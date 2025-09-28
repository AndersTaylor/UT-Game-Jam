using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Rendering;

public class CanController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float angle = 5f;
    [SerializeField] int numToShake = 5;
    [SerializeField] float timer = 10f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI shakeText;
    [SerializeField] GameObject sodaSpill;

    float timeClicked = 0f;
    float timePassed = 0f;
    //int numStrikes = 3;
    bool clicked = false;
    bool spill = false;
    float oscillate = 1f;
    void Start()
    {
        shakeText.text = "Shakes left: " + numToShake;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time: " + (int)timer;
        timePassed += Time.deltaTime;
        if (spill && numToShake > 0)
        {
            shakeText.text = "You spilled!";
        }
        else if (numToShake <= 0 && !spill)
        {
            shakeText.text = "Success!";
        }
        else
        {
            shakeText.text = "Shakes left: " + numToShake;
        }

    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle+oscillate);
        oscillate *= -1;
    }

    private void OnMouseDown()
    {
        if (clicked)
        {
            if (timePassed - timeClicked < .1f)
            {
                //numStrikes--;
                //if (numStrikes <= 0)
                    GameObject particle = Instantiate(sodaSpill, transform);
                    particle.transform.position += new Vector3(.25f, 3f, 0);
                    spill = true;
            }
        }
        clicked = true;
        timeClicked = timePassed;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale += new Vector3(.05f, .05f, 0);
        angle *= -1.5f;
        oscillate *= 1.1f;
        numToShake--;
    }
}

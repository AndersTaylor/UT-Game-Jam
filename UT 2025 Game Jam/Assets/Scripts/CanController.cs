using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class CanController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float angle = 5f;
    [SerializeField] int numToShake = 5;
    [SerializeField] float timer = 10f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI shakeText;

    float timeClicked = 0f;
    float timePassed = 0f;
    //int numStrikes = 3;
    bool clicked = false;
    bool spill = false;
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

    private void OnMouseDown()
    {
        if (clicked)
        {
            if (timePassed - timeClicked < .2f)
            {
                //numStrikes--;
                //if (numStrikes <= 0)
                    spill = true;
            }
        }
        clicked = true;
        timeClicked = timePassed;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        angle *= -1.5f;
        numToShake--;
    }
}

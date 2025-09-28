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
    [SerializeField] Sprite spillSprite;
    [SerializeField] AudioClip sodaPop;
    [SerializeField] AudioClip sodaBomb;
    [SerializeField] AudioClip fizz;

    float timeClicked = 0f;
    float timePassed = 0f;
    //int numStrikes = 3;
    bool clicked = false;
    bool spill = false;
    bool success = false;
    float oscillate = 1f;
    void Start()
    {
        shakeText.text = "Shakes left: " + numToShake;
    }

    // Update is called once per frame
    void Update()
    {
        //timePassed += Time.deltaTime;
        //if (spill && !success)
        //{
        //    shakeText.text = "You spilled!";
        //    timerText.text = "\"no more metal gear.\" - Hideo \"Game\" Kojima";
            
        //    MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("CanShake", success: false));
        //}
        if (success)
        {
            shakeText.text = "Success!";
            timerText.text = "\"hideo game.\" - Hideo \"Game\" Kojima";
            
            MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("CanShake", true, 1));
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
        //if (clicked)
        //{
        //    if (timePassed - timeClicked < .1f && !spill && !success)
        //    {
        //        //numStrikes--;
        //        //if (numStrikes <= 0)
        //    }
        //}
        //clicked = true;
        //timeClicked = timePassed;
        if (!success)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.localScale += new Vector3(.02f, .02f, 0);
            if (angle < 1f && angle > -1f)
                angle *= -1.5f;
            else
                angle *= -1;
                oscillate *= 1.1f;
            numToShake--;
        }
        if (numToShake <= 0 && !success && !spill)
        {
            success = true;
            GameObject particle = Instantiate(sodaSpill, transform);
            particle.transform.position += new Vector3(.25f, 3f, 0);
            GetComponent<SpriteRenderer>().sprite = spillSprite;
            //PlayClip(sodaBomb);
            PlayClip(sodaPop);
            GetComponent<AudioSource>().clip = fizz;
            GetComponent<AudioSource>().Play(1);
        }
    }

    void PlayClip(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}

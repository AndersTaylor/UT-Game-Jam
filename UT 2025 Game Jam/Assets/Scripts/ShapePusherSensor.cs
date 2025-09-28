using UnityEngine;

public class ShapePusherSensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string shape;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip bonk;
    [SerializeField] AudioClip filledClip;
    public bool filled;
    void Start()
    {
        filled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if shape matches sensor; resets if false
        GameObject co = collision.gameObject;
        if ((co.name).Equals(shape))
        {
            source.PlayOneShot(filledClip);
            filled = true;
            Destroy(co.gameObject);
        }
        else
        {
            filled = false;
            source.PlayOneShot(bonk);
            co.gameObject.transform.position = new Vector3(0, 15, 8.111721f);
        }
        
    }

    public void setSensor(string shape)
    {
        this.shape = shape;
    }

    public string getSensor() 
    {
        return this.shape;
    }
}

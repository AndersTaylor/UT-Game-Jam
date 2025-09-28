using UnityEngine;

public class ShapePusherSensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string shape;

    public bool filled;
    void Start()
    {
        filled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(shape);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // checks if shape matches sensor; resets if false
        GameObject co = collision.gameObject;
        if ((co.name).Equals(shape))
        {
            filled = true;
            Destroy(co.gameObject);
        }
        else
        {
            filled = false;
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

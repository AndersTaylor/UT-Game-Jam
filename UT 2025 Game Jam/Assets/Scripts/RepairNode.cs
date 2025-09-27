using UnityEngine;

public class RepairNode : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool repaired;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        repaired = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // changes color once "fixed"
        spriteRenderer.color = new Color(0, 255, 0);
        repaired = true;
    }
}

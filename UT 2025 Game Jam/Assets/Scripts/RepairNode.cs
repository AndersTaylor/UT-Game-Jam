using UnityEngine;

public class RepairNode : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite onSprite;
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip bloop;
    [SerializeField] GameObject sparks;
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
        spriteRenderer.sprite = onSprite;
        GetComponent<AudioSource>().PlayOneShot(click);
        GetComponent<AudioSource>().PlayOneShot(bloop);
        GameObject sp = Instantiate(sparks, transform);
        repaired = true;
    }
}

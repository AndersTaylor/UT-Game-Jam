using Unity.VisualScripting;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public bool isGrow;
    [SerializeField] AudioSource bgSound;
    [SerializeField] AudioClip splash;
    [SerializeField] GameObject effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrow = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // checks if player falls in lava
        PlatformerCharacterController player = other.GetComponent<PlatformerCharacterController>();
        if (player != null)
        {
            isGrow = false;
            bgSound.PlayOneShot(splash);
            Instantiate(effect, player.transform.position, player.transform.rotation);
           // Debug.Log("dead");
        }
    }
}

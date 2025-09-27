using Unity.VisualScripting;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public bool isGrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlatformerCharacterController player = other.GetComponent<PlatformerCharacterController>();
        if (player != null)
        {
            isGrow = false;
        }
    }
}

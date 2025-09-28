using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RepositionHydroplanes : MonoBehaviour
{
    [SerializeField] private Transform centerPoint; // The center point to reposition the hydroplanes
    [SerializeField] private Transform indicator; // The transform of the hydroplanes
    [SerializeField] private Transform triangle; // The transform of the hydroplanesS
    [SerializeField] private float rotationSpeed = 50f; // The radius around the center point
    private bool isRotating = true; // Flag to check if the hydroplanes are rotating
    [SerializeField] private HydroplanesAligned hydroplanesAlignedObject;
    private HydroplanesAligned hydroplanesAlignedScript;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    public event Action OnIndicatorAligned; // Event to notify when the indicator is aligned with the triangle

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RotateHydroplanes());
        hydroplanesAlignedScript = hydroplanesAlignedObject.GetComponent<HydroplanesAligned>(); //Find the HydroplanesAligned script in the scene
        hydroplanesAlignedScript.AttemptsText.text = $"Attempts remaining: {hydroplanesAlignedScript.attemptsRemaining}"; // Set the initial attempts text
        if (hydroplanesAlignedScript == null)
        {
            Debug.LogError("HydroplanesAligned script not found in the scene.");
        }

    }

    private IEnumerator WaitAndRestartRotation(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        isRotating = true; // Resume the rotation
    }

    private IEnumerator RotateHydroplanes()
    {
        while (true) // Keep the coroutine running indefinitely
        {
            if (isRotating)
            {
                // Rotate the hydroplanes around the center point
                indicator.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            yield return null; // Wait for the next frame
        }
    }
    
    public void OnWheelClicked()
    {
        source.PlayOneShot(clip);
        // Stop the rotation
        isRotating = false;

        OnIndicatorAligned?.Invoke(); // Trigger the event to notify that the indicator is aligned

        if (IsIndicatorAligned)
        {
           // Debug.Log("Indicator is aligned with the triangle!");

            // Snap the indicator to the triangle's position
            Vector3 offsetPosition = triangle.position - new Vector3(10f, 0f, 0f);
            indicator.position = offsetPosition;

            //Trigger the event to notify that the indicator is aligned
            //OnIndicatorAligned?.Invoke();

        }
        else
        {
            
           // Debug.Log("Indicator is not aligned with the triangle!");
            hydroplanesAlignedScript.attemptsRemaining--;
            hydroplanesAlignedScript.AttemptsText.text = $"Incorrect alignment. Attempts remaining: {hydroplanesAlignedScript.attemptsRemaining}";
            if (hydroplanesAlignedScript.attemptsRemaining <= 0)
            {
                hydroplanesAlignedScript.AttemptsText.text = "No attempts remaining!";
                hydroplanesAlignedScript.stopAllRotations();
                
            } 
          
            StartCoroutine(WaitAndRestartRotation(1f));
        }

        
    }
    
    public bool IsIndicatorAligned
    {
        get
        {
            // Check if the indicator is aligned with the triangle
            Vector3 indicatorDirection = (indicator.position - centerPoint.position).normalized;
            Vector3 triangleDirection = (triangle.position - centerPoint.position).normalized;

            // Calculate the dot product of the two direction vectors
            float dotProduct = Vector3.Dot(indicatorDirection, triangleDirection);

            return dotProduct > 0.99f; // Return true if the distance is less than 0.1 units
        }
    }
}

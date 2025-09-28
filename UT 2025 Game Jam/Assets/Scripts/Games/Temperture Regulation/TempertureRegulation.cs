using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TempertureRegulation : MonoBehaviour
{
    [SerializeField] Slider temperatureSlider;
    [SerializeField] TMP_Text temperatureText; // Reference to the TMP_Text component
    [SerializeField] TMP_Text TimerText; // Reference to the TMP_Text component
    [SerializeField] float minTemperature = 0f; // Minimum temperature value
    [SerializeField] float maxTemperature = 100f; // Maximum temperature value
    [SerializeField] float decreaseRate = 0.1f; // Rate at which the temperature decreases
    [SerializeField] float increaseRate = 0.1f; // Rate at which the temperature increases
    [SerializeField] float decreaseDelay = 1f; // Delay between temperature decreases

    [SerializeField] float calibrationMin = 40f; // Minimum temperature for calibration
    [SerializeField] float calibrationMax = 60f; // Maximum temperature for calibration
    [SerializeField] float calibrationTime = 5f; // Time required to stay in range for calibration

    private bool isCalibrating = false; // Flag to prevent multiple calibration checks

   //  [SerializeField] private Room roomToLoadAfterCompletion; // Reference to the room to load after completion
  //  [SerializeField] private Chapter ChapterToLoadAfterCompletion; // Reference to the room to load after completion


    void Start()
    {
        temperatureSlider = GetComponent<Slider>();
        temperatureSlider.minValue = 0f; // Slider range starts at 0
        temperatureSlider.maxValue = 1f; // Slider range ends at 1
        StartCoroutine(DecreaseTemperature());
        temperatureSlider.value = NormalizeTemperature(minTemperature); // Initialize slider value
    }

    void Update()
    {
        // Update the temperature text to show the actual temperature
        temperatureText.text = $" {DenormalizeTemperature(temperatureSlider.value):F1}";

        // Start calibration check if not already calibrating
        if (!isCalibrating)
        {
            StartCoroutine(CheckCalibration());
        }
    }

    public void increaseTemp()
    {
        temperatureSlider.value += NormalizeTemperature(increaseRate);
        temperatureSlider.value = Mathf.Clamp(temperatureSlider.value, 0f, 1f); // Clamp to slider range
        //Debug.Log("Temperature increased: " + DenormalizeTemperature(temperatureSlider.value));
    }

    private IEnumerator DecreaseTemperature()
    {
        
        while (temperatureSlider.value >= 0)
        {
            temperatureSlider.value -= NormalizeTemperature(decreaseRate);
            temperatureSlider.value = Mathf.Clamp(temperatureSlider.value, 0f, 1f); // Clamp to slider range
            //Debug.Log($"Decreasing: Slider Value = {temperatureSlider.value}, Temperature = {DenormalizeTemperature(temperatureSlider.value)}");
            yield return new WaitForSeconds(decreaseDelay);
        }
        
    }

    private IEnumerator CheckCalibration()
    {
        isCalibrating = true;
        float elapsedTime = 0f;

        while (elapsedTime < calibrationTime)
        {
            float currentTemperature = DenormalizeTemperature(temperatureSlider.value);

            // Check if the temperature is outside the calibration range
            if (currentTemperature <= calibrationMin || currentTemperature >= calibrationMax)
            {
                //Debug.Log("Temperature out of range. Resetting calibration timer.");
                elapsedTime = 0f; // Reset the timer
            }
            else
            {
                elapsedTime += Time.deltaTime; // Increment the timer
                TimerText.text = $"Calibration Time: {elapsedTime:F1}/{calibrationTime} seconds"; // Update the timer text
              //  Debug.Log($"Calibration in progress: {elapsedTime:F1}/{calibrationTime} seconds");
            }

            yield return null; // Wait for the next frame
        }

      //  Debug.Log("Calibration complete!");
        OnCalibrationComplete(); // Trigger the calibration complete event
        isCalibrating = false;
    }

    private void OnCalibrationComplete()
    {
        // Add logic for what happens when calibration is complete
        TimerText.text = "Temperature calibrated!";
       // Debug.Log("Temperature successfully calibrated!");
        StopAllCoroutines(); // Stop the temperature decrease coroutine


        MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Temperature Regulation", true, 1));
    }

    private float NormalizeTemperature(float temperature)
    {
        return (temperature - minTemperature) / (maxTemperature - minTemperature);
    }

    private float DenormalizeTemperature(float normalizedValue)
    {
        return normalizedValue * (maxTemperature - minTemperature) + minTemperature;
    }
}

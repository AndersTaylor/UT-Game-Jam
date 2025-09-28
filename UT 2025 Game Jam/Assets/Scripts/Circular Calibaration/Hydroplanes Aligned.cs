using UnityEngine;
using TMPro;

public class HydroplanesAligned : MonoBehaviour
{
    [SerializeField] private GameObject[] circularCalibrator; // Array of hydroplane scripts
    public int attemptsRemaining; // Number of attempts remaining
    public TMP_Text AttemptsText; // Reference to the TextMeshProUGUI component to display attempts
    //[SerializeField] private Room roomToLoadAfterCompletion; // Reference to the room to load after completion
   // [SerializeField] private Chapter ChapterToLoadAfterCompletion; // Reference to the room to load after completion
  //  [SerializeField] private Room roomToLoadAfterFailure; // Reference to the room to load after completion
  //  [SerializeField] private Chapter ChapterToLoadAfterFailure; 

    void Start()
    {
        // Subscribe to each calibrator's alignment check
        foreach (var calibrator in circularCalibrator)
        {
            calibrator.GetComponent<RepositionHydroplanes>().OnIndicatorAligned += CheckAlignmentStatus;
        }

    }

    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        foreach (var calibrator in circularCalibrator)
        {
            calibrator.GetComponent<RepositionHydroplanes>().OnIndicatorAligned -= CheckAlignmentStatus;
        }
        AttemptsText.text = $"Attempts remaining: {attemptsRemaining}";
    }

    private void CheckAlignmentStatus()
    {
        // Check if all indicators are aligned
        if (AreAllIndicatorsAligned())
        {
            GameManager.Instance.MiniGameSuccess();
        }
        else if (attemptsRemaining == 0)
        { 
            GameManager.Instance.MiniGameFailed();
        }
    }

    private bool AreAllIndicatorsAligned()
    {
        // Loop through each calibrator and check if all are aligned
        foreach (var calibrator in circularCalibrator)
        {
            if (!calibrator.GetComponent<RepositionHydroplanes>().IsIndicatorAligned)
            {
                return false; // If any indicator is not aligned, return false
            }
        }
        return true; // All indicators are aligned
    }
    
    public void stopAllRotations()
    {
        // Stop all rotations
        foreach (var calibrator in circularCalibrator)
        {
            var hydroplaneScript = calibrator.GetComponent<RepositionHydroplanes>();
            if (hydroplaneScript != null)
            {
                hydroplaneScript.StopAllCoroutines();
            }
        }
    }
}

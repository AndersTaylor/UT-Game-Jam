using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to use UI components
using TMPro; // Import the TextMeshPro namespace to use TextMeshPro components

public class SymbolsManager : MonoBehaviour
{
    [SerializeField] private string[] symbols; // Array to hold the symbols
    [SerializeField] private int SequenceLength; // Variable to hold the length of the sequence
    [SerializeField] private int SequenceRepeatRate; // Variable to hold the length of the sequence
    [SerializeField] private int currentAttempts = 0; 
    [SerializeField] private int maxAttempts = 3; 
    [SerializeField] private bool maxAttemptsReached = false; 
    [SerializeField] private TMP_Text AttemptsText; // Reference to the TextMeshProUGUI component to display attempts
    [SerializeField] private UnityEngine.UI.Image RedImage; // Reference to the red image component
    [SerializeField] private UnityEngine.UI.Image BlueImage; // Reference to the blue image component
    [SerializeField] private UnityEngine.UI.Image GreenImage; // Reference to the green image component
    [SerializeField] private UnityEngine.UI.Image YellowImage; // Reference to the yellow image component
    [SerializeField] private Color GreenColor;
    private string[] repeatedSequence; // Array to hold the repeated sequence

    private int currentSequenceIndex = 0; // Variable to keep track of the current index in the sequence

  //  [SerializeField] private Room roomToLoadAfterCompletion; // Reference to the room to load after completion
  //  [SerializeField] private Chapter ChapterToLoadAfterCompletion; // Reference to the room to load after completion
  //   [SerializeField] private Room roomToLoadAfterFailure; // Reference to the room to load after completion
 //  [SerializeField] private Chapter ChapterToLoadAfterFailure; // Reference to the room to load after completion

    // Add flags to track if an image is being modified
    private bool isRedFlashing = false;
    private bool isBlueFlashing = false;
    private bool isGreenFlashing = false;
    private bool isYellowFlashing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        symbols = new string[] { "RED", "BLUE", "GREEN", "YELLOW" }; // Initialize the symbols array with some values
        repeatedSequence = GetRandomizeSequence(SequenceLength); // Call the method to repeat the sequence
        AttemptsText.text = $"Attempts: {currentAttempts} remaning"; // Update text on success
        StartCoroutine(RepeatFlashSequence()); // Start the coroutine to repeat the flash sequence
    }

    private IEnumerator RepeatFlashSequence()
    {
        while (true) // Infinite loop to repeat the sequence
        {

         //   print("Randomized sequence: " + string.Join(", ", repeatedSequence)); // Print the randomized sequence to the console
            yield return StartCoroutine(FlashSequence(repeatedSequence)); // Start the coroutine to show the symbols
            yield return new WaitForSeconds(SequenceRepeatRate); // Wait for 1 second before repeating the sequence
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonPress(string color)
    {
        if (maxAttemptsReached)
        {
           // print("Max attempts reached. Sequence failed.");
           
           MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Sequence Puzzle", false));
            return; // Exit if max attempts have been reached
        }
        if (repeatedSequence[currentSequenceIndex] == color)
        {
            currentSequenceIndex++;
            if (currentSequenceIndex >= repeatedSequence.Length)
            {
              //  print("Sequence completed successfully!");
                currentSequenceIndex = 0;
                currentAttempts = 0; // Reset attempts on success
                AttemptsText.text = $"Attempts: {currentAttempts} remaning"; // Update text on success
               
                MiniGameEventBus.RaiseOnMiniGameComplete(new MiniGameEventBus.Result("Sequence Puzzle", true, 1));
            }
        }
        else
        {
          //  print("Wrong button! Sequence failed.");
            currentSequenceIndex = 0;
            currentAttempts--;
            AttemptsText.text = $"Attempts: {currentAttempts} remaning"; // Update text on failure
            if (currentAttempts == 0)
            {
              //  print("Max attempts reached. Sequence failed.");
                AttemptsText.text =$"Reactor Restart Failed"; // Update text on failure
                maxAttemptsReached = true;


                StopAllCoroutines(); // Stop all ongoing flashes
                
                
            }
            StopAllCoroutines(); // Stop all ongoing flashes
            ForceResetAllFlashes(); // <--- Add this line
            StartCoroutine(FlashAllImagesWhite());
        }
    }

    private void ResetFlashingFlags()
    {
        isRedFlashing = false;
        isBlueFlashing = false;
        isGreenFlashing = false;
        isYellowFlashing = false;
    }

    private IEnumerator FlashAllImagesWhite()
    {
        // Reset flags before flashing
        ResetFlashingFlags();

        Color redOriginal = RedImage.color;
        Color blueOriginal = BlueImage.color;
        Color greenOriginal = GreenImage.color;
        Color yellowOriginal = YellowImage.color;

        RedImage.color = Color.white;
        BlueImage.color = Color.white;
        GreenImage.color = Color.white;
        YellowImage.color = Color.white;

        yield return new WaitForSeconds(1f);

        RedImage.color = redOriginal;
        BlueImage.color = blueOriginal;
        GreenImage.color = greenOriginal;
        YellowImage.color = yellowOriginal;

        // Reset flags again after flashing
        ResetFlashingFlags();

        // Optionally, restart the sequence flashing
        StartCoroutine(RepeatFlashSequence());
    }

    public string[] GetRandomizeSequence(int sequenceLength)
    {
        // Create a copy of the symbols array
        string[] randomizedSequence = new string[sequenceLength];

        for (int i = 0; i < sequenceLength; i++)
        {
            int randomIndex = Random.Range(0, symbols.Length); // Generate a random index within the range of the symbols array
            randomizedSequence[i] = symbols[randomIndex]; // Assign a random symbol to the randomized array
        }


        return randomizedSequence; // Return the randomized array
    }

    private IEnumerator FlashSequence(string[] sequence)
    {
        foreach (string color in sequence)
        {
            switch (color)
            {
                case "RED":
                    yield return flashColor(RedImage, () => isRedFlashing, v => isRedFlashing = v);
                    break;
                case "BLUE":
                    yield return flashColor(BlueImage, () => isBlueFlashing, v => isBlueFlashing = v);
                    break;
                case "GREEN":
                    yield return flashColor(GreenImage, () => isGreenFlashing, v => isGreenFlashing = v);
                    break;
                case "YELLOW":
                    yield return flashColor(YellowImage, () => isYellowFlashing, v => isYellowFlashing = v);
                    break;
            }
        }
    }

    private IEnumerator flashColor(UnityEngine.UI.Image image, System.Func<bool> getFlag, System.Action<bool> setFlag)
    {
        if (getFlag()) yield break; // Prevent overlapping flashes
        setFlag(true);

        Color originalColor = image.color;
        image.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        image.color = originalColor;
        yield return new WaitForSeconds(0.2f);

        setFlag(false);
    }

    private void ForceResetAllFlashes()
    {
        // Reset all flags
        isRedFlashing = false;
        isBlueFlashing = false;
        isGreenFlashing = false;
        isYellowFlashing = false;

        // Restore all image colors (replace with your default colors if not Color.red/blue/green/yellow)
        if (RedImage != null) RedImage.color = GreenColor; // Replace with your default color
        if (BlueImage != null) BlueImage.color = GreenColor;
        if (GreenImage != null) GreenImage.color = GreenColor;
        if (YellowImage != null) YellowImage.color = GreenColor;
    }
    
}

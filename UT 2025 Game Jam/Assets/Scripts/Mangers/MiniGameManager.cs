using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<MiniGameData> miniGames = new List<MiniGameData>();
    [SerializeField] private List<MiniGameData> discardedGames = new List<MiniGameData>();

    [SerializeField] private int selectedGameIndex;
    [SerializeField] private int gamesPlayed = 0;
    private int gamesBeforeReset = 4;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        StartNextMiniGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnMiniGameComplete();
        }
    }


    private void repopulateMiniGames()
    {
        miniGames.AddRange(discardedGames);
        discardedGames.Clear();
        ShuffleMiniGames(miniGames); // shuffle the list after repopulating
    }

    private void ShuffleMiniGames<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    // Update is called once per frame

    public string randomizeMiniGames()
    {
        selectedGameIndex = Random.Range(0, miniGames.Count);
       // Debug.Log(miniGames[selectedGameIndex].miniGameName);
        return miniGames[selectedGameIndex].miniGameName;

    }

    public float timeToCompleteGame()
    {
        //Debug.Log(miniGames[selectedGameIndex].timeLimit);
       // Debug.Log("selected index: " + selectedGameIndex);
        //Debug.Log("games count: " + miniGames.Count);
        return miniGames[selectedGameIndex].timeLimit;
    }

    void LoadMiniGames( string _miniGameName)
    {
        SceneManager.LoadScene(_miniGameName);

    }

    public void StartNextMiniGame()
    {
        MiniGameTimer.Instance.ResetTimer(MiniGameManager.Instance.timeToCompleteGame());
        
        if (miniGames.Count == 0)
        {
            repopulateMiniGames();
            gamesPlayed = 0;
            //Debug.Log("Mini games list repopulated.");
        }

        string miniGameName = randomizeMiniGames();
        LoadMiniGames(miniGameName);
        // The mini-game scene should call OnMiniGameComplete() when done
    }

    public void OnMiniGameComplete()
    {
        if (selectedGameIndex >= 0 )
        {
            discardedGames.Add(miniGames[selectedGameIndex]);

            miniGames.RemoveAt(selectedGameIndex);
        }
        else
        {
            //Debug.LogWarning("selectedGameIndex out of range!");
        }
        gamesPlayed++;
       // Debug.Log($"discardedGames count: {discardedGames.Count}");

        if (gamesPlayed >= gamesBeforeReset)
        {
            repopulateMiniGames();
            gamesPlayed = 0;
            //Debug.Log("Mini games list repopulated.");
        }

        StartNextMiniGame();
    }
}


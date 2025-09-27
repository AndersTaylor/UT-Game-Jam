using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<MiniGameData> miniGames = new List<MiniGameData>();
    [SerializeField] private int selectedGameIndex;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        randomizeMiniGames();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadMiniGames();
    }

    public string randomizeMiniGames()
    {
        selectedGameIndex = Random.Range(0, miniGames.Count);
        Debug.Log(miniGames[selectedGameIndex].miniGameName);
        return miniGames[selectedGameIndex].miniGameName;

    }

    public float timeToCompleteGame()
    {
        Debug.Log(miniGames[selectedGameIndex].timeLimit);
        return miniGames[selectedGameIndex].timeLimit;
    }
    
    void LoadMiniGames()
    {
        var miniGameName = randomizeMiniGames();
        var mingameTime = timeToCompleteGame();
        SceneManager.LoadScene(miniGameName);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<MiniGameData> miniGames = new List<MiniGameData>();
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
        int randomIndex = Random.Range(0, miniGames.Count);
        Debug.Log(miniGames[randomIndex].miniGameName);
        return miniGames[randomIndex].miniGameName;

    }
    
    void LoadMiniGames()
    {
        var miniGameName = randomizeMiniGames();
        SceneManager.LoadScene(miniGameName);
    }


    
}

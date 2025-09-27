using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MiniGameData", fileName = "New MiniGameData")]
public class MiniGameData : ScriptableObject
{
    public string miniGameName;
    public float timeLimit;

}

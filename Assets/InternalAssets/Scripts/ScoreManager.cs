using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int victoryScoreEditorVariable;

    static public int VictoryScore;
    static public Dictionary<int, int> playerScores;

    private void Awake()
    {
        playerScores = new Dictionary<int, int>();
    }

    // Use this for initialization
    void Start ()
    {
        VictoryScore = victoryScoreEditorVariable;
    }

    public static void RegisterPlayer(int m_playerId)
    {
        if (!playerScores.ContainsKey(m_playerId))
            playerScores.Add(m_playerId, 0);
    }

    public static int GetPlayerScore(int m_playerId)
    {
        return playerScores[m_playerId];
    }

    public static void ChangePlayerScoreBy(int m_playerId, int value)
    {
        playerScores[m_playerId] += value;
        HudManager.IncrementScoreSliderValue(m_playerId, value);
        if (playerScores[m_playerId] > VictoryScore)
        {
            Time.timeScale = 0.0f;
            Debug.Log("WIN SCREEN, win for Player "+m_playerId+1);
        }
    }

}

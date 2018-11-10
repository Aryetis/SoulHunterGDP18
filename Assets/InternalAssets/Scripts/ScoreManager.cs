using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static public Dictionary<int, int> playerScores;

    // Use this for initialization
    void Start ()
    {
        playerScores = new Dictionary<int, int>();
    }

    public void RegisterPlayer(int m_playerId)
    {
        if (!playerScores.ContainsKey(m_playerId))
            playerScores.Add(m_playerId, 0);
    }

    public int GetPlayerScore(int m_playerId)
    {
        return playerScores[m_playerId];
    }

    public void ChangePlayerScoreBy(int m_playerId, int value)
    {
        playerScores[m_playerId] += value;
        HudManager.IncrementScoreSliderValue(m_playerId, value);
    }

}

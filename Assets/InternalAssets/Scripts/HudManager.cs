using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    private const double V = 0.1;
    private static Slider[] scoreSliders;

    // Use this for initialization
    void Start()
    {
        scoreSliders = new Slider[3];
        scoreSliders[0] = GameObject.Find("PlayersBlackCountSoul").GetComponent<Slider>();
        scoreSliders[0].value = 0;
        scoreSliders[1] = GameObject.Find("PlayersRedCountSoul").GetComponent<Slider>();
        scoreSliders[1].value = 0;
        scoreSliders[2] = GameObject.Find("PlayersYellowCountSoul").GetComponent<Slider>();
        scoreSliders[2].value = 0;
    }

    public static void IncrementScoreSliderValue(int player_id, int value_)
    {
        if (scoreSliders[player_id].value + value_ > 1)
            scoreSliders[player_id].value = 1.0f;
        else if (scoreSliders[player_id].value + value_ < 0)
            scoreSliders[player_id].value = 0.0f;
        else
            scoreSliders[player_id].value += value_; 
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (Input.GetKey(KeyCode.UpArrow))
    //        slider.value += 0.100f;
    //    if (Input.GetKey(KeyCode.DownArrow))
    //        slider.value = 0;
    //}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    private const double V = 0.1;
    GameObject SliderBlack;
    Slider slider;

    // Use this for initialization
    void Start()
    {
        SliderBlack = GameObject.Find("PlayersBlackCountSoul");
        slider = SliderBlack.GetComponent<Slider>();
        slider.value = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            slider.value += 0.100f;
        if (Input.GetKey(KeyCode.DownArrow))
            slider.value = 0;
    }
}


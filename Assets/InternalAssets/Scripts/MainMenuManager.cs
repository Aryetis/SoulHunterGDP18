using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static bool MAIN_MENU_DEBUG = true;

	// Use this for initialization
	void Start ()
    {
        if(MAIN_MENU_DEBUG)
            SceneLoader.StaticLoadScene("ControllerSelectionScene");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadControllerScene()
    {
        SceneLoader.StaticLoadScene("ControllerSelectionScene");
    }
}

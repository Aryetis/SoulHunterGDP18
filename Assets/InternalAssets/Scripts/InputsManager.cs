using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : MonoBehaviour
{
    static public Dictionary<int, InputTable> playerInputsDictionary;
    delegate void OnControllerDetached(int controllerID);
    delegate void OnControllerAttached(int controllerID);

    // Use this for initialization
    void Start ()
    {
        playerInputsDictionary = new Dictionary<int, InputTable>();
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //void RegisterToAttachedControllerEvent()
    //{

    //}

    //void RegisterToDetachedControllerEvent()
    //{

    //}

    //void OnControllerDetached()
    //{
            
    //}

    //void OnControllerAttached()
    //{

    //}

    public static void SetControllerIdToPlayer(int controllerId_, int playerID)
    {
        if (!playerInputsDictionary.ContainsKey(playerID))
            playerInputsDictionary[playerID] = new InputTable();
        playerInputsDictionary[playerID].controllerId = controllerId_;
    }

    public static bool IsControllerIdUsed(int controllerId_)
    {
        foreach (KeyValuePair<int, InputTable> entry in InputsManager.playerInputsDictionary)
        {
            if ( entry.Value.controllerId == controllerId_ )
                return true;
        }

        return false;
    }
}


public class InputTable
{
    public int controllerId;
    public float LeftAnalogForwardAxis; // X Axis && buttons
    public float LeftAnalogStrafeAxis; // Y Axis && buttons
    public float RightAnalogXAxis;
    public float RightAnalogYAxis;
    public bool Dash;
    public bool Pause;

    public InputTable()
    { controllerId = -1; }
}
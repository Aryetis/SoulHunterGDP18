﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsBehavior : MonoBehaviour
{
    static private int playerIDGenerator = 0;
    private int playerID; // Used to map controller

    private void Awake()
    {
        playerID = playerIDGenerator;
        ++playerIDGenerator;
    }

    // Use this for initialization
    void Start ()
    {
    }

    private void FixedUpdate()
    {
        
    }

 //   // Update is called once per frame
 //   void Update ()
 //   {
		
	//}
}

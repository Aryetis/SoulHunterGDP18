﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsBehavior : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float startDashTime;
    public float dashCooldownTime;
    public bool IsStunned;

    private Rigidbody rb;
    private Collider playerCollider;
    private float dashTime;
    private bool IsDashing;
    private int playerId;
    private static int playerIdGenerator = 0; // TODO reset on scene load
    private float dashCooldown;
    private bool dashAllowed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponentInChildren<Collider>();
        dashTime = startDashTime;
        IsDashing = false;
        rb.velocity = Vector3.zero;
        playerCollider.enabled = true;
        playerId = playerIdGenerator;
        ++playerIdGenerator;
        dashCooldown = 0;
        dashAllowed = true;
        IsStunned = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("DashRequested : " + IsDashing);
        Debug.Log("dashTime : " + dashTime);
        Debug.Log("dashCooldown : " + dashCooldown);

        if (!dashAllowed)
        {
            dashCooldown -= Time.fixedDeltaTime;
            if (dashCooldown <= 0)
            {
                dashAllowed = true;
                dashCooldown = dashCooldownTime;
            }
        }

        if (InputsManager.playerInputsDictionary[playerId].DashPressed && dashTime == startDashTime && dashAllowed)
            IsDashing = true;

        if (!IsDashing)
        {
            rb.velocity = Vector3.zero;
            rb.drag = 0.0f;

            // Basic Movements
            rb.velocity = (Vector3.forward * speed * InputsManager.playerInputsDictionary[playerId].LeftAnalogForwardAxis
                         + Vector3.right * speed * InputsManager.playerInputsDictionary[playerId].LeftAnalogStrafeAxis) * Time.fixedDeltaTime;
        }
        else if (IsDashing)
        {
            if (dashTime == startDashTime) // Give impulse
            {
                playerCollider.enabled = false;

                rb.velocity = ((Vector3.forward * InputsManager.playerInputsDictionary[playerId].LeftAnalogForwardAxis
                         + Vector3.right * InputsManager.playerInputsDictionary[playerId].LeftAnalogStrafeAxis).normalized * dashSpeed
                         * Time.fixedDeltaTime);
                dashAllowed = false;
            }
            else if (dashTime <= 0) // Dash is Over
            {
                IsDashing = false;
                playerCollider.enabled = true;
                dashTime = startDashTime;
                dashCooldown = dashCooldownTime;
                return;
            }

            dashTime -= Time.fixedDeltaTime;
        }
    }
}


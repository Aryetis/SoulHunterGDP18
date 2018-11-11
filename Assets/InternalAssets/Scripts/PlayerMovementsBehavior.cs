using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsBehavior : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float startDashTime;
    public float dashCooldownTime;
    public bool IsStunned;
    public bool IsAttacking;

    private Rigidbody rb;
    private Collider playerCollider;
    private float dashTime;
    private bool IsDashing;
    private float dashCooldown;
    private bool dashAllowed;
    private PlayerIdDistributor pid;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponentInChildren<Collider>();
        dashTime = startDashTime;
        IsDashing = false;
        rb.velocity = Vector3.zero;
        playerCollider.enabled = true;
        dashCooldown = 0;
        dashAllowed = true;
        IsStunned = false;
        pid = GetComponent<PlayerIdDistributor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("DashRequested : " + IsDashing);
        Debug.Log("dashTime : " + dashTime);
        Debug.Log("dashCooldown : " + dashCooldown);

        if (!dashAllowed || IsStunned || IsAttacking )
        {
            dashCooldown -= Time.fixedDeltaTime;
            if (dashCooldown <= 0)
            {
                dashAllowed = true;
                dashCooldown = dashCooldownTime;
            }
        }

        if (InputsManager.playerInputsDictionary[pid.PlayerId].DashPressed && dashTime == startDashTime && dashAllowed)
            IsDashing = true;

        if (!IsDashing)
        {
            rb.velocity = Vector3.zero;
            rb.drag = 0.0f;

            // Basic Movements
            rb.velocity = (Vector3.forward * speed * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogForwardAxis
                         + Vector3.right * speed * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogStrafeAxis) * Time.fixedDeltaTime;
        }
        else if (IsDashing)
        {
            if (dashTime == startDashTime) // Give impulse
            {
                playerCollider.enabled = false;

                rb.velocity = ((Vector3.forward * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogForwardAxis
                         + Vector3.right * InputsManager.playerInputsDictionary[pid.PlayerId].LeftAnalogStrafeAxis).normalized * dashSpeed
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

    public void StunForSeconds(float seconds)
    {
        IsStunned = true;
        Invoke("UnStun", seconds);
    }

    private void UnStun()
    {
        IsStunned = false;
    }
}


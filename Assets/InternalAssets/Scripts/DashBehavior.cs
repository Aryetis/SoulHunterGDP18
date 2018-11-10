using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehavior : MonoBehaviour
{
  private Rigidbody rb;
  public float dashSpeed;
  private float dashTime;
  public float startDashTime;
  private BoxCollider playerCollider;
	private bool IsDdashing;

  // Use this for initialization
  void Start()
  {
    rb = GetComponent<Rigidbody>();
		playerCollider = GetComponentInChildren<BoxCollider>();
    dashTime = startDashTime;
    rb.velocity = Vector3.zero;
		playerCollider.enabled = true;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (Input.GetKey(KeyCode.UpArrow))
      transform.position += new Vector3(0, 0, 1) * Time.deltaTime;

    if (Input.GetKey(KeyCode.DownArrow))
      transform.position -= new Vector3(0, 0, 1) * Time.deltaTime;

    if (Input.GetKey(KeyCode.RightArrow))
      transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

    if (Input.GetKey(KeyCode.LeftArrow))
      transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space) || dashTime <=0 )
	     IsDdashing = true;
  
      if (dashTime <= 0)
      {
				IsDdashing = false;
        dashTime = startDashTime;
      }
      else
      {
				if (!IsDdashing)
					rb.velocity = Vector3.zero;
          playerCollider.enabled = true;
				
        dashTime -= Time.fixedDeltaTime;

			if (IsDdashing)
			{
       
        playerCollider.enabled = false;

        if (Input.GetKey(KeyCode.RightArrow))
        {
          rb.velocity = Vector3.right * dashSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
          rb.velocity = Vector3.left * dashSpeed;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
          rb.velocity = Vector3.forward * dashSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
          rb.velocity = Vector3.back * dashSpeed;
        }
			}

      }
  }
}


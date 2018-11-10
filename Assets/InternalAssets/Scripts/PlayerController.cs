using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNum;

    private float sphereLoadingTime;
    private float maxSphereLoadingTime;
    private bool sphereIsLoading;
    private float maxSphereDeathCooldownTime;
    private float sphereDeathCooldown;
    private float speed;

    private GameObject deathSphere;
    private Vector3 initalDeathSphereScale;

    public int score;
    public int gatheredSouls;

    public bool isStunned;
    public float stunTimeLeft;
    public float stunTime;
    

	// Use this for initialization
	void Start () {
        playerNum = 1;
        maxSphereDeathCooldownTime = 0.5f;
        sphereDeathCooldown = 0f;
        sphereIsLoading = false;
        sphereLoadingTime = 0f;
        maxSphereLoadingTime = 2f;
        speed = 5f;

        isStunned = false;
        stunTime = 0.5f;
        stunTime = 0f;
        

        deathSphere = transform.GetChild(0).gameObject;
        initalDeathSphereScale = deathSphere.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isStunned)
        {



            if (Input.GetKey(KeyCode.UpArrow))
                transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;

            if (Input.GetKey(KeyCode.DownArrow))
                transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * speed;

            if (Input.GetKey(KeyCode.RightArrow))
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;

            if (Input.GetKey(KeyCode.LeftArrow))
                transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * speed;


            //Managing the death sphere
            if (sphereDeathCooldown >= maxSphereDeathCooldownTime)
            {

                if (Input.GetKey(KeyCode.Space) && sphereLoadingTime < maxSphereLoadingTime)
                {


                    //reducing speed while attacking
                    speed = 2.5f;

                    sphereIsLoading = true;
                    deathSphere.transform.localScale += new Vector3(deathSphere.transform.localScale.x, deathSphere.transform.localScale.y, deathSphere.transform.localScale.z) * Time.deltaTime;
                    sphereLoadingTime += Time.deltaTime;
                }
                else if (sphereIsLoading)
                {
                    //getting PNJs in the death sphere area before killing them
                    Collider[] sphereDeathCollider = Physics.OverlapSphere(transform.position, deathSphere.GetComponent<SphereCollider>().radius * deathSphere.transform.localScale.x);
                    for (int i = 0; i < sphereDeathCollider.Length; i++)
                    {

                        if (sphereDeathCollider[i].tag == "PNJ")
                        {
                            
                            Destroy(sphereDeathCollider[i].gameObject);
                        }
                    }

                    speed = 5f;
                    //reseting values
                    sphereIsLoading = false;
                    sphereDeathCooldown = 0;
                    sphereIsLoading = false;
                    sphereLoadingTime = 0f;
                    deathSphere.transform.localScale = initalDeathSphereScale;


                }
            }


            //gestion du cooldown de la deathsphere
            if (sphereDeathCooldown <= maxSphereDeathCooldownTime)
            {
                sphereDeathCooldown += Time.deltaTime;
            }
            else
            {
                sphereDeathCooldown = maxSphereDeathCooldownTime;
            }

            Collider[] soulCatcherCollider = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius * transform.localScale.x);
            for (int i = 0; i < soulCatcherCollider.Length; i++)
            {
                if (soulCatcherCollider[i].tag == "Soul")
                {
                    gatheredSouls += 1;
                    Debug.Log("GathereSouls : " + gatheredSouls);
                    Destroy(soulCatcherCollider[i].gameObject);
                }

                if (soulCatcherCollider[i].tag == "Base" + playerNum && gatheredSouls != 0)
                {

                    score += gatheredSouls;
                    Debug.Log("Score : " + score);
                    gatheredSouls = 0;
                }
            }
        }

    }
}

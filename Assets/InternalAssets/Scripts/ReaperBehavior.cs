using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ReaperBehavior : MonoBehaviour
{
    public int gatheredSouls; // must be public as it's modified by BaseBehavior
    public float maxSphereLoadingTime = 2f;
    public GameObject soul;
    public AudioClip sphereReleaseSfx;

    private float sphereLoadingTime;
    private float maxSphereDeathCooldownTime;
    private float sphereDeathCooldown;
    private GameObject deathSphere;
    private Vector3 initalDeathSphereScale;
    private PlayerMovementsBehavior pmb;
    private PlayerIdDistributor pid;

    // Use this for initialization
    void Start()
    {
        gatheredSouls = 0;
        maxSphereDeathCooldownTime = 0.5f;
        sphereDeathCooldown = 0f;
        sphereLoadingTime = 0f;

        pmb = GetComponent<PlayerMovementsBehavior>();
        pid = GetComponent<PlayerIdDistributor>();

        deathSphere = transform.Find("DeathSphere").gameObject;
        deathSphere.SetActive(false);
        initalDeathSphereScale = deathSphere.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pmb.IsStunned)
        {
            // About to Attack
            if (InputsManager.playerInputsDictionary[pid.PlayerId].AttackSphereDown 
                && sphereDeathCooldown >= maxSphereDeathCooldownTime 
                && sphereLoadingTime < maxSphereLoadingTime
                && !pmb.IsAttacking)
            {
                deathSphere.SetActive(true);
                pmb.IsAttacking = true; // pin down player while attacking (in PlayerMovementsBehavior.cs)
                deathSphere.transform.localScale += new Vector3(deathSphere.transform.localScale.x, deathSphere.transform.localScale.y, deathSphere.transform.localScale.z) * Time.deltaTime;
                sphereLoadingTime += Time.deltaTime;
            }
            // Attacking 
            else if (pmb.IsAttacking)
            {
                deathSphere.transform.localScale += new Vector3(deathSphere.transform.localScale.x, deathSphere.transform.localScale.y, deathSphere.transform.localScale.z) * Time.deltaTime;
                sphereLoadingTime += Time.deltaTime;

                // Releasing the attack
                if (!InputsManager.playerInputsDictionary[pid.PlayerId].AttackSphereDown || sphereLoadingTime >= maxSphereLoadingTime)
                {
                    // Play Sfx

                    //getting PNJs in the death sphere area before killing them
                    Collider[] sphereDeathCollider = Physics.OverlapSphere(transform.position, deathSphere.GetComponent<SphereCollider>().radius * deathSphere.transform.localScale.x);
                    for (int i = 0; i < sphereDeathCollider.Length; ++i)
                    {
                        if (sphereDeathCollider[i].tag == "PNJ")
                        {
                            Vector3 victimPosition = sphereDeathCollider[i].gameObject.transform.position;
                            // Destroy PNJ
                            Destroy(sphereDeathCollider[i].gameObject);
                            // Spawn Souls
                            soul.transform.position = victimPosition;
                            Instantiate(soul);
                        }
                    }

                    ResetSphere();
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
        }
        else
        {
            ResetSphere();
        }
    }

    private void ResetSphere()
    {
        deathSphere.SetActive(false);
        pmb.IsAttacking = false;
        sphereDeathCooldown = 0;
        sphereLoadingTime = 0f;
        deathSphere.transform.localScale = initalDeathSphereScale;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            gatheredSouls += 1;
            Destroy(collision.gameObject);
        }
    }
}

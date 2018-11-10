using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// A CHANGER : Cylinder 0, 1 et 2
public class PNJBehavior : MonoBehaviour
{

    public Vector3 positionDestination;
    public NavMeshAgent agent;
    Vector3 pointJoueur1;
    Vector3 pointJoueur2;
    Vector3 pointJoueur3;
    Rigidbody rb;
    public float targetTime = 3f;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        pointJoueur1 = GameObject.Find("Cylinder").transform.position;
        pointJoueur2 = GameObject.Find("Cylinder (1)").transform.position;
        pointJoueur3 = GameObject.Find("Cylinder (2)").transform.position;

        positionDestination = getPointRandom();
    }

    // Update is called once per frame
    void Update()
    {
        // timer


        if (rb.velocity.magnitude < 0.1f)
        {
            if (targetTime <= 0)
            {
                agent.SetDestination(getPointRandomValide());
                return;
            }
            targetTime -= Time.deltaTime;
        }
        else
        {
            targetTime = 3f;
        }
        Vector3 positionPNJ = agent.transform.position;
        float distanceJoueur1 = Vector3.Distance(pointJoueur1, positionPNJ);
        float distanceJoueur2 = Vector3.Distance(pointJoueur2, positionPNJ);
        float distanceJoueur3 = Vector3.Distance(pointJoueur3, positionPNJ);
        if (distanceJoueur1 < 2 || distanceJoueur2 < 2 || distanceJoueur3 < 2)
        {
            if (distanceJoueur1 < 2)
            {
                float x = pointJoueur1.x + (positionPNJ.x - pointJoueur1.x) * 2;
                float z = pointJoueur1.z + (positionPNJ.z - pointJoueur1.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }
            if (distanceJoueur2 < 2)
            {
                float x = pointJoueur2.x + (positionPNJ.x - pointJoueur2.x) * 2;
                float z = pointJoueur2.z + (positionPNJ.z - pointJoueur2.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }
            if (distanceJoueur3 < 2)
            {
                float x = pointJoueur3.x + (positionPNJ.x - pointJoueur3.x) * 2;
                float z = pointJoueur3.z + (positionPNJ.z - pointJoueur3.z) * 2;
                positionDestination = new Vector3(x, 1, z);
            }
            // Si il est impossible d'éviter l'eventuel conflit du point de destination
            NavMeshHit hit2;
            if (!NavMesh.SamplePosition(positionDestination, out hit2, 1.0f, NavMesh.AllAreas))
            {
                Debug.Log("IMPOSSIBLE D'EVITER LE CONFLIT");
                positionDestination = getPointRandomValide();
            }
            else
            {   // évite l'éventuel conflit (ne fait rien si pas de confilt)
                bool ok = NavMesh.SamplePosition(positionDestination, out hit2, 1.0f, NavMesh.AllAreas);
                positionDestination = hit2.position;
            }
        }

        //Debug.Log("Distance :" + Vector3.Distance(positionDestination, positionPNJ));
        if (Vector3.Distance(positionDestination, positionPNJ) < 2)
        {
            Debug.Log("Changement de point !");
            positionDestination = getPointRandomValide();
        }
        agent.SetDestination(positionDestination);
    }
    public Vector3 getPointRandom()
    {
        float z = Random.Range(-20, 20);
        float x = Random.Range(-20, 20);
        return new Vector3(x, 0, z);
    }

    public Vector3 getPointRandomValide()
    {
        NavMeshHit hit;
        bool result = false;
        int cpt = 0;
        Vector3 pointRandom = getPointRandom();
        while (result == false && cpt < 100)
        {
            result = NavMesh.SamplePosition(pointRandom, out hit, 1.0f, NavMesh.AllAreas);
            if (result)
            {
                pointRandom = hit.position;
            }
            else
            {
                pointRandom = getPointRandom();
            }
            cpt++;
        }

        return pointRandom;
    }
}
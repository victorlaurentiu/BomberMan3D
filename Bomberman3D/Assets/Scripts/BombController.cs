﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    // [HideInInspector] means that the attribute will be hidden in the Editor
    private float bombRange;
    public void SetBombRange(float newBombRange)
    {
        bombRange = newBombRange;
    }

    public ParticleSystem fuse;
    public ParticleSystem explosion;

    // SerializeField means that Unity will create a Field in the Editor so you can modify the attribute
    // click on Bomb prefab and you will see the field under "BombController" Component
    [SerializeField]
    private float explosionDelay;

    private BoxCollider bc;

    private ParticleSystem fuseObject;

    // Use this for initialization
    void Start () {
        bc = GetComponent<BoxCollider>();
        bc.isTrigger = true;

        //Vector3 fuseOffset = new Vector3(0, 1, 0);
        //fuseObject = Instantiate(fuse, gameObject.transform.position + fuseOffset, fuse.transform.rotation);

        // Explode will be called after explosionDelay seconds
        Invoke("Explode", explosionDelay);
    }

    private void OnTriggerExit(Collider other)
    {
        // if the Player exits the Trigger, the Trigger becomes a Collider
        if (other.gameObject.tag.Equals("Player"))
            bc.isTrigger = false;
    }

    // destroy the object and spawn explosions!!!!
    void Explode()
    {
        // x = E, z = N => directions = {N, E, S, W}
        Vector3[] directions = { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0) };

        foreach (var direction in directions)
        {
            // Debugging
            //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            //Vector3 newPos = gameObject.transform.position + direction * bombRange;
            //sphere.transform.position = newPos;
            //
            //Debug.Log("gameobject");
            //Debug.Log(gameObject.transform.position);
            //
            //Debug.Log("result");
            //Debug.Log(newPos);

            // retains objects' info
            RaycastHit hitInfo;

            // If there is something in that direction
            if (Physics.Raycast(gameObject.transform.position, direction, out hitInfo, bombRange))
            {
                // Display objects' tag
                //Debug.Log(hitInfo.transform.tag);

                if (hitInfo.transform.tag == "Breakable")
                {
                    Destroy(hitInfo.transform.gameObject);
                }
                else if (hitInfo.transform.tag == "Player")
                {
                    //Debug.Log("TODO: End Game");
                }
            }

        }

        Destroy(fuseObject);
        Destroy(gameObject);
    }

    void InstantiateExplosion(RaycastHit hitInfo, Vector3 startPos)
    {

    }
}

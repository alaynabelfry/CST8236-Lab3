using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplodeWall : MonoBehaviour {

    public AudioSource explodingNoise;
    public List<ParticleSystem> particles;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.name +" collision starting");
        if (explodingNoise != null)
        {
            explodingNoise.Play();
        }
        if (particles.Count > 0)
        {
            foreach(ParticleSystem pSystem in particles)
            {

                pSystem.transform.position = collider.transform.position;
                pSystem.Play();
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log(collider.gameObject.name + " collision in progress");
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider.gameObject.name + " collision ending");

    }
}

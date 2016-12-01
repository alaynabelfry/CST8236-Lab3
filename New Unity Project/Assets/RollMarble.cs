using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RollMarble : MonoBehaviour
{
    public ParticleSystem[] pSystems;
    public float explosionRadius = 5.0f;
    public float explosionPower = 10.0f;
    public bool resetPositionAfterExplosion = false;
    private int currentPSystem=0;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Vector3 direction = new Vector3();

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            direction.z = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            direction.z = -1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction.x = 1;
        }

        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, direction, out hit,direction.magnitude);

        if (didHit){
            Vector3 explosionPosition = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
            foreach(Collider collider in colliders)
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.AddExplosionForce(explosionPower, explosionPosition, explosionRadius, 3.0f);
                }
            }

            AudioSource explosionNoise = GetComponent<AudioSource>();
            explosionNoise.Play();

            pSystems[currentPSystem].transform.position = explosionPosition;
            Debug.Log(pSystems[currentPSystem].name + "is located at" + pSystems[currentPSystem].transform.position);
            pSystems[currentPSystem].Play();
            currentPSystem = (currentPSystem + 1) % pSystems.Length;
            transform.position += (hit.distance * direction.normalized);
            

        }
        else {
            transform.position += direction;
        }
    }
}
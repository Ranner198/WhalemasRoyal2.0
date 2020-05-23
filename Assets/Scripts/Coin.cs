using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();

    public ParticleSystem ps;

    public new AudioSource audio => GetComponent<AudioSource>();

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Destroy(gameObject, 2f);
            meshRenderer.enabled = false;
            // Start Partial System here            
            Debug.Log("Player Collected Coin");
            audio.Play();
        }
    }
}

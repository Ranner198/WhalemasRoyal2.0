using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOffScreen : MonoBehaviour
{
    public GameObject player;
    public Renderer rend;
    public void Start()
    {
        InvokeRepeating("CheckVisible", 3f, .1f);
    }

    public void CheckVisible()
    {
        print(Camera.main.gameObject);
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x < 0)
        {
            print("Bruh I cant see");
            GameManager.instance.players.Remove(player.GetComponent<Whaleburt>());
            Destroy(player, 1f);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehavior : MonoBehaviour
{
    public int index;    
    public GameObject hat;
    public List<GameObject> Hats = new List<GameObject>();
    public Whaleburt whaleburt;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (whaleburt.isBot)
                return;

            if (hat != null)
                Destroy(hat);

            // Check for index
            if (index < Hats.Count - 1)
                index++;
            else
                index = 0;

            if (Hats[index] != null)
            {
                GameObject newHat = Instantiate(Hats[index], transform.position, Quaternion.identity);
                newHat.transform.parent = transform;
                newHat.transform.localRotation = Quaternion.Euler(-90, 0, -90);
                hat = newHat;
            }
        }
    }

    public void GenerateRandomHat()
    {
        index = Random.Range(0, Hats.Count - 1);
        if (Hats[index] != null)
        {
            GameObject newHat = Instantiate(Hats[index], transform.position, Quaternion.identity);
            newHat.transform.parent = transform;
            newHat.transform.localRotation = Quaternion.Euler(-90, 0, -90);
            hat = newHat;
        }
    }
}

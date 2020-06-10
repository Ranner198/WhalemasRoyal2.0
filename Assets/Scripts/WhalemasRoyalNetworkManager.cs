using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;

public class WhalemasRoyalNetworkManager : NetworkManagerBehavior
{
    public static WhalemasRoyalNetworkManager instance;
    public float startTimer = 9.4f;

    private void Awake()
    {
        instance = this;
    }

    public void OnNetworkStart()
    {
        networkObject.timer = startTimer;
    }

    public void Update()
    {
        if (!networkObject.IsServer)
        {
            startTimer = networkObject.timer;
            return;
        }
                        
        if (startTimer > -1)
            startTimer -= Time.deltaTime;

        networkObject.timer = startTimer;
    }

    public float GetTimer()
    {
        return networkObject.timer;
    }
}

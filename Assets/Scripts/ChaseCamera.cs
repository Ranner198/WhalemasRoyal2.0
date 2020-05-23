using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    public Vector3 offset;
    public GameObject background;
    public float parallaxSpeed;
    private Material material;
    private Vector3 backgroundOffset;

    private void Start()
    {
        backgroundOffset = background.transform.localPosition;
        material = background.GetComponent<MeshRenderer>().material;
    }

    void LateUpdate()
    {
        Vector3 LeaderPosition = GameManager.instance.GetLeader();        
        transform.position = new Vector3(transform.position.x, LeaderPosition.y + offset.y, LeaderPosition.z + offset.z);
        Vector3 backgroundScroll = (transform.position * parallaxSpeed);
        backgroundScroll.y = 0;
        material.mainTextureOffset += new Vector2(backgroundScroll.x, backgroundScroll.y);
    }
}

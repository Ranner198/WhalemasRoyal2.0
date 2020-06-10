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
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPoint.x < -.1f)
        {
            GameManager.instance.players.Remove(player.GetComponent<Whaleburt>());
            Destroy(player, 1f);
        }
    }
}

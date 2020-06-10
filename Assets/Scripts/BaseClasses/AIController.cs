using UnityEngine;

public class AIController : MonoBehaviour
{
    public Whaleburt whaleburt;

    public LayerMask collisionDetection;

    public float timer;

    public void Update()
    {
        Vector3 bottom = transform.position;
        bottom.y += .03f;
        Debug.DrawRay(bottom, transform.forward * 8, Color.red, Time.deltaTime, false);
        if (Physics.Raycast(bottom, transform.forward, out RaycastHit hit, 8f, collisionDetection))
        {
            if (hit.transform.name == "Tree(Clone)")
            {
                whaleburt.Jump();
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            if (Random.Range(0f, 1f) > .5f)
                whaleburt.MoveUp();
            else
                whaleburt.MoveDown();

            timer = Random.Range(1, 10);
        }
    }
}

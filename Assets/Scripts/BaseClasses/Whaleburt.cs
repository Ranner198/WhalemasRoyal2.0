using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Whaleburt : MonoBehaviour
{
    /// <summary>
    /// The Current Players Index
    /// </summary>
    public Positions positions = new Positions();
    /// <summary>
    /// The current Players Index in the GameManager
    /// </summary>
    public int Index;
    /// <summary>
    /// The Players Current Rigidbody Velocity
    /// </summary>
    public float Speed
    {        
        get
        {
            if (rb != null)
                return rb.velocity.magnitude;
            else
            {
                Debug.Log("Error: Rigidbody not yet assigned");
                return -1;
            }
        }        
    }
    /// <summary>
    /// The amount of jump force the player has
    /// </summary>
    public float JumpForce = 1000;
    /// <summary>
    /// The Players overall accceleration overall
    /// </summary>
    public float Acceleration = 12;
    /// <summary>
    /// The Players Rigidbody Reference
    /// </summary>
    public Rigidbody rb;
    /// <summary>
    /// The Players Animator Controller reference
    /// </summary>
    public Animator anim;
    /// <summary>
    /// The Raycast layer mask to check if player is grounded
    /// </summary>
    public LayerMask lm;
    /// <summary>
    /// Boolean to check wheather or not the player is grounded
    /// </summary>
    public bool Grounded;
    /// <summary>
    /// Speed Limiter for Whaleburt to limit him from going too fast
    /// </summary>
    public float terminalVelocity;    
    /// <summary>
    /// Accelerate the player based off of the Players Acceleration
    /// </summary>
    public void SpeedBoost()
    {
        rb.velocity *= Acceleration/9;
    }
    /// <summary>
    /// Sound Clips
    /// </summary>
    public AudioClip Jump;
    /// <summary>
    /// Audio Source Component
    /// </summary>
    public new AudioSource audio => GetComponent<AudioSource>();
    /// <summary>
    /// When the user last up in Up or Down Input
    /// </summary>
    public float inputTime;
    /// <summary>
    /// When the player has hit something
    /// </summary>
    public void SlowDown()
    {
        rb.velocity /= (Acceleration / 3);
    }

    public void Trick()
    {
        anim.SetTrigger("Flip");
    }

    public void Update()
    {
        if (Grounded && !rb.isKinematic)
        {
            // Movement Controls
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (positions.Index < positions.XPos.Count - 1)
                {
                    positions.Index++;
                    inputTime = Time.time;
                    rb.velocity += Vector3.up * 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (positions.Index > 0)
                {
                    positions.Index--;
                    inputTime = Time.time;
                    rb.velocity += Vector3.up * 2;
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3(positions.XPos[positions.Index], transform.position.y, transform.position.z), Time.time / (inputTime + 200));
    }

    public void FixedUpdate()
    {
        // Ground Check
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, .1f))
        {
            //Debug.Log("I hit: " + hit.collider.name);
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }
        Debug.DrawRay(transform.position, Vector3.down * 1, Color.red, Time.deltaTime, false);

        if (rb.velocity.z < terminalVelocity)
            rb.AddForce(Vector3.forward * Acceleration * Time.deltaTime * 60);

        //else
        //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, terminalVelocity);

        // If the player is grounded
        if (Grounded && !rb.isKinematic)
        {
            // Check grounded
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(rb.velocity.x, JumpForce / 1000, rb.velocity.z);
                audio.PlayOneShot(Jump);
            }
        }
    }
}

[System.Serializable]
public class Positions
{
    public int Index;
    public List<float> XPos = new List<float>();
}


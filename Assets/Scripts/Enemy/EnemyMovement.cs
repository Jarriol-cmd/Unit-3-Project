using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    Rigidbody2D rb;
    bool isGrounded;
    public bool isFacingRight;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    float xvel, yvel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xvel = 6;
        yvel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        yvel = rb.linearVelocity.y;


        if (xvel < 0)
        {
            if (ExtendedRayCollisionCheck(-1, 0) == false && xvel < 0)
            {
                Flip();
                xvel = -xvel;

            }
        }


        if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(1, 0) == false && xvel > 0)
            {
                Flip();
                xvel = -xvel;

            }
        }

        if (ExtendedplayerCollisionCheck(0, 1) == true)
        {
            Destroy(gameObject);
        }


            rb.linearVelocity = new Vector3(xvel, yvel, 0);
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localscale = transform.localScale;
        localscale.x *= -1f;
        transform.localScale = localscale;
    }


    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 2.5f;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
        }

        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;
    }

    public bool ExtendedplayerCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, playerLayer);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            hitColor = Color.green;
            hitSomething = true;
        }

        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;
    }
}

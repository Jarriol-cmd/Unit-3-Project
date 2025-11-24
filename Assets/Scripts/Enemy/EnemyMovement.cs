using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    Rigidbody2D rb;
    float timer;
    bool isGrounded;
    public bool isFacingRight;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public GameObject weapon;
    float xvel, yvel;
    public Animator anim;
    public float Dtimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xvel = 4;
        yvel = 0;
        timer = 4;
        anim = GetComponent<Animator>();
        Dtimer = 2;

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walking", false);
        anim.SetBool("attacking", false);
        anim.SetBool("Dying", false);

        yvel = rb.linearVelocity.y;
        timer -= Time.deltaTime;

        if (xvel < 0)
        {
            if (ExtendedRayCollisionCheck(-1, 0) == false && xvel < 0)
            {
                Flip();
                xvel = -xvel;
                anim.SetBool("walking", true);
            }
        }


        if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(1, 0) == false && xvel > 0)
            {
                Flip();
                xvel = -xvel;
                anim.SetBool("walking", true);
            }
        }

        if (ExtendedplayerCollisionCheck(0, 2) == true || ExtendedplayerCollisionCheck(0, 1) == true)
        {
            xvel = 0;

        }

        if (xvel == 0)
        {
            Dtimer -= Time.deltaTime;
            anim.SetBool("Dying", true);

            if (Dtimer <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (xvel != 0)
        {

            if (timer <= 0)
            {

                if (xvel >= 0)
                {
                    GameObject clone;
                    clone = Instantiate(weapon, transform.position, Quaternion.identity);

                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                    rb.linearVelocity = new Vector2(15, 0);

                    rb.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);

                    rb.transform.Rotate(new Vector3(0, 0, 315));
                }

                if (xvel <= 0)
                {
                    GameObject clone;
                    clone = Instantiate(weapon, transform.position, Quaternion.identity);

                    Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                    rb.linearVelocity = new Vector2(-15, 0);

                    rb.transform.position = new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z);

                    rb.transform.Rotate(new Vector3(0, 0, 135));
                }
                anim.SetBool("attacking", true);
                timer = 4;
            }
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
        float rayLength = 2f;
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

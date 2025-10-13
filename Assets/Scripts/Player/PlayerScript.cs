using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    Rigidbody2D RB;
    HelperScript helperScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xvel = RB.linearVelocity.x;
        float yvel = RB.linearVelocity.y;


        if(Input.GetKey("d"))
        {
            xvel = 4;
        }

        if (Input.GetKey("a"))
        {
            xvel = -4;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 8;
        }

        if (Input.GetKeyDown("s"))
        {
            yvel = -yvel;
            xvel = -xvel;
        }

        if (ExtendedenemyCollisionCheck(-0.5f, 0.5f) == true || ExtendedenemyCollisionCheck(0.5f,0.5f) == true)
        {
            Destroy(gameObject);
        }

        /*if (xvel > 0)
        {
            helperScript.DoFlipObject(true);
        }

        if (xvel < 0)
        {
            helperScript.DoFlipObject(false);
        }

        */
        RB.linearVelocity = new Vector3(xvel, yvel, 0);

        GroundCheck();
       
    }

    void GroundCheck()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        Debug.DrawRay(position, direction, Color.black);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
            Debug.DrawRay(position, direction, Color.green);
        }

        else if (hit.collider == null)
        {
            isGrounded = false;
            Debug.DrawRay(position, direction, Color.darkRed);
        }
    }

    public bool ExtendedenemyCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 1f;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, enemyLayer);

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

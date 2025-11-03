using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    public bool isGrounded;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    Rigidbody2D RB;
    HelperScript helperScript;
    public GameObject weapon;
    public Animator anim;
    public Transform respawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        helperScript = gameObject.AddComponent<HelperScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walking", false);
        anim.SetBool("jumping", false);
        anim.SetBool("attacking", false);
        anim.SetBool("Dying", false);

        float xvel = RB.linearVelocity.x;
        float yvel = RB.linearVelocity.y;


        if(Input.GetKey("d"))
        {
            xvel = 4;
            anim.SetBool("walking", true);
            helperScript.DoFlipObject(false);
        }

        if (Input.GetKey("a"))
        {
            xvel = -4;
            anim.SetBool("walking", true);
            helperScript.DoFlipObject(true);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 8;
            anim.SetBool("jumping", true);
        }

        if(isGrounded == false)
        {
            anim.SetBool("jumping", true);
        }

        if (Input.GetKeyDown("s"))
        {
            yvel = -yvel;
            xvel = -xvel;
        }

        if (ExtendedenemyCollisionCheck(-0.5f, 0.5f) == true || ExtendedenemyCollisionCheck(0.5f,0.5f) == true)
        {
            
            respawnPlayer();
        }

        RB.linearVelocity = new Vector3(xvel, yvel, 0);

        GroundCheck();

       
        if (Input.GetKeyDown("t"))
        {
            anim.SetBool("attacking", true);

            if (xvel >= 0)
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, Quaternion.identity);

                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                rb.linearVelocity = new Vector2(15, 0);

                rb.transform.position = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);

                rb.transform.Rotate(new Vector3(0, 0, 315));
            }
            
            if (xvel <= 0)
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, Quaternion.identity);

                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

                rb.linearVelocity = new Vector2(-15, 0);

                rb.transform.position = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);

                rb.transform.Rotate(new Vector3(0, 0, 135));
            }
        }

    }

    void GroundCheck()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 2.0f;
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

    void respawnPlayer()
    {
        transform.position = respawnPoint.position;
        anim.SetBool("Dying", true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //movement fields
    //control values
    public float castDist = 0.5f;
    public float gravityScale = 1f;
    public float floatingScale = 0.75f;
    public float speed = 5;
    public float gravityFall = 5;
    public float jumpLimit = 5;

    //public float turnSpeed;
    float horizontalMove;

    //movement checks
    bool jump = false;
    public bool floating = false;
    [SerializeField]bool grounded;

    //external references
    public GameManager gameManager;

    //component fields
    Animator myAnimator;
    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //left and right movement
        horizontalMove = Input.GetAxis("Horizontal");
        if(gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        //jump check
        if (Input.GetButtonDown("Jump")&& grounded)
        {
            jump = true;  
        }

        if (Input.GetButton("Jump"))
        {
            floating = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            floating = false;
        }

       

        //animator conditions
        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            myAnimator.SetBool("walking", true);
            
        }
        else
        {
            myAnimator.SetBool("walking", false);
        }
    }

    private void FixedUpdate()
    {
        //moves the character
        float moveSpeed = horizontalMove * speed;

        //jump physics
        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }

        if(myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if(myBody.velocity.y <= 0 && floating)
        {
            myBody.gravityScale = floatingScale;
        }
        else if (myBody.velocity.y <= 0 && !floating)
        {
            myBody.gravityScale = gravityFall;
        }

        //raycasting
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        if (hit.collider != null && hit.transform.tag == "Ground")
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            gameManager.CandyGet();
        }
        if (collision.CompareTag("Death Plane"))
        {
            //gameManager.SpawnPlayer();
            Destroy(gameObject); 
        }
        if (collision.CompareTag("Checkpoint"))
        {
            if (gameManager.spawnerNumber < gameManager.spawners.Length)
            {
                gameManager.spawnerNumber++;

            }
            
            Debug.Log("erm?");
            collision.enabled = false;
        }
    }
}

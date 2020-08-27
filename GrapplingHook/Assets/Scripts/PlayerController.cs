using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class PlayerController : MonoBehaviour
{
    private Player player1;

    [SerializeField] Vector3 moveVector;
    [SerializeField] float moveSpeed;

    [SerializeField] GameObject target;
    [SerializeField] Rigidbody2D obj;

    [SerializeField] bool touchingObj;

    [SerializeField] Vector2 destination;

    [SerializeField] Rigidbody2D objToTeleport;






    Color colr;

    private static bool noTeleport; //stops from teleporting into gaps

    RaycastHit2D hit;

    [SerializeField] Collider2D hitCollider;

    string sceneName;

    public Animator anim;

    Rigidbody2D rb;

    private StateMachine stateMachine = new StateMachine();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player1 = ReInput.players.GetPlayer(0);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.transform.position);

        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

    }

    // Update is called once per frame
    void Update()
    {
        noTeleport = NoTeleportZone.dontTeleport;   //stops from teleporting into gaps

        hit = Physics2D.Linecast(transform.position, target.transform.position);

        GetInput();

        LineOfSight();
        TeleportObject();
        TeleportToTarget();

        hitCollider = hit.collider;

    }

    private void FixedUpdate()
    {
        if (player1.GetButton("Move"))
        {
            ProcessInput();
        }
        if (!player1.GetButton("Move"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("isRunning", false);
            Debug.Log("not moving");

        }

    }

    void GetInput()
    {
        moveVector.x = player1.GetAxis("Move Horizontal");

        moveVector.y = player1.GetAxis("Move Vertical");
    }

    void ProcessInput()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime * 0;

        if (moveVector.x > 0)
        {
            rb.velocity = transform.right * moveSpeed * Time.deltaTime;

            anim.SetBool("isRunning", true);
            Debug.Log("right moving");

        }
        if (moveVector.x < 0)
        {
            rb.velocity = -transform.right * moveSpeed * Time.deltaTime;

            anim.SetBool("isRunning", true);
            Debug.Log("left moving");

        }

    }

    void TeleportToTarget()
    {
        if (player1.GetButtonDown("Teleport") && !touchingObj) //teleport to cursor position if not touching anything

        {
            //Teleport
            transform.position = destination;
            destination = target.transform.position;
        }
        else if (player1.GetButtonDown("Teleport") && !touchingObj && hit.collider.CompareTag("NoTeleportZone")) //don't teleport if cursor is on designated "No Teleport Zone"
        {
            //noTeleport
            transform.position = destination;
            destination = target.transform.position;
        }
        else if (player1.GetButtonDown("Teleport") && touchingObj) //if cursor is touching an object switch places with it
        {
            //Teleport
            obj = hit.rigidbody;
            Vector2 temp = obj.transform.position;
            obj.transform.position = transform.position;
            destination = temp;

            transform.position = destination;

        }
    }

    void TeleportObject() //to teleport an object from one place to another
    {//yes1,no2,box3,enemy4
        if (player1.GetButtonDown("other.Teleport") && hit.collider != null && objToTeleport != null) //if an object has been selected to be teleported and something else is colliding with cursor linecast
        {
            if (hit.collider.CompareTag("Wall")) //if linecast is hitting a wall teleport object to the point of collision
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;

                TargetSwitchControl.targetColor = 2;

                objToTeleport.transform.position = hit.point;
            }
        }
        else if (player1.GetButtonDown("other.Teleport") && objToTeleport == null) // if nothing has been selected to be teleported
        {
            if (hit.collider.CompareTag("Object")) //if linecast is hitting an object, select as object to be teleported
            {
                objToTeleport = hit.rigidbody;
                colr = objToTeleport.GetComponent<SpriteRenderer>().color;

                objToTeleport.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        else if (player1.GetButtonDown("other.Teleport") && hit.collider == null) //teleport the object to the cursor position
        {
            objToTeleport.transform.position = target.transform.position;
            objToTeleport.GetComponent<SpriteRenderer>().color = colr;

            objToTeleport = null;
            Debug.Log("teleportobj");
        }

    }

    void LineOfSight() // controls destination of teleport thorugh linecast collisions
    {

        if (hit.collider != null) //if linecast is hitting something
        {
            Debug.Log("hit");
            if (noTeleport) // destination is equal to the edge of gap
            {

                //target.transform.position = target.transform.position;
                destination = hit.point;

                Debug.Log(hit.transform.name);
            }

            if (!noTeleport) // destination is equal to cursor position
            {
                destination = target.transform.position;
            }

            if(hit.collider.CompareTag("Gap Wall")) // destination equal to edge of gap wall
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;
                TargetSwitchControl.targetColor = 2;

                destination = transform.position;
            }
            else if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground")) //destination equal to collision point of linecast and wall/ground
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;
                TargetSwitchControl.targetColor = 2;

                destination = hit.point;
                Debug.Log(hit.transform.name);

                touchingObj = false;

            }

            else if (hit.collider.CompareTag("Object") || hit.collider.CompareTag("Enemy")) //set "touchingObj" to true see "TeleportObject" method
            {
                target.GetComponent<SpriteRenderer>().color = Color.magenta;

                if (hit.collider.CompareTag("Object"))
                    TargetSwitchControl.targetColor = 3;
                if (hit.collider.CompareTag("Enemy"))
                    TargetSwitchControl.targetColor = 4;

                Debug.Log(hit.transform.name);
                touchingObj = true;

            }


        }
        else //destination equal to cursor position & touchingObject = false
        {
            target.GetComponent<SpriteRenderer>().color = Color.green;

            destination = target.transform.position;
            Debug.Log("not hitting");
            TargetSwitchControl.targetColor = 1;

            touchingObj = false;

        }
        Debug.DrawLine(transform.position, target.transform.position, Color.red);

    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //reload scene on death
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}


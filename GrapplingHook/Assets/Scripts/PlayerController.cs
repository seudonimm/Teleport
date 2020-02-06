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

    private static bool noTeleport;

    RaycastHit2D hit;

    string sceneName;

    public Animator anim;

    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player1 = ReInput.players.GetPlayer(0);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, target.transform.position);

        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

        //this.GetComponent<PhysicsMaterial2D>().bounciness = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //this.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Extrapolate;

        noTeleport = NoTeleportZone.dontTeleport;   //stops from teleporting into gaps

        hit = Physics2D.Linecast(transform.position, target.transform.position);

        GetInput();
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

        LineOfSight();
        TeleportObject();
        TeleportToTarget();



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
            //transform.position += transform.right * moveSpeed * Time.deltaTime;

            anim.SetBool("isRunning", true);
            Debug.Log("right moving");

        }
        if (moveVector.x < 0)
        {
            rb.velocity = -transform.right * moveSpeed * Time.deltaTime;

            //transform.position += -transform.right * moveSpeed * Time.deltaTime;
            anim.SetBool("isRunning", true);
            Debug.Log("left moving");

        }

    }

    void TeleportToTarget()
    {
        if (player1.GetButtonDown("Teleport") && !touchingObj)
        {
            transform.position = destination;
            destination = target.transform.position;
        }
        else if (player1.GetButtonDown("Teleport") && !touchingObj && hit.collider.CompareTag("NoTeleportZone"))
        {
            transform.position = destination;
            destination = target.transform.position;
        }
        else if (player1.GetButtonDown("Teleport") && touchingObj)
        {
            obj = hit.rigidbody;
            Vector2 temp = obj.transform.position;
            obj.transform.position = transform.position;
            destination = temp;

            transform.position = destination;

        }
    }

    void TeleportObject()
    {
        if (player1.GetButtonDown("other.Teleport") && objToTeleport == null)
        {
            if (hit.collider.CompareTag("Object"))
            {
                objToTeleport = hit.rigidbody;
                colr = objToTeleport.GetComponent<SpriteRenderer>().color;

                objToTeleport.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        else if (player1.GetButtonDown("other.Teleport"))
        {
            objToTeleport.transform.position = target.transform.position;
            objToTeleport.GetComponent<SpriteRenderer>().color = colr;

            objToTeleport = null;
        }

    }

    void LineOfSight()
    {

        if (hit.collider != null)
        {
            Debug.Log("hit");
            if (noTeleport)
            {

                //target.transform.position = target.transform.position;
                destination = hit.point;

                Debug.Log(hit.transform.name);
            }

            if (!noTeleport)
            {
                destination = target.transform.position;
            }

            if(hit.collider.CompareTag("Gap Wall"))
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;

                destination = transform.position;
            }
            else if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Ground"))
            {
                target.GetComponent<SpriteRenderer>().color = Color.red;

                destination = hit.point;
                Debug.Log(hit.transform.name);

                touchingObj = false;
                //target.transform.position = hit.point;

            }

            else if (hit.collider.CompareTag("Object") || hit.collider.CompareTag("Enemy"))
            {
                target.GetComponent<SpriteRenderer>().color = Color.magenta;

                Debug.Log(hit.transform.name);
                touchingObj = true;
                //target.transform.position = hit.point;

            }


        }
        else
        {
            target.GetComponent<SpriteRenderer>().color = Color.green;

            destination = target.transform.position;
            Debug.Log("not hitting");

            touchingObj = false;

        }
        Debug.DrawLine(transform.position, target.transform.position, Color.red);

    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

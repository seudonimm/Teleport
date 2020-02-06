using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool moveRight;
    bool moveLeft;

    [SerializeField] float moveSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = false;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;

        }
        else if (moveLeft)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("DeathCollider"))
        {
            Destroy(this.gameObject);
        }
        if (!collision.gameObject.CompareTag("Ground"))
        {
            moveSpeed *= -1;
        }
    }
}

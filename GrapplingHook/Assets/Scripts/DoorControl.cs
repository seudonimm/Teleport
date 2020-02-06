using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField] GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        door.GetComponent<Collider2D>().enabled = true;
        door.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            door.GetComponent<Collider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().enabled = false;

            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            door.GetComponent<Collider2D>().enabled = true;
            door.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = Color.yellow;

        }
    }
}

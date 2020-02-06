using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathPit : MonoBehaviour
{
    [SerializeField] GameObject door;
    string level;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentLevel = SceneManager.GetActiveScene();

        level = currentLevel.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(level);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            door.GetComponent<Collider2D>().enabled = false;
            door.GetComponent<SpriteRenderer>().enabled = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "TestLevel1")
            {
                SceneManager.LoadScene("TestLevel2");
            }
            if (SceneManager.GetActiveScene().name == "TestLevel2")
            {
                SceneManager.LoadScene("TestLevel3");
            }
            if (SceneManager.GetActiveScene().name == "TestLevel3")
            {
                SceneManager.LoadScene("TestLevel4");
            }
            if (SceneManager.GetActiveScene().name == "TestLevel4")
            {
                SceneManager.LoadScene("TestLevel5");
            }
            if (SceneManager.GetActiveScene().name == "TestLevel5")
            {
                SceneManager.LoadScene("TestLevel6");
            }
            if (SceneManager.GetActiveScene().name == "TestLevel6")
            {
                SceneManager.LoadScene("End");
            }
        }
    }
}

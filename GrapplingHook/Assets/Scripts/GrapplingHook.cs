using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] Vector2 direction;

    [SerializeField] Sprite noTele;

    [SerializeField] Sprite yesTele;

    [SerializeField] Sprite boxTele;

    [SerializeField] Sprite enemyTele;

    [SerializeField] GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {//yes1,no2,box3,enemy4
        Vector3 worldMousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, player.transform.position.z);
        direction.Normalize();


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = worldMousePos;
        Debug.Log(angle);

        // controls look of cursor depending on state
        if (TargetSwitchControl.targetColor == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = yesTele;
        }
        else if (TargetSwitchControl.targetColor == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = noTele;
        }
        else if (TargetSwitchControl.targetColor == 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = boxTele;
        }
        else if (TargetSwitchControl.targetColor == 4)
        {
            this.GetComponent<SpriteRenderer>().sprite = enemyTele;
        }

    }
}

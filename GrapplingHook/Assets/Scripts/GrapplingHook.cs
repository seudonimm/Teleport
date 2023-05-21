using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the position and appearance of the target on screen
public class GrapplingHook : MonoBehaviour
{

    [SerializeField] Sprite noTele;

    [SerializeField] Sprite yesTele;

    [SerializeField] Sprite boxTele;

    [SerializeField] Sprite enemyTele;

    [SerializeField] GameObject player;

    [SerializeField] SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {//yes1,no2,box3,enemy4
        Vector2 worldMousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, player.transform.position.z);

        transform.position = worldMousePos;
        var pos = transform.position;

        pos.z = -3;
        transform.position = pos;

        // controls look of cursor depending on state
        if (TargetSwitchControl.targetColor == 1)
        {
            spr.sprite = yesTele;
        }
        else if (TargetSwitchControl.targetColor == 2)
        {
            spr.sprite = noTele;
        }
        else if (TargetSwitchControl.targetColor == 3)
        {
            spr.sprite = boxTele;
        }
        else if (TargetSwitchControl.targetColor == 4)
        {
            spr.sprite = enemyTele;
        }

    }
}

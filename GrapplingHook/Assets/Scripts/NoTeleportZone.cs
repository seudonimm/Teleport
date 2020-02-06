using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTeleportZone : MonoBehaviour
{
    [SerializeField] bool test;
    public static bool dontTeleport;

    [SerializeField] Collider2D noTeleportBox;

    [SerializeField] GameObject target;

    [SerializeField] Collider2D targetCol;

    // Start is called before the first frame update
    void Start()
    {
        dontTeleport = false;

        noTeleportBox = this.GetComponent<Collider2D>();

        target = GameObject.FindGameObjectWithTag("Target");
        targetCol = target.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        test = dontTeleport;

        dontTeleport  = noTeleportBox.IsTouching(targetCol);

        if (dontTeleport)
        {
            this.gameObject.layer = 0;
        }
        else if (!dontTeleport)
        {
            this.gameObject.layer = 2;
        }
    }


}

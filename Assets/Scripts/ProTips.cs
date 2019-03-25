using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProTips : MonoBehaviour
{
    
    private Animator anim;
    private bool space, shift, a, s;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("ArrowsPressed", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            space = true;
        if(Input.GetKeyDown(KeyCode.LeftShift))
            shift = true;
        if (space && shift)
        {
            anim.SetBool("SpacePressed", true);
        }

        if (Input.GetKeyDown(KeyCode.A))
            a = true;
        if (Input.GetKeyDown(KeyCode.S))
            s = true;

        if (a && s)
        {
            anim.SetBool("APressed", true);
        }
    }
}

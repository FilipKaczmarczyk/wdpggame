using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProTips : MonoBehaviour
{
    
    private Animator anim;

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
        {
            anim.SetBool("SpacePressed", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("APressed", true);
        }
    }
}

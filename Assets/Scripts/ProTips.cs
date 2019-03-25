using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProTips : MonoBehaviour
{
    private bool leftPressed, rightPressed;


    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void CheckArrowKeys()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPressed = true;
        }

        if (leftPressed == true && rightPressed == true)
        {
            anim.SetBool("ArrowsPressed", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckArrowKeys();
    }
}

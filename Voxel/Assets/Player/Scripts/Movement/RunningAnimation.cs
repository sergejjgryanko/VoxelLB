using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunningAnimation : MonoBehaviour
{
    public int direction;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(direction == 0)
        {
            animator.SetBool("running", true);
            animator.SetBool("runningback", false);
        }
        else if(direction == 1)
        {
            animator.SetBool("running", true);
            animator.SetBool("runningback", false);
        }
        else if(direction == 2)
        {
            animator.SetBool("running", true);
            animator.SetBool("runningback", false);
        }
        else if(direction == 3)
        {
            animator.SetBool("running", true);
            animator.SetBool("runningback", false);
        }
        else if(direction == 4)
        {
            animator.SetBool("running", true);
            animator.SetBool("runningback", false);
        }
        else if(direction == 5)
        {
            animator.SetBool("running", false);
            animator.SetBool("runningback", true);
        }
        else if(direction == 6)
        {
            animator.SetBool("running", false);
            animator.SetBool("runningback", true);
        }
        else if(direction == 7)
        {
            animator.SetBool("running", false);
            animator.SetBool("runningback", true);
        }
        else if(direction == 8)
        {
            animator.SetBool("running", false);
            animator.SetBool("runningback", false);
        }
    }
}

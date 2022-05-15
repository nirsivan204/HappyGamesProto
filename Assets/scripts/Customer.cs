using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Adjust the speed for the application.
    [SerializeField] float speed = 1.0f;

    // The target (cylinder) position.
    [SerializeField] Transform target;
    bool canMove = false;
    [SerializeField] Animator animator;
    // Start is called before the first frame update

    // Update is called once per frame

    public void startMoving()
    {
        canMove = true;
        animator.SetTrigger("Walk");
    }

    void Update()
    {
        if (canMove)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}

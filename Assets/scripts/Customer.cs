using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Adjust the speed for the application.
    [SerializeField] float speed = 1.0f;

    // The target (cylinder) position.
    [SerializeField] Transform counter;
    [SerializeField] Transform outOfScreen;
    [SerializeField] Transform parent;
    bool isServed = false;
    Transform target;

    bool canMove = false;
    [SerializeField] Animator animator;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Start()
    {
        target = counter;
    }

    public void startMoving()
    {
        canMove = true;
        animator.SetTrigger("Walk");
    }

    public void goodServe()
    {
        canMove = false;
        animator.SetTrigger("Success");
        target = outOfScreen;
        isServed = true;
        animator.ResetTrigger("Idle");
        Invoke("startMoving", 4);
    }

    public void badServe()
    {
        canMove = false;
        animator.SetTrigger("Failure");
        target = outOfScreen;
        isServed = true;
        animator.ResetTrigger("Idle");
        Invoke("startMoving", 4);
    }
    void Update()
    {
        if (canMove)
        {
            // Move our position a step closer to the target.
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            parent.transform.LookAt(target);
            if (!isServed && Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}

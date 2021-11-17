using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject sprite;

    Rigidbody rb;
    Animator anim;
    SpriteRenderer theSR;

    private Vector2 moveInput;
    private bool movingBackwards;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = sprite.GetComponent<Animator>();
        theSR = sprite.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);

        anim.SetFloat("moveSpeed", rb.velocity.magnitude);
        if(!theSR.flipX && moveInput.x < 0)
            theSR.flipX = true;
        else if(theSR.flipX && moveInput.x > 0)
            theSR.flipX = false;
        if (!movingBackwards && moveInput.y > 0)
            movingBackwards = true;
        else if (movingBackwards && moveInput.y < 0)
            movingBackwards = false;
        anim.SetBool("movingBackwards", movingBackwards);
    }
}

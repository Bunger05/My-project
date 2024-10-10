using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOtherPillar : MonoBehaviour
{
    
    public int counter = 0;

    SpriteRenderer spriteRenderer;
    Animator animator;
  
    Collider2D collide;
    Rigidbody2D rb;
    Transform player;
    private void Awake()
    {
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.disa = opaque;
        animator = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale *= new Vector2(-1, 1);
    }

    private void Update()
    {
        if (SetPillarBehavior.Instance.counter==1)
        {
            counter = 1;
            animator.SetTrigger("Instantiate");
            collide.isTrigger = false;
        }
        if (SetPillarBehavior.Instance.counter == 0)
        {
            counter = 0;
            collide.isTrigger = true;
            animator.SetTrigger("Back");
        }


    }



}

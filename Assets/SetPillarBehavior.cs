using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SetPillarBehavior : MonoBehaviour
{
    public static SetPillarBehavior Instance { get; private set; }
   public  int counter =0;
    public bool start;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Material opaque;
    public Material normal;
    Collider2D collide;
    Rigidbody2D rb;
    Transform player;
    private void Awake()
    {
        Instance = this;    
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.disa = opaque;
        animator=GetComponent<Animator>();
        collide= GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if(player.position.x-transform.position.x>1&&player.position.y-transform.position.y>-1.2f)
        {
            counter=1;
            animator.SetTrigger("Instantiate");
            collide.isTrigger = false;
            start = true;
        }
        if(!Damageable.Instance.IsAlive)
        {
            counter=0;
            collide.isTrigger = true;
            animator.SetTrigger("Back");
            start = false;
        }
        

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHitCollision : MonoBehaviour
{
    public static EnemyHitCollision Instance { get;  set; }
    public string enemyName;
    [SerializeField] public bool applyForce;
    ParticleSystem dust;
    PlayerController controller;
    public float timer;
    private bool _attackSucess;
    public ParticleSystem blood;
    Animator animator;
    [SerializeField] private bool _doBlood=true;
    public Transform player;
    [SerializeField] private bool _boss;
    public bool AttackSucess
    {
        get
        {
            return _attackSucess;
        }
        set
        {
            _attackSucess = value;
        }
    }
    private string _enemyTag;
    public string EnemyTag
    {
        get
        {
            return _enemyTag;
        }
        set
        {
            _enemyTag = value;
        }
    }


    Collider2D attackCollider;
    Rigidbody2D playerRb;
    


    private void Awake()
    {
        Instance = this;
        attackCollider = GetComponent<Collider2D>();
        animator=GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
        player= GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyName=collision.gameObject.name;
        //subtract health from enemy 
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable == null)
        {
            //EnemyTag = "";
        }
        if (damageable != null)
        {
            
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.3f);
           
            if(_doBlood)
            {
                blood.transform.position = new Vector2(player.position.x, player.position.y + 1f);
                blood.Play();
            }
            if(_boss)
            {
               
            }
            Damageable.Instance.Hit(20);
           
            AttackSucess = true;
            
            //Rigidbody2D enemyRb = damageable.GetComponentInParent<Rigidbody2D>();

        }
       


    }
    
    private void FixedUpdate()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

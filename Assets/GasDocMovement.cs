using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class GasDocMovement : MonoBehaviour
{
    public static GasDocMovement Instance { get; private set; }
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject pillar;
    [SerializeField]ParticleSystem dust;
    Transform playerTransform;
    EnemyDamageable damageable;
    private ThrowSaw throwSawInstance;

    int pillarCounter = 0;
    int flameCounter = 0;
    int counter = 0;
    private float attackCooldown;
    Attack attack;
    Animator animator;
    private bool _isFacingRight;
    private bool _canAttack = true;
    private bool _shortDistance;
    private bool _mediumDistance;
    private bool _longDistance;
    Rigidbody2D rb;
    [SerializeField] private float jetpack;
    private bool _projectileThrow;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);

            }
            _isFacingRight = value;
        }
    }
    CollisionDetector collisionDetector;
    private void Awake()
    {
        Instance = this;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        damageable = GetComponent<EnemyDamageable>();
        attack = playerTransform.GetComponent<Attack>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(13, 14);
        collisionDetector = GetComponentInChildren<CollisionDetector>();
    }
    private void Start()
    {
        
    }
    public void SwitchDirections()
    {
        if (playerTransform.position.x > transform.position.x)
        {

            IsFacingRight = false;


        }
        else
        {

            IsFacingRight = true;


        }

    }
    private void Update()
    {

        if(_projectileThrow)
        {
            StartCoroutine(ThrowProjectiles());
            
            _projectileThrow = false;
        }
        Damageable damageable = FindObjectOfType<Damageable>();
        if (damageable.IsInvincible2)
        {
            StartCoroutine(Freeze());
            
        }
        else
        {
            animator.speed = 1f;
        }
        SwitchDirections();
        
    }
    private IEnumerator Freeze()
    {
        rb.velocity = Vector2.zero; 
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        animator.SetTrigger("GoBack");
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
    private void FixedUpdate()
    {
       
        float distance = Vector2.Distance(transform.position, playerTransform.position);
       
        if (distance < 6)
        {
            _longDistance = false;
            _shortDistance = true;
            _mediumDistance = false;
        }
        if (distance < 20&&distance>6)
        {
            _shortDistance = false;
            _mediumDistance = true;
            _longDistance = false;
        }
        if (!_shortDistance && !_mediumDistance)
        {
            Debug.Log("hu9abuiFAHUAF");
            _longDistance = true;
            _shortDistance = false;
            _mediumDistance = false;
        }
        if(SetPillarBehavior.Instance.start)
        {
            if (_canAttack && counter == 0)
            {


                ChooseRandomAttackWithParams();
                StartCoroutine(TimeBetweenAttacks(attackCooldown));

            }
            Debug.Log("short: " + _shortDistance + " med " + _mediumDistance + "  long" + _longDistance);
        }
       

    }
    IEnumerator TimeBetweenAttacks(float time)
    {
        _canAttack = false;
        yield return new WaitForSeconds(time);
        _canAttack = true;

    }
    IEnumerator FlameThrower()
    {
        float time = UnityEngine.Random.Range(2.5f, 4);
        attackCooldown = 0.5f+time;
        animator.SetBool("FlameAttack", true);
        yield return new WaitForSeconds(time);
        animator.SetBool("FlameAttack", false);
    }

    private IEnumerator ThrowProjectiles()
    {
        Vector2 positionOffset = IsFacingRight ? new Vector2(-1, 0) : new Vector2(1, 0);

        for (int i = 0; i < 4; i++)
        {
            // Instantiate the projectile
            Instantiate(projectile, (Vector2)transform.position + positionOffset, Quaternion.identity);

            // Wait for 0.1 seconds before the next instantiation
            yield return new WaitForSeconds(0.1f);
        }

        animator.SetTrigger("Bonesaw");
    }
    public void EndOfThrowInstantiation()
    {
        
    }

    public void DashAndSwingBeginning()
    {
        if (IsFacingRight)
        {
            rb.velocity = new Vector2(-13f, jetpack);
        }
        else
        {
            rb.velocity = new Vector2(13f, jetpack);
        }

    }
    
    IEnumerator SpawnPillarIE()
    {
        animator.SetTrigger("Pillar");
        int random = Random.Range(2, 5);
        for (int i = 0; i < random; i++)
        {
            dust.transform.position = new Vector2(dust.transform.position.x, 19.05f);
            dust.Play();
            Vector2 previousDustPos = new Vector2(dust.transform.position.x, dust.transform.position.y + 2.5f);

            yield return new WaitForSeconds(0.9f); 

            Instantiate(pillar, previousDustPos, Quaternion.identity);

        }
    }
    public void GasDocTp()
    {
        int random = Random.Range(1, 3);
        if((random== 0&& playerTransform.position.x<-89)|| playerTransform.position.x < -174)
        {
            transform.position = new Vector2(playerTransform.position.x + 5, 20.8f);
        }
        else if(playerTransform.position.x>-174)
        {
            transform.position = new Vector2(playerTransform.position.x - 5, 20.8f);
        }
        
    }
    public void ChooseRandomAttackWithParams()
    {
        
        int random = Random.Range(1, 13);
        
        if  (_mediumDistance||_longDistance)
        {
           
            if (random <5)
            {
                flameCounter = 0;
                _projectileThrow = true;
                attackCooldown = 0.1f;
                pillarCounter = 0;
            }
            else if (random > 5 && random < 9)
            {
               
                animator.SetTrigger("DashAndSwing");
                flameCounter = 0;
                attackCooldown = 2.5f;
                pillarCounter = 0;

            }
            else if(random > 10 && random < 12)
            {
                flameCounter = 0;
                animator.SetTrigger("GasDocTP");
                attackCooldown = 0.5f;
                pillarCounter = 0;
            }
            else if (pillarCounter < 3)
            {
                flameCounter = 0;
                StartCoroutine(SpawnPillarIE());
                attackCooldown = 0.5f;
                pillarCounter++;
            }
            else
            {
                animator.SetTrigger("DashAndSwing");
                flameCounter = 0;
                attackCooldown = 2.5f;
                pillarCounter = 0;
            }

        }
        else
        {
            if ((random == 1 || random == 2||random == 5 || random == 6)&&flameCounter==0)
            {
                StartCoroutine(FlameThrower());
                pillarCounter = 0;
                flameCounter++;

            }
            else if (random > 2 && random < 5)
            {
                
                flameCounter = 0;
                _projectileThrow = true;
                attackCooldown = 0.1f;
                pillarCounter = 0;
            }
           
            else if (random > 10 && random < 12)
            {
                flameCounter = 0;
                animator.SetTrigger("GasDocTP");
                attackCooldown = 0.5f;
                pillarCounter = 0;
            }
            else if(pillarCounter<3)
            {
                flameCounter = 0;
                StartCoroutine(SpawnPillarIE());
                attackCooldown = 1f;
                pillarCounter++;
            }
            else
            {
                flameCounter = 0;
                animator.SetTrigger("GasDocTP");
                pillarCounter = 0;
            }


        }
       
    }
}

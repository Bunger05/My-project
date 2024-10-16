using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public static Damageable Instance { get; set; }
    Animator animator;
    private float invincibilityTimer;
    [SerializeField] private Material tempMaterial;
    private Material originalMaterial;
    [SerializeField] private float _maxHealth=100f;
    private Coroutine flashRoutine;
    SpriteRenderer spriteRenderer;
    private float _maxVirus=100;
    public float MaxVirus
    {
        get
        {
            return _maxVirus;
        }
        set
        {
            _maxVirus = value;
        }
    }
    [SerializeField] private float _virus=0;
    private bool _takeHalfDamage;
    public bool TakeHalfDamage
    {
        get
        {
            return _takeHalfDamage;
        }
        set
        {
            _takeHalfDamage = value;
        }
    }
    public float Virus
    {
        get
        {
            return _virus;
        }
        set
        {
            _virus = value;
        }
    }
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField] private float _health=100f;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;    
            if(_health<=0)
            {
                animator.SetBool("IsAlive", false);
                IsAlive = false;
            }
            else
            {
                IsAlive = true;
            }
        }
    }
    private bool _isAlive=true;
    private bool _isInvincible;
    public bool IsInvincible
    {
        get
        {
            return _isInvincible;
        }
        set
        {
            _isInvincible = value;
        }
    }
    private bool _isInvincible2;
    public bool IsInvincible2
    {
        get
        {
            return _isInvincible2;
        }
        set
        {
            _isInvincible2 = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            
          return _isAlive;
        }
        set
        {
            _isAlive = value;
        }
    }
    Rigidbody2D rb;
    private void Awake()
    {
        Instance = this;
        
        animator =GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }
    private void Update()
    {
        //Debug.Log("IsAlive: " + IsAlive);
        if(IsInvincible&&invincibilityTimer<0.5f)
        {
            invincibilityTimer += Time.deltaTime;
        }
        else
        {
            IsInvincible = false;
        }
        if(!IsAlive)
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void MakeInvincibleForBlock()
    {
        IsInvincible2 = true;
    }
    public void UnMakeInvincibleForBlock()
    {
        //IsInvincible2 = false;
        TakeHalfDamage = true;
    }
    public void EndBlockCompletely()
    {
        TakeHalfDamage = false;
    }
    public void Hit(float damage)
    {
        if(IsAlive&&(!IsInvincible||!IsInvincible2))
        {
           
            if(TakeHalfDamage)
            {
                damage *= 0.5f;
            }
            Health -= damage;
            Virus += (int)(damage/2);
            invincibilityTimer = 0;
            if (flashRoutine != null)
            {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(flashRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            flashRoutine = StartCoroutine(FlashRoutine());
            IsInvincible = true;
            
        }
        else if(IsInvincible2&&IsAlive)
        {
            Debug.Log("Heeeeerrrre");
            
            animator.SetBool("SoulParry",true);
            //StartCoroutine(TempFreeze());
        }
    }
    public void EndSoulParry()
    {
        animator.SetBool("SoulParry", false);
    }
    IEnumerator TempFreeze()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
    }
    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = tempMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(0.15f);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}

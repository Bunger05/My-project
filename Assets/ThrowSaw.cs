using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSaw : MonoBehaviour
{
       
    public float speed = 10f;
    public float lifetime = 5f;
    public LayerMask layerMask;
    int counter = 0;
    private Rigidbody2D rb;
    public ParticleSystem explosion;
    GasDocMovement objecada;

    private void Awake()
    {
      
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(8, 7, false);
        Physics2D.IgnoreLayerCollision(13,14,true);
        objecada = FindObjectOfType<GasDocMovement>();
    }

    private void Start()
    {
        Launch();
    }

    public void Launch()
    {
       

        Vector2 direction;

        if (!objecada.IsFacingRight) // Check the player's facing direction
        {
            direction = new Vector2(1, Random.Range(0.3f, 1f)); // Right direction
        }
        else
        {
            direction = new Vector2(-1, Random.Range(0.3f, 1f)); // Left direction
        }

        rb.velocity = direction * speed; // Set the velocity based on direction
         // Add torque for spinning effect
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {

        rb.AddTorque(25, ForceMode2D.Force);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            Debug.Log("HELELLALLALALLA");
            // Handle collision logic here (e.g., apply damage to player)


            explosion.transform.position = collision.transform.position;
            //Instantiate(explosion);
            // Destroy the projectile on collision
        }
        if (collision.gameObject.layer != 7)
        {


        }
       
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;
       

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = FindObjectOfType<Damageable>();
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            
            Debug.Log("HELELLALLALALLA");
            // Handle collision logic here (e.g., apply damage to player)

            damageable.Hit(10);
            explosion.transform.position = collision.transform.position;
            //Instantiate(explosion);
            // Destroy the projectile on collision
        }
      
        if (collision.gameObject.layer != 7)
        {


        }

        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.freezeRotation = true;


        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(gameObject);
    }
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("How fast the projectile moves")]
    [SerializeField]
    private float projectileSpeed;

    public Vector2 FireDirection { get; set; }
    public float Damage { get; set; }
    private void Update()
    {
        transform.Translate(FireDirection * (projectileSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Enemy"))
        {
            var enemyHealth = collision.GetComponent<EnemyHealth>();    
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
            }
        }
       Destroy(gameObject);
    }
}

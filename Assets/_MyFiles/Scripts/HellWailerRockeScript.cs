using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellWailerRockeScript : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionRadius = 3f;
    
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<EnemyScript>().TakeDamage(10);
                }
                if (nearbyObject.CompareTag("Boss"))
                {
                    collision.gameObject.GetComponent<EnemyScript>().TakeDamage(10);
                }
            }

            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

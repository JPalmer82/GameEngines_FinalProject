using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
     public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; //3 - 2 - 1 - 0 = Enemy has Died


        if(health <= 0)
        {
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            GetComponent<EnemyAIScript>().enabled = false;
        }
        else
        {
            animator.SetTrigger("Damage");

        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}

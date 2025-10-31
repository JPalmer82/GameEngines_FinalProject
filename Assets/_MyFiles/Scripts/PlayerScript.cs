using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Slider healthBar;
    public int currentHealth = 0;
    public int maxHealth = 100;
    public bool isDead = false;

    public Slider healthSlider;





    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead == false)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.value = currentHealth;
            Debug.Log(" takes " + damage + " damage");
        }

        if (currentHealth <= 0)
        {
            Debug.Log(" is dead!");

            SceneManager.LoadScene(2);
        }
    }
}

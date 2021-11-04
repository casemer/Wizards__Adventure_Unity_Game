using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth;
    public Rigidbody2D rb;
    public HealthBar healthBar;
    Vector3 cameraPos;
    public Camera mainCamera;

    public GameObject playerDeath;

    private bool facingRight;
    public Animator anim;

    public GameManager GameManager;

    private int fallBoundary = -20;

    void Start()
    {
        if(PlayerPrefs.GetInt("Hardcore") > 0)
        {
            currentHealth = PlayerPrefs.GetInt("Health");
        }
        else {
             currentHealth = maxHealth;
        }


        healthBar.SetHealth(currentHealth);

        if (mainCamera)
            cameraPos = mainCamera.transform.position;

    }

    void Update()
    {




        if (transform.position.y <= fallBoundary)
        {
            GameManager.KillPlayer(this);
            FindObjectOfType<AudioManager>().Play("WizardDeath");
            GameManager.EndGame();
        }

    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        PlayerPrefs.SetInt("Health", currentHealth);

        StartCoroutine(HurtAnim());

        if (currentHealth <= 0)
        {
            GameManager.KillPlayer(this);
            GameObject impact = (GameObject)Instantiate(playerDeath, transform.position, transform.rotation);
            Destroy(impact, .20f);
            FindObjectOfType<AudioManager>().Play("WizardDeath");
            GameManager.EndGame();
        }

    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
        PlayerPrefs.SetInt("Health", currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    IEnumerator HurtAnim()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(.20f);
        anim.SetBool("Hit", false);
    }


}
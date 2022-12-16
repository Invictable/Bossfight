using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive = true;
    public double startingHealth;
    public double currentHealth;
    public Image hurtImage;
    public AudioSource Hurt;
    public AudioSource Death;

    bool damageCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        currentHealth = startingHealth;
        hurtImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            //isAlive = false;
        }
    }

    public void AddHealth(double amount, bool overrideStarting)
    {
        if (overrideStarting || (amount + currentHealth) <= startingHealth)
        {
            currentHealth = currentHealth += amount;
        }
        else
        {
            currentHealth = startingHealth;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Damager") && !damageCooldown)
        {
            Attack thisAttack = other.gameObject.GetComponent<Attack>();
            doDamage(thisAttack.damage);
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Border"))
        {
            doDamage(currentHealth);
        }
    }

    public void doDamage(double damage)
    {
        currentHealth -= damage;
        hurtImage.enabled = true;
        StartCoroutine(flashRed());
        if (currentHealth > 0)
        {
            Hurt.Play();
        }
        else
        {
            Death.Play();
        }
    }

    IEnumerator DamageTimer(int i)
    {
        yield return new WaitForSeconds(i);
        damageCooldown = false;
    }

    IEnumerator flashRed()
    {
        yield return new WaitForSeconds(0.2f);
        hurtImage.enabled = false;
    }
}

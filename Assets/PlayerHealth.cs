using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive = true;
    public double startingHealth;
    public double currentHealth;

    bool damageCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        currentHealth = startingHealth;
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
            currentHealth -= thisAttack.damage;
            other.gameObject.SetActive(false);
            damageCooldown = true;
            StartCoroutine(DamageTimer(1)); // prevents damage stacking
        }
        else if(other.gameObject.CompareTag("Border"))
        {
            currentHealth = 0;
        }
    }

    IEnumerator DamageTimer(int i)
    {
        yield return new WaitForSeconds(i);
        damageCooldown = false;
    }
}

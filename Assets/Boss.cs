using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Monitor")]
    public double BossHealth;
    public bool isAlive = true;
    public bool canTakeDamage = true;

    [Header("Modify")]
    public float rotationSpeed;
    public double StartingHealth = 100;

    [Header("Data")]
    public GameObject proj1;
    public GameObject proj2;
    public GameObject spike;
    public GameObject Player;
    public GameObject projOrigin1;
    public GameObject projOrigin2;
    public GameObject spikeOrigin;

    private int rotationDirection = 1;
    bool shouldRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        BossHealth = StartingHealth;
        Invoke(nameof(switchRotation), 15);
        Invoke(nameof(firingHandler), 5);
       // invoke(nameof(switchSpacing), 20);
    }

    public void takeHealth(double amount)
    {
        if(canTakeDamage)
        {
            BossHealth -= amount;
        }
    }

    public void giveHealth(double amount)
    {
        if(amount+BossHealth <= StartingHealth)
        {
            BossHealth += amount;
        }
        else
        {
            BossHealth = StartingHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BossHealth <= 0)
        {
            isAlive = false;
        }

        if(shouldRotate && isAlive)
            transform.Rotate(new Vector3(0, rotationSpeed*rotationDirection, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.CompareTag("BossDamager"))
        {
            Attack thisAttack = other.gameObject.GetComponent<Attack>();
            takeHealth(thisAttack.damage);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    void fire(float force, GameObject projectile)
    {
        GameObject FiringObj;
        if (UnityEngine.Random.Range(0, 2) != 1)
        {
            FiringObj = projOrigin1;
        }
        else
        {
            FiringObj = projOrigin2;
        }

        GameObject proj = Instantiate(projectile, FiringObj.transform.position, Quaternion.identity);
        Rigidbody projRB = proj.GetComponent<Rigidbody>();


        Vector3 forceDirection = (Player.transform.position - FiringObj.transform.position).normalized;
        Vector3 rbforce = forceDirection * force/2 + transform.up*5/* * currentArmStrength*/;
        projRB.AddForce(rbforce, ForceMode.Impulse);
    }

    void switchRotation()
    {
        rotationDirection = (UnityEngine.Random.Range(0, 4)-2);
        Invoke(nameof(switchRotation), UnityEngine.Random.Range(2, 20));
    }

    void firingHandler()
    {
        // fire(30, proj1);
        StartCoroutine(DelayedLaunch(1));
        StartCoroutine(DelayedLaunch(2));
        int firingMode = UnityEngine.Random.Range(0, 4);
        switch (firingMode)
        {
            case 0:
                {
                    fire(70, proj2);
                    fire(70, proj2);
                    break;
                }
            case 1:
                {
                    shouldRotate = false;
                    StartCoroutine(DelayedBarrage(1));
                    StartCoroutine(DelayedBarrage(2));
                    StartCoroutine(DelayedBarrage(3));
                    Invoke(nameof(resetRotation), 5);
                    break;
                }
            case 2:
                { 
                    fire(70, proj1);
                    fire(70, proj1);
                    fire(70, proj1);
                    fire(70, proj1);
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(2));
                    break;
                }
            case 3:
                {
                    shouldRotate = false;
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(2));
                    StartCoroutine(DelayedLaunch(3));
                    StartCoroutine(DelayedLaunch(4));
                    StartCoroutine(DelayedLaunch(5));
                    Invoke(nameof(resetRotation), 6);
                    break;
                }
        }

        if (isAlive)
            Invoke(nameof(firingHandler), UnityEngine.Random.Range(0, 3)+2);
    }

    void resetRotation()
    {
        shouldRotate = true;
    }

    IEnumerator DelayedBarrage(int i)
    {
        yield return new WaitForSeconds(i);
        fire(70, proj1);
        fire(70, proj1);
        fire(70, proj1);
        fire(70, proj1);
    }

    IEnumerator DelayedLaunch(int i) // 0, 250, 110
    {
        yield return new WaitForSeconds(i);
        GameObject proj = Instantiate(spike, spikeOrigin.transform.position, Quaternion.identity);
        Rigidbody projRB = proj.GetComponent<Rigidbody>();
        Vector3 rbforce = spikeOrigin.transform.up * 100 + transform.up + transform.forward * -1/* * currentArmStrength*/;
        projRB.AddForce(rbforce, ForceMode.Impulse);
        int x = UnityEngine.Random.Range(-15, 15);
        int z = UnityEngine.Random.Range(-15, 15);
        Vector3 spawnLoc = new Vector3(x,350,z);
        GameObject proj2 = Instantiate(spike, spawnLoc, Quaternion.identity);
    }
}


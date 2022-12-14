using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header("Monitor")]
    public double BossHealth;
    public bool isAlive = true;
    public bool canTakeDamage = false;

    [Header("Modify")]
    public float rotationSpeed;
    public double StartingHealth = 100;

    [Header("Data")]
    public GameObject BossModel;
    public Text WinScreen;
    public GameObject proj1;
    public GameObject proj2;
    public GameObject spike;
    public GameObject Player;
    public GameObject projOrigin1;
    public GameObject projOrigin2;
    public GameObject projOrigin3;
    public GameObject projOrigin4;
    public GameObject spikeOrigin;
    public Material hurtable;
    public Material unhurtable;
    public Material dead;

    [Header("Sounds")]
    public AudioSource Death;
    public AudioSource Launch1;
    public AudioSource Launch2;
    public AudioSource Launch3;

    private int rotationDirection = 1;
    bool shouldRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        WinScreen.enabled = false;
        BossHealth = StartingHealth;
        Invoke(nameof(switchRotation), 14);
        Invoke(nameof(firingHandler), 5);
        Invoke(nameof(damagePhaseHandler), 16);
    }

    public void damage(double amount)
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
    int firingHandleCount = 1;
    bool once = false;
    void Update()
    {
        if((BossHealth <= 0 || !isAlive) && !once)
        {
            once = true;
            isAlive = false;
            WinScreen.enabled = true;
            BossModel.GetComponent<Renderer>().material = dead;
            Death.Play();
           // BossModel.transform.localScale -= new Vector3(5, 5, 5);
        }

        if (BossHealth <= StartingHealth / 2 && firingHandleCount <= 1) // makes it more intense at lower health
        {
            Invoke(nameof(firingHandler), 5);
            firingHandleCount++;
        }
        else if (BossHealth <= StartingHealth / 10 && firingHandleCount <= 2)
        {
            Invoke(nameof(firingHandler), 5);
            firingHandleCount++;
        }

        if (shouldRotate && isAlive)
            transform.Rotate(new Vector3(0, rotationSpeed*rotationDirection, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.CompareTag("BossDamager"))
        {
            Attack thisAttack = other.gameObject.GetComponent<Attack>();
            damage(thisAttack.damage);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    void fire(float force, GameObject projectile)
    {
        if (!isAlive)
            return;

        Launch2.Play();
        GameObject FiringObj;
        if (UnityEngine.Random.Range(0, 2) != 1)
        {
            FiringObj = projOrigin1;
        }
        else if (UnityEngine.Random.Range(0, 2) != 1)
        {
            FiringObj = projOrigin2;
        }
        else if (UnityEngine.Random.Range(0, 2) != 1)
        {
            FiringObj = projOrigin3;
        }
        else
        {
            FiringObj = projOrigin4;
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

    void damagePhaseHandler()
    {
        if(isAlive)
        {
            BossModel.GetComponent<Renderer>().material = hurtable;
            canTakeDamage = true;
            Invoke(nameof(resetDamagePhase), UnityEngine.Random.Range(5, 10));
        }
    }

    void resetDamagePhase()
    {
        if (isAlive)
        {
            BossModel.GetComponent<Renderer>().material = unhurtable;
            canTakeDamage = false;
            Invoke(nameof(damagePhaseHandler), UnityEngine.Random.Range(5, 30));
        }
    }

    void firingHandler()
    {
        // fire(30, proj1);
        int firingMode = UnityEngine.Random.Range(0, 7);
        fire(70, proj1);
        fire(70, proj1);
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
                    StartCoroutine(DelayedBarrage(1));
                    StartCoroutine(DelayedBarrage(2));
                    StartCoroutine(DelayedBarrage(3));
                    break;
                }
            case 2:
                { 
                    fire(70, proj1);
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
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(2));
                    StartCoroutine(DelayedLaunch(2));
                    StartCoroutine(DelayedLaunch(3));
                    StartCoroutine(DelayedLaunch(3));
                    StartCoroutine(DelayedLaunch(4));
                    StartCoroutine(DelayedLaunch(4));
                    StartCoroutine(DelayedLaunch(5));
                    StartCoroutine(DelayedLaunch(5));
                    Invoke(nameof(resetRotation), 3);
                    break;
                }
            case 4:
                {
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(1));
                    StartCoroutine(DelayedLaunch(1));
                    break;
                }
            case 5:
                {
                    fire(70, proj2);
                    fire(70, proj2);
                    fire(70, proj2);
                    fire(70, proj2);
                    break;
                }
            case 6:
                {
                    giveHealth(3);
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
        if (!isAlive)
            yield break;

        GameObject proj = Instantiate(spike, spikeOrigin.transform.position, Quaternion.identity);
        Launch1.Play();
        Rigidbody projRB = proj.GetComponent<Rigidbody>();
        Vector3 rbforce = spikeOrigin.transform.up * 100 + transform.up + transform.forward * -1/* * currentArmStrength*/;
        projRB.AddForce(rbforce, ForceMode.Impulse);
        int x = UnityEngine.Random.Range(-15, 15);
        int z = UnityEngine.Random.Range(-15, 15);
        Vector3 spawnLoc = new Vector3(x,350,z);
        GameObject proj2 = Instantiate(spike, spawnLoc, Quaternion.identity);
    }
}


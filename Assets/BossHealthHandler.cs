using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthHandler : MonoBehaviour
{
    public GameObject bossObj;
    public AudioSource Damage;
    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        boss = bossObj.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossDamager") && boss.canTakeDamage)
        {
            //Attack thisAttack = other.gameObject.GetComponent<Attack>();
            Damage.Play();
            boss.damage(1);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}

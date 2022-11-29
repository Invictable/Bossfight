using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthHandler : MonoBehaviour
{
    public static GameObject bossObj;
    public Boss boss = bossObj.GetComponent<Boss>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BossDamager"))
        {
            Attack thisAttack = other.gameObject.GetComponent<Attack>();
            boss.takeHealth(thisAttack.damage);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}

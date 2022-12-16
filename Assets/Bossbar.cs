using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bossbar : MonoBehaviour
{
    public GameObject BossObj;
    private Image healthbar;
    Boss health;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
    }

    void Awake()
    {
        health = BossObj.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = (float)(health.BossHealth / health.StartingHealth);
    }
}

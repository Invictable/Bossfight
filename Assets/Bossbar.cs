using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bossbar : MonoBehaviour
{

    private Image healthbar;
    Boss health;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
        GameObject player = GameObject.Find("Boss");
        health = player.GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = (float)(health.BossHealth / health.StartingHealth);
    }
}

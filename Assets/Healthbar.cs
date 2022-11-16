using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{

    private Image healthbar;
    PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
        GameObject player = GameObject.Find("Player");
        health = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = (float) (health.currentHealth / health.startingHealth);
    }
}

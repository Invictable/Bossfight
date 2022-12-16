using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacks : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode Ability1 = KeyCode.E;
    public KeyCode Ability2 = KeyCode.Q;

    [Header("Projectiles")]
    public GameObject bulletProjectile;
    public GameObject ballProjectile;
    public GameObject projectileOrigin;

    [Header("Points")]
    public Transform cam;
    public Transform attackPoint;
    public float cooldown;
    public float armStrength;
    public float currentArmStrength;
    public Text abilityPrompt;

    bool readyToThrow;
    bool ability1Ready = true;
    int ability1cooldown = 30;

    public AudioSource Shoot;
    public AudioSource Heal;
    public AudioSource Refresh;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
        currentArmStrength = armStrength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && readyToThrow)
        {
            fire(100f,bulletProjectile);
            Shoot.Play();
        }
        else if (Input.GetMouseButtonDown(1) && readyToThrow)
        {
            fire(50f, ballProjectile);
            Shoot.Play();
        }
        else if(Input.GetKey(Ability1) && ability1Ready)
        {
            ability1Ready = false;
            gameObject.GetComponent<PlayerHealth>().AddHealth(50, false);
            abilityPrompt.enabled = false;
            Invoke(nameof(resetAbility1Cooldown), ability1cooldown);
            Heal.Play();
        }
    }

    void fire(float force,GameObject projectile)
    {
        readyToThrow = false;
        GameObject proj = Instantiate(projectile, attackPoint.position, cam.rotation);
        Rigidbody projRB = proj.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(cam.position,cam.forward, out hit, 2500))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        Vector3 rbforce = forceDirection * force + transform.up * currentArmStrength;
        projRB.AddForce(rbforce, ForceMode.Impulse);
        
        Invoke(nameof(resetCooldown), cooldown);
    }

    void resetCooldown()
    {
        readyToThrow = true;
    }

    void resetAbility1Cooldown()
    {
        Refresh.Play();
        ability1Ready = true;
        abilityPrompt.enabled = true;
    }
}

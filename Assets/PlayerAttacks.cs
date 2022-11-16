using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool readyToThrow;
    bool ability1Ready = true;
    int ability1cooldown = 30;

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
        }
        else if (Input.GetMouseButtonDown(1) && readyToThrow)
        {
            fire(50f, ballProjectile);
        }
        else if(Input.GetKey(Ability1) && ability1Ready)
        {
            ability1Ready = false;
            gameObject.GetComponent<PlayerHealth>().AddHealth(50, false);
            Invoke(nameof(resetAbility1Cooldown), ability1cooldown);
        }
    }

    void fire(float force,GameObject projectile)
    {
        readyToThrow = false;
        GameObject proj = Instantiate(projectile, attackPoint.position, cam.rotation);
        Rigidbody projRB = proj.GetComponent<Rigidbody>();

        Vector3 rbforce = cam.transform.forward * force + transform.up * currentArmStrength;
        projRB.AddForce(rbforce, ForceMode.Impulse);
        
        Invoke(nameof(resetCooldown), cooldown);

        //proj.GetComponent<Bullet>().Launch(shootDir);
    }

    void resetCooldown()
    {
        readyToThrow = true;
    }

    void resetAbility1Cooldown()
    {
        ability1Ready = true;
    }
}

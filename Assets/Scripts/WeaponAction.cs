using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAction : MonoBehaviour
{
    [Header("Configuration")]
    public bool meleeMode;
    public bool projectileMode;
    public new Camera camera;
    DamageAction damage;
    Animator animator;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;
    public int projectileLifeTime = 3;
    public Transform projectileOrigin;
    GameObject projectile;
    Rigidbody projectileRB;

    [Header("Ammo")]
    public bool useAmmo = true;
    public int maxAmmo = 8;
    public int currentAmmo = 0;
    public Text ammoCountText;

    void Start()
    {
        this.currentAmmo = maxAmmo;
        this.animator = GetComponent<Animator>();
        this.damage = GetComponent<DamageAction>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (this.projectileMode)
            {
                ProjectileAttack();
            }
            if (this.meleeMode)
                MeleeAtack();
        }

        if (this.useAmmo)
            Reload();
    }
    void MeleeAtack()
    {
        this.animator.SetTrigger("Attack");
    }

    void ProjectileAttack()
    {
        if (!this.useAmmo || this.useAmmo && this.currentAmmo > 0)
        {
            this.animator.SetTrigger("Attack");

            projectile = Instantiate(projectilePrefab, projectileOrigin.position, camera.transform.rotation);
            projectileRB = projectile.GetComponent<Rigidbody>();
            projectileRB.AddForce(camera.transform.rotation * Vector3.forward * projectileSpeed, ForceMode.Impulse);
            Destroy(projectile, projectileLifeTime);

            currentAmmo -= 1;
            ammoCountText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }

    void Reload()
    {
        if (Input.GetButtonDown("Reload") && this.currentAmmo < this.maxAmmo)
        {
            animator.SetTrigger("Reload");
            this.currentAmmo = maxAmmo;
            ammoCountText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }

}

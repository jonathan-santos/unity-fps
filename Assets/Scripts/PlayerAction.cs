using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 4f;
    public float jumpStrengh = 30f;
    Rigidbody rb;

    [Header("Camera")]
    public float sensitivity = 2.5f;
    public float smoothing = 1f;
    Vector2 mouseLook;
    Vector2 smoothV;
    new Camera camera;

    [Header("Ground detection")]
    public LayerMask groundLayer;
    public GameObject groundDetect;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletStrength = 50f;
    public int bulletLifeTime = 3;
    public Transform bulletOrigin;
    public Animator GunAnimator;
    GameObject bullet;
    Rigidbody bulletRB;

    [Header("Ammo")]
    public int maxAmmo = 8;
    public int currentAmmo = 8;
    public Text ammoCountText;

    [Header("Sword")]
    public Animator SwordAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        RemoveMouse();
    }

    void Update()
    {
        Movement();
        Aim();
        Jump();
        AttackSword();
        //Shoot();
        //Reload();
    }

    private void RemoveMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Movement()
    {
        var speed = Input.GetKey("left shift") ? movementSpeed * 2f : movementSpeed;
        var forwardTranslation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        var sideTranslation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        this.transform.Translate(sideTranslation, 0, forwardTranslation);
    }

    void Aim()
    {
        var aimDelta = new Vector2(Input.GetAxisRaw("Aim X"), Input.GetAxisRaw("Aim Y"));
        aimDelta = Vector2.Scale(aimDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, aimDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, aimDelta.y, 1f / smoothing);
        mouseLook += smoothV;

        camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        this.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, this.transform.up);
    }

    void Jump()
    {
        var isGrounded = Physics.CheckSphere(groundDetect.transform.position, 0.5f, groundLayer);
        if (isGrounded && Input.GetAxis("Jump") > 0)
            rb.AddForce(Vector3.up * jumpStrengh);
    }

    void AttackSword()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SwordAnimator.SetTrigger("Attack");
        }
    }

    void Shoot()
    {
        if(Input.GetButtonDown("Fire1") && this.currentAmmo > 0)
        {
            GunAnimator.SetTrigger("Shoot");

            bullet = Instantiate(bulletPrefab, bulletOrigin.position, camera.transform.rotation);
            bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(camera.transform.rotation * Vector3.forward * bulletStrength, ForceMode.Impulse);
            Physics.IgnoreCollision(this.GetComponent<Collider>(), bullet.GetComponent<Collider>());
            Destroy(bullet, bulletLifeTime);
            currentAmmo -= 1;
            ammoCountText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }

    void Reload()
    {
        if(Input.GetButtonDown("Reload") && this.currentAmmo < this.maxAmmo)
        {
            GunAnimator.SetTrigger("Reload");
            this.currentAmmo = maxAmmo;
            ammoCountText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}

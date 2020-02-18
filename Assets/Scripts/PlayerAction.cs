using System;
using UnityEngine;
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

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletStrength = 50f;
    public Transform bulletOrigin;
    public Animator GunAnimator;
    Rigidbody bulletRB;

    private void Start()
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
        Shoot();
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
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        mouseLook += smoothV;

        camera.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        this.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, this.transform.up);
    }

    void Jump()
    {
        var isGrounded = Physics.CheckSphere(groundDetect.transform.position, 0.5f, groundLayer);
        if (isGrounded && Input.GetAxis("Jump") > 0)
            rb.AddForce(Vector3.up * jumpStrengh, ForceMode.Force);
    }

    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GunAnimator.SetTrigger("Shoot");
            var bullet = Instantiate(bulletPrefab, bulletOrigin.position, camera.transform.rotation);
            bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(camera.transform.rotation * Vector3.forward * bulletStrength, ForceMode.Impulse);
            Physics.IgnoreCollision(this.GetComponent<Collider>(), bullet.GetComponent<Collider>());
        }
    }
}

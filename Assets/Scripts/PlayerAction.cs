using System;
using UnityEngine;
public class PlayerAction : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 3f;
    public float jumpStrengh = 0.35f;

    [Header("Camera")]
    public float sensitivity = 2.5f;
    public float smoothing = 1f;
    private Vector2 mouseLook;
    private Vector2 smoothV;

    [Header("Ground detection")]
    public LayerMask groundLayer;
    public GameObject groundDetect;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float bulletStrength = 5f;
    Rigidbody bulletRB;


    Rigidbody rb;
    new Camera camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        ChangeCursor();
    }

    void Update()
    {
        Movement();
        Aim();
        Jump();
        Shoot();
    }

    private void ChangeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Movement()
    {
        var translation = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        var straffe = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        this.transform.Translate(straffe, 0, translation);
    }

    private void Aim()
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
            rb.AddForce(Vector3.up * jumpStrengh, ForceMode.Impulse);
    }

    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = camera.transform.rotation;
            bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(camera.transform.rotation * Vector3.forward * bulletStrength, ForceMode.Impulse);
            Physics.IgnoreCollision(this.GetComponent<Collider>(), bullet.GetComponent<Collider>());
        }
    }
}

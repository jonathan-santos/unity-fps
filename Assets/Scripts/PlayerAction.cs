using System;
using UnityEngine;
public class PlayerAction : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float jumpStrengh = 0.5f;

    [Header("Camera")]
    public float sensitivity = 3f;
    public float smoothing = 1f;
    private Vector2 mouseLook;
    private Vector2 smoothV;

    [Header("Ground detection")]
    public LayerMask groundLayer;
    public GameObject groundDetect;

    Rigidbody rb;
    new Camera camera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Movement();
        Aim();
        Jump();
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
}

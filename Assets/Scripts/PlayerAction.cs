using TMPro;
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

    [Header("Weapon Selector")]
    public GameObject[] weapons;
    WeaponAction currentWeapon;

    [Header("UI")]
    public TextMeshProUGUI ammoCountText;
    public Image ammoCountImage;
    public Image aimCursor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = GetComponentInChildren<Camera>();
        RemoveMouse();
        SelectWeapon(0);
    }

    void Update()
    {
        Movement();
        Aim();
        Jump();
        SelectWeapons();
    }

    void RemoveMouse()
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
    void SelectWeapons()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            SelectWeapon(0);
        if (Input.GetKey(KeyCode.Alpha2))
            SelectWeapon(1);
    }

    void SelectWeapon(int selectedWeaponIndex)
    {
        for(int i = 0; i < this.weapons.Length; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weapons[i].SetActive(true);

                this.currentWeapon = weapons[i].GetComponent<WeaponAction>();
                this.currentWeapon.OnChangeAmmo.RemoveAllListeners();
                this.currentWeapon.OnChangeAmmo.AddListener(this.OnWeaponReload);
                this.currentWeapon.OnChangeAmmo.Invoke();

                aimCursor.enabled = this.currentWeapon.aimCursorSprite != null;
                ammoCountText.enabled = this.currentWeapon.useAmmo;
                ammoCountImage.enabled = this.currentWeapon.ammoCountSprite!= null;

                aimCursor.sprite = this.currentWeapon.aimCursorSprite;
                ammoCountImage.sprite = this.currentWeapon.ammoCountSprite;
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    void OnWeaponReload()
    {
        ammoCountText.text = $"{this.currentWeapon.currentAmmo}/{this.currentWeapon.maxAmmo}";
    }
}

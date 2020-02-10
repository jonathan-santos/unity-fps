using System;
using UnityEngine;
public class PlayerAction : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 50f;

    Camera camera;

    private void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        KeyboardMovement();

    }
    void KeyboardMovement()
    {
        var translation = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        var straffe = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);
    }
}

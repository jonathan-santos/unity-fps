using UnityEngine;

public class CanvasBillboardAction : MonoBehaviour
{
    Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + playerCamera.forward);
    }
}
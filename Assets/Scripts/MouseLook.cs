using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensetivity = 200f;
    public Transform player;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

        yRotation = Mathf.Clamp(yRotation - y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        player.Rotate(Vector3.up, x);
    }
}

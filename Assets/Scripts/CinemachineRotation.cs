using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinemachineRotation : MonoBehaviour
{
    CinemachineCamera cinemachineCamera;
    public float sensitivity = 500f;
    public float scrollSpeed = 10f;
    float yRotation = 0f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        cinemachineCamera.Lens.OrthographicSize -= scroll * scrollSpeed * Time.deltaTime;
        cinemachineCamera.Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.Lens.OrthographicSize, 2f, 5.5f);
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(30f, yRotation, 0f);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }



    }
}

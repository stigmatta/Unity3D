using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private float sensitivityX = 4f;
    [SerializeField] private float sensitivityY = 1f;
    private float minVerticalAngle = 50f;
    private float maxVerticalAngle = 70f;


    private Vector3 offset;
    private float angleY;
    private float angleX;
    private InputAction lookAction;

    private void Start()
    {
        offset = transform.position - cameraAnchor.position;

        angleY = transform.eulerAngles.y;
        angleX = transform.eulerAngles.x;

        lookAction = InputSystem.actions.FindAction("Look");
        if (lookAction != null)
            lookAction.Enable();
    }

    private void Update()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();

        angleY += lookValue.x * sensitivityX * Time.deltaTime;
        angleX -= lookValue.y * sensitivityY * Time.deltaTime;

        angleX = Mathf.Clamp(angleX, minVerticalAngle, maxVerticalAngle);


        transform.rotation = Quaternion.Euler(angleX, angleY, 0f);
        transform.position = cameraAnchor.position + Quaternion.Euler(0f, angleY, 0f) * offset;
    }
}

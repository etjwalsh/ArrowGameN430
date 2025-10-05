using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] float swayTransform = 1f;
    // [SerializeField] float swayRotation = 200f;
    [SerializeField] float smoothSpeed = 5f;

    Vector3 initPos;

    void Start()
    {
        initPos = transform.localPosition;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x - Screen.width / 2f) / Screen.width;
        float mouseY = (Input.mousePosition.y - Screen.width / 2f) / Screen.width;

        Vector3 targetPositionTransform = new Vector3(mouseX * swayTransform, mouseY * swayTransform, 0);
        // Vector3 targetPositionRotation = new Vector3(0, mouseY * swayRotation, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, initPos + targetPositionTransform, Time.deltaTime * smoothSpeed);
        // transform.localEulerAngles = Vector3.Lerp(transform.localPosition, initPos + targetPositionRotation, Time.deltaTime * smoothSpeed);
    }
}
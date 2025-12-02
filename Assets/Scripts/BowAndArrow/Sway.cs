using UnityEngine;

public class Sway : MonoBehaviour
{
    [SerializeField] float swayTransform = 1f;
    [SerializeField] float swayRotation = 1f;
    [SerializeField] float smoothSpeed = 5f;

    Vector3 initPos;
    private Quaternion initRot;

    void Start()
    {
        initPos = transform.localPosition;
        initRot = transform.localRotation;
    }

    void Update()
    {
        float mouseX = (Input.mousePosition.x - Screen.width / 2f) / Screen.width;
        float mouseY = (Input.mousePosition.y - Screen.width / 2f) / Screen.width;

        //move back and forth (transform movement)
        Vector3 targetPositionTransform = new Vector3(mouseX * swayTransform, mouseY * swayTransform, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initPos + targetPositionTransform, Time.deltaTime * smoothSpeed);

        //rotate back and forth (rotation movement)
        Vector3 targetEuler = new Vector3(-mouseY * swayRotation, mouseX * swayRotation, 0);
        Quaternion targetRotation = Quaternion.Euler(targetEuler);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, initRot * targetRotation, Time.deltaTime * smoothSpeed);
    }
}
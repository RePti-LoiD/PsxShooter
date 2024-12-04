using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int ShiftVelocityDivider = 500;

    private Vector3 defaultPosition;

    private void Awake()
    {
        defaultPosition = transform.localPosition;
    }

    public void CurrentDirectionChangedHandler(Vector3 currentDirection) =>
        transform.localPosition = Vector3.Lerp(
                transform.localPosition + (new Vector3(currentDirection.x, 0, currentDirection.z) / ShiftVelocityDivider), 
                defaultPosition, 
                Time.deltaTime * speed
            );
}
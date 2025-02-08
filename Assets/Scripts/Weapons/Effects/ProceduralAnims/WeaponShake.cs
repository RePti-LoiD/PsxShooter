using UnityEngine;

public class WeaponShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeFreq;
    [SerializeField] private float inputWeight;
    [SerializeField] private float mouseInputWeight;
    [SerializeField] private float lerpValue;

    private float time;
    private float Cos { get => Mathf.Abs(Mathf.Cos(shakeFreq * time)) * shakeAmount; }
    private float Sin { get => Mathf.Sin(shakeFreq * time) * shakeAmount; }

    private Vector2 mouseInput;
    private bool isGrounded;

    public void OnIsGroundedChanged(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }


    public void OnMouse(Vector2 input)
    {
        mouseInput = input;
    }

    public void OnMove(Vector2 input)
    {
        input = -input;
        if (isGrounded && input != Vector2.zero)
            time += Time.deltaTime;

        MoveWeapon(input);
    }

    private void MoveWeapon(Vector2 input)
    {
        transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
                    new Vector3(
                        Sin + input.x * inputWeight + mouseInput.x * mouseInputWeight,
                        Cos + mouseInput.y * mouseInputWeight,
                        input.y * inputWeight
                    ),
                    Time.deltaTime * lerpValue
                );
    }

}
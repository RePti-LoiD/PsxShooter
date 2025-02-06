using UnityEngine;

public class WeaponShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeFreq;
    [SerializeField] private float inputWeight;
    [SerializeField] private float lerpValue;

    private float time;
    private float Cos { get => Mathf.Abs(Mathf.Cos(shakeFreq * time)) * shakeAmount; }
    private float Sin { get => Mathf.Sin(shakeFreq * time) * shakeAmount; }

    
    public void OnMove(Vector2 input)
    {
        input = -input;
        if (input != Vector2.zero)
            time += Time.deltaTime;

        transform.localPosition = Vector3.Lerp (
            transform.localPosition, 
            new Vector3 (
                Sin + input.x * inputWeight, 
                Cos, 
                input.y * inputWeight
            ), 
            Time.deltaTime * lerpValue
        );
    }
}
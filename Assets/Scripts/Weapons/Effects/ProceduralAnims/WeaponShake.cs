using UnityEngine;

public class WeaponShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount;
    [SerializeField] private float shakeFreq;

    private float Cos { get => Mathf.Cos(shakeFreq * Time.time) * shakeAmount; }
    private float Sin { get => Mathf.Sin(shakeFreq * Time.time) * shakeAmount; }

    
    public void OnMove(Vector2 input)
    {
        transform.localPosition = input;
    }
}
using System.Collections;
using UnityEngine;

public class LinearInterpolationAnim : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float animTime;

    public void AnimateGunEnabling()
    {
        StartCoroutine(MoveObject(startPosition, targetPosition, animTime));
    }

    private IEnumerator MoveObject(Vector3 startPosition, Vector3 targetPosition, float time)
    {
        float currentTime = 0;

        while (currentTime / time < 1)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, currentTime / time);

            yield return null;
        }
    }
}
using System.Collections;
using System.Linq;
using UnityEngine;

public class ShotTrailLine : MonoBehaviour
{
    [SerializeField] private Transform trailOrigin;
    [SerializeField] private GameObject trailRendererPrefab;
    [SerializeField] private float lineLifetime;

    public void DrawTrail(Vector3[] trailTarget)
    {
        var trailObj = Instantiate(trailRendererPrefab, Vector3.zero, Quaternion.identity, null);
        var line = trailObj.GetComponent<LineRenderer>();
        line.positionCount = trailTarget.Length + 1;
         
        line.SetPositions(new Vector3[] { trailOrigin.position }.Concat(trailTarget).ToArray());

        StartCoroutine(DestroyLineAfter(trailObj, lineLifetime));
    }

    private IEnumerator DestroyLineAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(obj);
    }
}
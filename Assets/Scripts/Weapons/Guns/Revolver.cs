using UnityEngine;

public class Revolver : WeaponInputLayer
{
    [SerializeField] private float damage;
    [SerializeField] private Vector3Event OnShotTargetTransform;


    private Transform raycastOrigin;

    private void Start()
    {
        raycastOrigin = Camera.main.gameObject.transform;
    }

    public override void OnShot()
    {        
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out RaycastHit hitInfo))
        {

            OnShotTargetTransform?.Invoke(hitInfo.point);

        }
        else
        {
            OnShotTargetTransform?.Invoke(raycastOrigin.forward * 100);
        }
    }
}
using UnityEngine;

public class WeaponInputLayer : MonoBehaviour
{
    [SerializeField] private ShotType shotType;

    public virtual void OnShotStart()
    {
        shotType.OnShotStart();
    }

    public virtual void OnShotStop()
    {
        shotType.OnShotStop();
    }


    public virtual void OnShot()
    {

    }

    public virtual void OnReload()
    {

    }

    public virtual void OnAdditionalAction()
    {

    }

    public virtual void OnInspect()
    {

    }
}
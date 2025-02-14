using UnityEngine;

public class WeaponInputLayer : MonoBehaviour
{
    [SerializeField] private ShotType shotType;

    public void OnShotStart()
    {
        shotType.OnShotStart();
    }

    public void OnShotStop()
    {
        shotType.OnShotStop();
    }


    public virtual void OnShot()
    {
        print("Shot");
    }

    public virtual void OnReload()
    {
        print("reload");
    }

    public virtual void OnAdditionalAction()
    {
        print("Additional action");
    }

    public virtual void OnInspect()
    {
        print("inspect");
    }
}
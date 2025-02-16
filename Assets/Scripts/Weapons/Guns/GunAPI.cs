using UnityEngine;

public abstract class GunAPI : MonoBehaviour
{
    [SerializeField] public GunIkAPI ArmsIkAPI;

    public abstract void ShotStart();
    public abstract void ShotStop();
    public abstract void Reload();
    public abstract void AdditionalAction();
    public abstract void Inspect();

    public abstract void EnableGun(ExternalDataForGun data);

    public abstract void DisableGun();
}
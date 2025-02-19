using System;
using UnityEngine;

public abstract class GunAPI : MonoBehaviour
{
    [SerializeField] public GunIkAPI ArmsIkAPI;

    public Action<GunAPI> Disabled;

    public abstract void ShotStart();
    public abstract void ShotStop();
    public abstract void Reload();
    public abstract void AdditionalAction();
    public abstract void Inspect();

    public abstract void EnableGun(ExternalDataForGun data);

    public virtual void DisableGun() =>
        Disabled?.Invoke(this);
}
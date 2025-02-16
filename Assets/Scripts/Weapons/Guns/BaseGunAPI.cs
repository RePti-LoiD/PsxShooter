using UnityEngine;
using UnityEngine.Events;

public class BaseGunAPI : GunAPI
{
    [SerializeField] private GameObject children;

    [Header("Events")]
    [SerializeField] private UnityEvent shotStart;
    [SerializeField] private UnityEvent shotStop;
    [SerializeField] private UnityEvent additionalAction;
    [SerializeField] private UnityEvent inspect;
    [SerializeField] private UnityEvent reload;
    
    [Space]
    [SerializeField] private UnityEvent gunEnabled;
    [SerializeField] private UnityEvent gunDisabled;

    protected ExternalDataForGun LastData;

    public override void DisableGun()
    {
        gunDisabled?.Invoke();

        children.SetActive(false);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        LastData = data;
        gunEnabled?.Invoke();

        children.SetActive(true);
    }

    public override void ShotStart() =>
        shotStart?.Invoke();

    public override void ShotStop() =>
        shotStop?.Invoke();

    public override void AdditionalAction() =>
        additionalAction?.Invoke();

    public override void Inspect() =>
        inspect?.Invoke();

    public override void Reload() =>
        reload?.Invoke();
}

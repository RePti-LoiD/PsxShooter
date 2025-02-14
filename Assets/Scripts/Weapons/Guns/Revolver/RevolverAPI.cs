using UnityEngine;
using UnityEngine.Events;

public class RevolverAPI : GunAPI
{
    [SerializeField] private GameObject children;

    [Header("Events")]
    [SerializeField] private UnityEvent shotStart;
    [SerializeField] private UnityEvent shotStop;
    [SerializeField] private UnityEvent additionalAction;

    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private RecoilRotationSender cameraRecoil;

    private ExternalDataForGun lastData;

    public override void DisableGun()
    {
        children.SetActive(false);

        soundPlayer.Source = null;
        cameraRecoil.OnRecoil.RemoveListener(lastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        children.SetActive(true);

        lastData = data;

        soundPlayer.Source = data.AudioSource;
        cameraRecoil.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void ShotStart() =>
        shotStart?.Invoke();

    public override void ShotStop() =>
        shotStop?.Invoke();

    public override void AdditionalAction() =>
        additionalAction?.Invoke();

    public override void Reload() { }
    public override void Inspect() { }
}
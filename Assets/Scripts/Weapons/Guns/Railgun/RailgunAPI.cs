using UnityEngine;

public class RailgunAPI : BaseGunAPI
{
    [SerializeField] private RecoilRotationSender RecoilRotationSender;

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        RecoilRotationSender.OnRecoil.AddListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void DisableGun()
    {
        base.DisableGun();

        RecoilRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }
}
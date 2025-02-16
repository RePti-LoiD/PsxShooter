using UnityEngine;

public class FistsAPI : BaseGunAPI
{
    [SerializeField] public RecoilRotationSender leftHitRotationSender;
    [SerializeField] public RecoilRotationSender rightHitRotationSender;

    public override void DisableGun()
    {
        base.DisableGun();

        leftHitRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
        rightHitRotationSender.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        leftHitRotationSender.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
        rightHitRotationSender.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
    }
}
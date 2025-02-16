using UnityEngine;

public class RevolverAPI : BaseGunAPI
{
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private RecoilRotationSender cameraRecoil;

    public override void DisableGun()
    {
        base.DisableGun();

        soundPlayer.Source = null;
        cameraRecoil.OnRecoil.RemoveListener(LastData.CameraRecoilRotationReceiver.RotateObject);
    }

    public override void EnableGun(ExternalDataForGun data)
    {
        base.EnableGun(data);

        soundPlayer.Source = data.AudioSource;
        cameraRecoil.OnRecoil.AddListener(data.CameraRecoilRotationReceiver.RotateObject);
    }
}
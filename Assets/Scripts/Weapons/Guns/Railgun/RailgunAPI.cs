using UnityEngine;

public class RailgunAPI : BaseGunAPI
{
    [SerializeField] private RecoilRotationSender RecoilRotationSender;

    public void SetJumpVelocity(float jumpVelocity)
    {
        LastData.Movement.SetJumpVelocity(jumpVelocity);
    }

    public void AddMovementAdditionalVelocity(Vector3 additionalVelocity)
    {
        LastData.Movement.AddLinearVelocity(additionalVelocity);
    }

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
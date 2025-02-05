using UnityEngine;

public class SingleShotType : ShotType
{
    [SerializeField] private float fireRatePerMinute;

    private float durationBetweenShot;
    private float lastShotTime = 0;

    private void Start()
    {
        durationBetweenShot = fireRatePerMinute / 60;
    }

    public override void OnShotStart()
    {
        if (Time.time - lastShotTime < durationBetweenShot) return;
        lastShotTime = Time.time;

        OnShot?.Invoke();
    }
}
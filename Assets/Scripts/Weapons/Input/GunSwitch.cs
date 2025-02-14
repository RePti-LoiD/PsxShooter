using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [Header("Guns/events")]
    [SerializeField] private List<GunAPI> guns = new List<GunAPI>();
    [SerializeField] private GunAPIEvent GunSelected;

    [Header("Data for gun")]
    [SerializeField] private MonoBehaviour coroutineRunner;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private RecoilRotationReceiver cameraRecoilRotationReceiver;

    private GunAPI currentGun;

    public int CurrentIndex 
    { 
        private set
        {
            currentIndex = value;
        }
        get => Mathf.Abs(currentIndex) % 4;
    }
    private int currentIndex = 0;

    private void Start()
    {
        SelectGun(guns.First());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectGun(guns.First());
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectGun(guns.Last());
    }

    private void SelectGun(GunAPI gun)
    {
        currentGun?.DisableGun();

        gun.EnableGun(new ExternalDataForGun
        {
            CoroutineRunner = coroutineRunner,
            AudioSource = audioSource,
            GunTransform = gunTransform,
            CameraRecoilRotationReceiver = cameraRecoilRotationReceiver
        });
        
        currentGun = gun;

        GunSelected?.Invoke(gun);
    }
}
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource source;

    public void PlaySound()
    {
        PlaySound(Random.Range(0, clips.Length));
    }

    public void PlaySound(int index)
    {
        source.PlayOneShot(clips[index]);
    }
}
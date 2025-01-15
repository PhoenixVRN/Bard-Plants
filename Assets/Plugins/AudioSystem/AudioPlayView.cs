using UnityEngine;

public class AudioPlayView : MonoBehaviour
{
    private AudioSource _source;

    private void Awake() => _source = GetComponent<AudioSource>();

    public void PlaySound() => _source.Play();
}

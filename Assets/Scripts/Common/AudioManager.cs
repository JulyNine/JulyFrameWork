using UnityEngine;
/// <summary>
/// 音频管理类
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource audioSource;
    // Note: this is set on Awake()

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        // MessageDispatcher.AddListener(GameEvent.CHANGE_LANGUAGE, ChangeLanguage);
    }

    private void OnDisable()
    {
        // MessageDispatcher.RemoveListener(GameEvent.CHANGE_LANGUAGE, ChangeLanguage);
    }
    // Use this for initialization

    //public void PlayAudio()
    //{
    //    audioSource.Play();
    //}
    public void PlayAudio(string audioName)
    {
        var audioPath = "Audios/" + audioName;
        var clip = Resources.Load(audioPath) as AudioClip;
        if (clip != null)
            audioSource.PlayOneShot(clip);
        else
            Debug.Log("No Clip : " + audioName);
    }
}
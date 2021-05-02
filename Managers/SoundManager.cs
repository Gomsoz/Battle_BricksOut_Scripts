using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Defines.SoundType.SoundCnt];
    Dictionary<AudioSource, AudioClip> _audioClips = new Dictionary<AudioSource, AudioClip>();

    AudioSource[] _itemAudioSources = new AudioSource[(int)Defines.SpeciesofItem.ItemCount];
    Dictionary<AudioSource, AudioClip> _itemAudioClips = new Dictionary<AudioSource, AudioClip>();

    GameObject rootSoundGo;

    public void Init()
    {
        rootSoundGo = GameObject.Find("@Sounds");
        if (rootSoundGo == null)
        {
            rootSoundGo = new GameObject { name = "@Sounds" };
            GameObject.DontDestroyOnLoad(rootSoundGo);
        }

        AddAudioSources<Defines.SoundType>(_audioSources, _audioClips, (int)Defines.SoundType.SoundCnt);
        AddAudioSources<Defines.SpeciesofItem>(_itemAudioSources, _itemAudioClips, (int)Defines.SpeciesofItem.ItemCount);
    }

    void AddAudioSources<T>
        (AudioSource[] arrayAudioSources, Dictionary<AudioSource, AudioClip> dicAudioClips, int cnt) where T : System.Enum
    {
        for (int i = 0; i < cnt; i++)
        {
            string[] typeNames = System.Enum.GetNames(typeof(T));

            GameObject soundGo = new GameObject { name = typeNames[i] };
            soundGo.transform.parent = rootSoundGo.transform;
            soundGo.AddComponent<AudioSource>();
            arrayAudioSources[i] = soundGo.GetComponent<AudioSource>();
            dicAudioClips.Add(arrayAudioSources[i], null);
        }
    }

    public void Play(string path, float pitch, Defines.SpeciesofItem itemType)
    {
        if (_itemAudioClips[_itemAudioSources[(int)itemType]] == null)
        {
            if (!path.Contains("Sounds/"))
                path = $"Sounds/ItemSounds/ItemSound_{path}";

            AudioClip _audioClip = Managers.Resources.Load<AudioClip>(path);

            if (_audioClip == null)
            {
                Debug.Log($"Not Found Sound Clip : {path}");
                return;
            }
            _itemAudioClips[_itemAudioSources[(int)itemType]] = _audioClip;
        }

        AudioSource audioSource = _itemAudioSources[(int)itemType];
        audioSource.clip = _itemAudioClips[_itemAudioSources[(int)itemType]];
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void Play(string path, float pitch, Defines.SoundType soundType)
    {

        if(_audioClips[_audioSources[(int)soundType]] == null)
        {
            if (!path.Contains("Sounds/"))
                path = $"Sounds/{path}";

            AudioClip _audioClip = Managers.Resources.Load<AudioClip>(path);

            if (_audioClip == null)
            {
                Debug.Log($"Not Found Sound Clip : {path}");
                return;
            }

            _audioClips[_audioSources[(int)soundType]] = _audioClip;
        }      

        if (soundType == Defines.SoundType.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Defines.SoundType.Bgm];

            audioSource.pitch = pitch;
            audioSource.loop = true;

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = _audioClips[_audioSources[(int)soundType]];
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)soundType];
            audioSource.clip = _audioClips[_audioSources[(int)soundType]];
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void Pause(Defines.SoundType Soundtype)
    {
        AudioSource audioSource = _audioSources[(int)Soundtype];

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            return;
        }
    }

    public void UnPause(Defines.SoundType Soundtype)
    {
        AudioSource audioSource = _audioSources[(int)Soundtype];

        if (audioSource.isPlaying == false)
        {
            audioSource.UnPause();
            return;
        }
    }
}

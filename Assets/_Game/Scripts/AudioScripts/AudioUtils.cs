
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;


namespace Game
{
    
    public static class AudioUtils
    {
        public static async UniTask Mute(this AudioSource audioSource, float duration)
        {
            await audioSource.DoUniFade(0f, duration);
            audioSource.mute = true;
        }

        public static async UniTask Activate(this AudioSource audioSource, float duration)
        {
            audioSource.mute = false;
            await audioSource.DoUniFade(1f, duration);
        }

        public static void PlaySound(this AudioSource audioSource, Sound sound)
        {
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.loop;
            audioSource.clip = sound.clip;

            audioSource.Play();
        }
    }

}
using UnityEngine;
using System.Collections;

public class GeunSuAudio : MonoBehaviour
{
    // 효과음 등의 사운드를 1회만 재생하고 제거하는 함수
    public void PlaySoundOnce(AudioClip audioClip)
    {
        StartCoroutine(PlaySound(audioClip));
    }
    // 효과음 재생 코루틴
    IEnumerator PlaySound(AudioClip audioClip)
    {
        // 사운드 1회 재생
        GetComponent<AudioSource>().PlayOneShot(audioClip);
        yield return new WaitForSeconds(audioClip.length);
        // 사운드의 길이만큼 1회 재생한 후 제거
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    // 사운드 클립 단순 반복 재생
    public void PlaySoundLoop(AudioClip audioClip)
    {
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    // 오디오 소스를 멈추고 오디오 스크립트가 붙어있는 오브젝트를 제거
    public void StopAudioSource()
    {
        GetComponent<AudioSource>().Stop();
        Destroy(gameObject);
        //gameObject.SetActive(false);
        Debug.Log("되긴되냐");
    }
}
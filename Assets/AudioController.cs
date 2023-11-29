using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip Far;
    public AudioClip Close;
    public AudioClip Closer;
    public AudioClip Chase;
    private AudioClip Current;
    public GameObject player;
    public GameObject Yamamba;
    int ClipIndex = 4;
    // Start is called before the first frame update
    void Start()
    {
        Audio = this.GetComponent<AudioSource>();
        Audio.clip = Far;
        Audio.Play();
        StartCoroutine(ClipPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Yamamba.transform.position, player.transform.position) < 2f)
        {
            ClipIndex = 3;
        }
        else if (Vector3.Distance(Yamamba.transform.position, player.transform.position) < 4f)
        {
            ClipIndex = 2;
        }
        else if (Vector3.Distance(Yamamba.transform.position, player.transform.position) < 6f)
        {
            ClipIndex = 1;
        }
        else
        {
            ClipIndex = 4;
        }
    }

    IEnumerator ClipPlayer()
    {
        if (ClipIndex == 1)
        {
            Audio.clip = Close;
            Audio.PlayOneShot(Audio.clip);
            yield return new WaitForSeconds(Audio.clip.length);
            StartCoroutine(ClipPlayer());
            yield break;
        }
        else if (ClipIndex == 2)
        {
            Audio.clip = Closer;
            Audio.PlayOneShot(Audio.clip);
            yield return new WaitForSeconds(Audio.clip.length);
            StartCoroutine(ClipPlayer());
            yield break;
        }
        else if (ClipIndex == 3)
        {
            Audio.clip = Chase;
            Audio.PlayOneShot(Audio.clip);
            yield return new WaitForSeconds(Audio.clip.length);
            StartCoroutine(ClipPlayer());
            yield break;
        }
        else
        {
            Audio.clip = Far;
            Audio.PlayOneShot(Audio.clip);
            yield return new WaitForSeconds(Audio.clip.length);
            StartCoroutine(ClipPlayer());
            yield break;
        }
    }
}

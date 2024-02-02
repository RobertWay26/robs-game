using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioClip[] menuSounds;
    float rand;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying) {

            rand = Random.Range(0,3);
            source.clip = menuSounds[(int)rand];
            source.Play();
        }
    }
}

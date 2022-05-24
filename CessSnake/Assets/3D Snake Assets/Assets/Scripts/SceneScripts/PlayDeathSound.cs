using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathSound : MonoBehaviour
{

    public AudioClip dead_Sound;
    // Start is called before the first frame update
    void Start()
    {
         AudioSource.PlayClipAtPoint(dead_Sound, transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

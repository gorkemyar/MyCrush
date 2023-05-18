using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] destroySound;

    public void PlayDestroySound(){
        int index = Random.Range(0, destroySound.Length);
        destroySound[index].Play();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
       int numMusicPlayers = FindObjectsOfType(GetType()).Length;

       if (numMusicPlayers > 1)
       {
           gameObject.SetActive(false);
           Destroy(gameObject);
       }
       else
       {
           DontDestroyOnLoad(gameObject);
       }

    }
}

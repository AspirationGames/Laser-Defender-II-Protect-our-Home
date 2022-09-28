using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] ParticleSystem explosionVGX;
    PlayerControls playerControls;

   

    private void Awake() 
    {
        playerControls = FindObjectOfType<PlayerControls>();    

    }
    private void OnTriggerEnter(Collider other) 
    {
        playerControls.enabled = false;
        explosionVGX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);

        
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}

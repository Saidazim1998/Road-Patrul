using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayRatsiya : MonoBehaviour
{
    public GameObject player;
    public Transform chorraxa;
    public Transform yolCheti;
    public AudioClip ambulance;
    public AudioClip fire;
    public AudioClip specialCar;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetPlayerCenter()
    {
        player.transform.position = chorraxa.position;
    }
    public void SetPlayerChet()
    {
        player.transform.position = yolCheti.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
    public void PlayAmbulance()
	{
        StartCoroutine(Pambulance());
	}
    IEnumerator Pambulance()
	{
        yield return new WaitForSeconds(10f);
        source.clip = ambulance;
        source.Play();
	}
    public void PlayFire()
    {
        StartCoroutine(Pfire());
    }
    IEnumerator Pfire()
    {
        yield return new WaitForSeconds(10f);
        source.clip = fire;
        source.Play();

    }
    public void PlaySpecial()
    {
        StartCoroutine(PSpeial());
    }
    IEnumerator PSpeial()
    {
        yield return new WaitForSeconds(10f);
        source.clip = specialCar;
        source.Play();

    }
}

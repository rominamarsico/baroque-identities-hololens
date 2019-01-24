using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    public AudioSource IntroSound;

    /*public GameObject[] allObjects;
    public IList<GameObject> newObjects;*/

    public GameObject ludwigPortrait;
    public GameObject portraitludwig;
    public GameObject eduardoPortrait;
    public GameObject doctorPortrait;
    public GameObject malerPortrait;
    public GameObject MissionPlan;

    void Start () {
        StartCoroutine(Example());
        IntroSound.Play();

        /*hideAll();
        allObjects = GameObject.FindGameObjectsWithTag("Inventar");
        newObjects = new List<GameObject>(allObjects);*/
    }

    void Update () {
		if (!IntroSound.isPlaying)
        {
            SceneManager.LoadScene("SampleScene");
        }
	}

    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        setActiveObject(ludwigPortrait);

        yield return new WaitForSeconds(7);
        hideObject(ludwigPortrait);

        yield return new WaitForSeconds(8);
        setActiveObject(portraitludwig);

        yield return new WaitForSeconds(12);
        hideObject(portraitludwig);
        setActiveObject(eduardoPortrait);

        yield return new WaitForSeconds(13);
        hideObject(eduardoPortrait);
        setActiveObject(doctorPortrait);

        yield return new WaitForSeconds(7);
        hideObject(doctorPortrait);
        setActiveObject(malerPortrait);

        yield return new WaitForSeconds(5);
        hideObject(malerPortrait);

        yield return new WaitForSeconds(10);
        setActiveObject(MissionPlan);

        yield return new WaitForSeconds(10);
        hideObject(MissionPlan);
    }

    public void setActiveObject (GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(0, 0, 9);
    }

    public void hideObject (GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    /*public void hideAll ()
    {
        foreach (GameObject _gameObject in newObjects)
        {
            _gameObject.SetActive(false);
        }
    }*/
}

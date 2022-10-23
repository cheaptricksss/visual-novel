using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    private AudioSource buttonPress;

    private void Start()
    {
        buttonPress = GetComponent<AudioSource>();
    }

    //public = accessible to other componenets and scripts
    //to be able to use a function on a button press, it must be public
    public void ChangeScene()
    {
        buttonPress.Play();
        SceneManager.LoadScene(sceneToLoad);
    }
}

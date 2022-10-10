using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;

    //public = accessible to other componenets and scripts
    //to be able to use a function on a button press, it must be public
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

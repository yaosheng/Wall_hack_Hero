using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    public string sceneName;

    void Start( )
    {
        sceneName = SceneManager.GetActiveScene( ).name;
    }

    public void RestartThisScene( )
    {
        SceneManager.LoadScene(sceneName);
    }
}

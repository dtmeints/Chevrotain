using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMachine : MonoBehaviour
{
    public void ReloadScene() => StartCoroutine(SceneReload());
    
    public IEnumerator SceneReload()
    {
        yield return new WaitForSeconds(2f);
        string activeScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeScene);
    }

    public void OnEnable() => Player.OnDeath += ReloadScene;
    public void OnDisable() => Player.OnDeath -= ReloadScene;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextSceneAfterCooldown : MonoBehaviour
{
    public string NextSceneName;

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    
        SceneManager.LoadScene(NextSceneName);
    }

    void Start()
    {
        StartCoroutine(ExecuteAfterTime(15));
    }
}
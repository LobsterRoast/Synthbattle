using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class WaitAndReloadInstance : MonoBehaviour
{
    IEnumerator WaitAndReload()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        StartCoroutine(WaitAndReload());
    }
}
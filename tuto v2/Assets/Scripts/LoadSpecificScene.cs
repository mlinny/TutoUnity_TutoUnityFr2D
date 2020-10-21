
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{

    public string SceneName;
    private  Animator fadeSystem;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    public IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn"); 
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
            
        }
    }
}

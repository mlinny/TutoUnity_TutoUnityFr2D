using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objets;

    private void Awake()
    {
        foreach(var element in objets)
        {
            DontDestroyOnLoad(element);
        }
    }
}

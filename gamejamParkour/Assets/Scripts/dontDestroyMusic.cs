using UnityEngine;
using System.Collections;

public class dontDestroyMusic : MonoBehaviour 
{
    void Awake()
    {
    GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
    
    if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
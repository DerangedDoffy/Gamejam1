using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PortalBlock")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            Debug.Log("Portal collision is true");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RRR : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Carga la escena con el nombre especificado
        SceneManager.LoadScene(sceneName);
    }
}

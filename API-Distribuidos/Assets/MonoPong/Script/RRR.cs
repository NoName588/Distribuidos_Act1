using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class RRR : MonoBehaviour
{

    string url = "https://sid-restapi.onrender.com";
    public TMP_Text errorMessageText;


    public void LoadScene(string sceneName)
    {
        // Carga la escena con el nombre especificado
        SceneManager.LoadScene(sceneName);
    }
}

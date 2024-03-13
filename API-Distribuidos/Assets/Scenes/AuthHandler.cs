using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class AuthHandler : MonoBehaviour
{
    string url = "https://sid-restapi.onrender.com";
    public TMP_Text errorMessageText;

    void Start()
    {
       
    }

    public void ENTRARREGISTRO() { 


        AuthenticationData data = new AuthenticationData();   
        data.username = GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text;
        data.password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;

        StartCoroutine("Registro", JsonUtility.ToJson(data));
        


    }

    public void ENTRARINGRESO() {

        AuthenticationData data = new AuthenticationData();
        data.username = GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text;
        data.password = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>().text;

        StartCoroutine("LogIn", JsonUtility.ToJson(data));

    }


    IEnumerator Registro(string json)
    {
        UnityWebRequest request = UnityWebRequest.Put(url+"/api/usuarios",json);
        request.method = "POST";
        request.SetRequestHeader("Content-Type","application/json");

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                Debug.Log("registro hecho");
                errorMessageText.text = "YEY REGISTRO EXITOSO :D";

            }
            else
            {
                Debug.Log(request.responseCode + "|" + request.error);
                errorMessageText.text = "UPPS ALGO SALIO MAL";
            }
        }

    }

    IEnumerator LogIn(string json)
    {
        UnityWebRequest request = UnityWebRequest.Put(url + "/api/auth/login", json);
        request.method = "POST";
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
            errorMessageText.text = "UPPS ERROR DE CONEXION :c.";
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            if (request.responseCode == 200)
            {
                AuthenticationData data = JsonUtility.FromJson<AuthenticationData>(request.downloadHandler.text);
                Debug.Log(data.token);
                SceneManager.LoadScene("MonoPong");
                StartCoroutine("LogIn", json);
                
            }
            else
            {
                Debug.Log(request.responseCode + "|" + request.error);
                errorMessageText.text = "Algo salio mal, seguro que es tu cuenta?";

            }
        }

    }
}
[System.Serializable]
public class AuthenticationData
{
    public string username;
    public string password;
    public UsuarioData usuario;
    public string token;
}

[System.Serializable]
public class UsuarioData
{
    public string id;
    public string _username;


}
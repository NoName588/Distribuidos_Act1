using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class HttpHandler : MonoBehaviour
{
    public RawImage[] images;
    public TextMeshProUGUI usernameText; 

    private string FakeApiUrl = "https://my-json-server.typicode.com/NoName588/Prueba---Json";
    private string RickYMortyApiUrl = "https://rickandmortyapi.com/api";
    private Coroutine sendRequest_GetCharacters;
    private int nextImageIndex = 0;

    public void SendRequest(int userId)
    {
        nextImageIndex = 0;
        if (sendRequest_GetCharacters == null)
            sendRequest_GetCharacters = StartCoroutine(GetUserData(userId));
    }

    public void ShowUsername(string username)
    {
        if (usernameText != null)
        {
            usernameText.text = username;
        }
    }

    IEnumerator GetUserData(int uid)
    {
        UnityWebRequest request = UnityWebRequest.Get(FakeApiUrl + "/users/" + uid);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                UserData user = JsonUtility.FromJson<UserData>(request.downloadHandler.text);
                Debug.Log(user.username);

                // Mostrar el username en TextMeshPro
                ShowUsername(user.username);

                foreach (int cardid in user.deck)
                {
                    StartCoroutine(GetCharacter(cardid));
                }
            }
            else
            {
                Debug.Log(request.responseCode + "|" + request.error);
            }
        }
        sendRequest_GetCharacters = null;
    }


    IEnumerator GetCharacter(int id)
    {
        UnityWebRequest request = UnityWebRequest.Get(RickYMortyApiUrl + "/character/" + id);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Character character = JsonUtility.FromJson<Character>(request.downloadHandler.text);
                Debug.Log(character.name + " is a " + character.species);
                Debug.Log(character.image);

                if (nextImageIndex < images.Length)
                {
                    StartCoroutine(DownloadImage(character.image, nextImageIndex));
                    nextImageIndex++;
                }
            }
            else
            {
                Debug.Log(request.responseCode + "|" + request.error);
            }
        }
    }

    IEnumerator DownloadImage(string url, int index)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            images[index].texture = texture;
        }
    }
}

[System.Serializable]
public class UserData
{
    public int id;
    public string username;
    public int[] deck;
}

[System.Serializable]
public class CharactersList
{
    public charactersInfo info;
    public Character[] results;
}

[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public string species;
    public string image;
}

[System.Serializable]
public class charactersInfo
{
    public int count;
    public int pages;
    public string prev;
    public string next;
}

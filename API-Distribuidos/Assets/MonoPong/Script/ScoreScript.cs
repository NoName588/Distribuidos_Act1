using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    [SerializeField]
    public Button _saveScoreButton;

    public int Score = 0;

    //method for updating the score

    private void Reset()
    {
        _saveScoreButton = GetComponent<Button>();

    }

    void Start()
    {
        _saveScoreButton.onClick.AddListener(HandlerSaveScoreButtonClicked);
    }

    private void HandlerSaveScoreButtonClicked()
    {

        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(uid).Child("score").SetValueAsync(Score);
    }

    public void UpdateScore(int Delta)
    {
        Score += Delta;

        gameObject.GetComponent<Text>().text = Score.ToString();
    }

    //method used to reset the score
    public void ResetScore()
    {
        Score = 0;
        gameObject.GetComponent<Text>().text = Score.ToString();
    }
}

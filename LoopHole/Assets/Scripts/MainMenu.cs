using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI score;

    void Start() {
        score.text = PlayerPrefs.GetFloat("score").ToString("#.##");
    }
    public void PlayGame() {
        SceneManager.LoadScene("level");
    }

    public void QuitGame() {
        Debug.Log("quitting");
        Application.Quit();
    }

}

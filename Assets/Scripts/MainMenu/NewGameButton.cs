using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

    public void NewGame()
        => SceneManager.LoadScene("Game");

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(NewGame);
    }
}

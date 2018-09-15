using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    public void Menu()
        => SceneManager.LoadScene("MainMenu");

    private void Start()
        => GetComponent<Button>().onClick.AddListener(Menu);
}

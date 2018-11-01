﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void LoadScene(string sceneName)
        => SceneManager.LoadScene(sceneName);

    public void SaveGame()
        => FindObjectOfType<GameState>().Save();
}

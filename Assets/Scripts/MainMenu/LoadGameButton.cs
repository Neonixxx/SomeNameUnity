using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour {

    public void LoadGame()
    {
        FindObjectOfType<GameState>().Load();
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadGame);
    }
}

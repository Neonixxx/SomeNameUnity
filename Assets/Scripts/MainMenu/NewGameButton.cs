using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

    public void NewGame()
    {
        FindObjectOfType<GameState>().New();
        SceneManager.LoadScene("Game");
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(NewGame);
    }
}

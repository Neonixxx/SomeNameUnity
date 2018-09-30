using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FarmButton : MonoBehaviour {

    public void NewGame()
        => SceneManager.LoadScene("Farm");

    private void Start()
        => GetComponent<Button>().onClick.AddListener(NewGame);
}

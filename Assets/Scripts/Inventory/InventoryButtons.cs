using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryButtons : MonoBehaviour
{
    public void LoadGameScene()
        => SceneManager.LoadScene("Game");
}

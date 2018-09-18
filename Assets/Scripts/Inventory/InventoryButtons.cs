using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryButtons : MonoBehaviour
{
    public void LoadGameScene()
        => SceneManager.LoadScene("Game");
}

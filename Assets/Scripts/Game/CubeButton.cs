using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CubeButton : MonoBehaviour
{
    public void Cube()
        => SceneManager.LoadScene("Cube");

    private void Start()
        => GetComponent<Button>().onClick.AddListener(Cube);
}

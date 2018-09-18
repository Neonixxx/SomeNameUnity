using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour {

    public void Inventory()
        => SceneManager.LoadScene("Inventory");

    private void Start()
        => GetComponent<Button>().onClick.AddListener(Inventory);
}

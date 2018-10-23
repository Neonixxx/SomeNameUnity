using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class SaveButton : MonoBehaviour
    {
        private GameState _gameState;

        private void Start()
        {
            _gameState = FindObjectOfType<GameState>();
            GetComponent<Button>().onClick.AddListener(() => _gameState.Save());
        }
    }
}

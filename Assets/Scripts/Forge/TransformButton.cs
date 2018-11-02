using Forge;
using UnityEngine;
using UnityEngine.UI;

public class TransformButton : MonoBehaviour
{
    public Cube Cube;

    public void Transform()
        => Cube.Transform();

    private void Start()
        => GetComponent<Button>().onClick.AddListener(Transform);
}

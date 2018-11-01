using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public Sprite[] Images;

    private Dictionary<string, int> _images = new Dictionary<string, int>();

    void Start()
    {
        DontDestroyOnLoad(this);
        for (var i = 0; i < Images.Length; i++)
            _images.Add(Images[i].name, i);
    }

    public Sprite GetSprite(string id)
        => Images[_images[id]];
}

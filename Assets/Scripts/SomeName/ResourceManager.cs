using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public Sprite[] Images;

    private Dictionary<string, int> _images = new Dictionary<string, int>();

    private string[] _imagePaths = new string[]
    {
        "BeginnerSword.jpg",
        "SimpleSword.jpg",
        "SimpleChest.jpg",
        "SimpleGloves.jpg",
        "ScrollOfEnchantWeapon.jpg",
        "ScrollOfEnchantArmor.jpg",
    };

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        for (var i = 0; i < Images.Length; i++)
            _images.Add(Images[i].name, i);
        //var imagesCount = _imagePaths.Length;
        //Images = new Sprite[imagesCount];
        //for (int i = 0; i < imagesCount; i++)
        //    Images[i] = Resources.Load<Sprite>(_imagePaths[i]);
    }

    public Sprite GetSprite(string id)
        => Images[_images[id]];
}

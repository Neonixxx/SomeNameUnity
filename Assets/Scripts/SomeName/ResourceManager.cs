using UnityEngine;

public class ResourceManager : MonoBehaviour {

    private Sprite[] _images;

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
	void Start ()
    {
        var imagesCount = _imagePaths.Length;
        _images = new Sprite[imagesCount];
        for (int i = 0; i < imagesCount; i++)
            _images[i] = Resources.Load<Sprite>(_imagePaths[i]);
	}

    public Sprite GetImage(int id)
        => _images[id];
}

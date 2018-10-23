using System.IO;
using SomeName.Core.Domain;
using SomeName.Core.IO;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public PlayerIO PlayerIO { get; set; } = new PlayerIO();

    public Player Player { get; set; }

    public GameState New()
    {
        Player = PlayerIO.StartNew();
        PlayerIO.Save(Player);
        return this;
    }

    public GameState Load()
    {
        Player player;
        if (PlayerIO.TryLoad(out player))
        {
            Player = player;
            return this;
        }
        else
            throw new IOException("Не удалось загрузить сохранение");
    }

    // Use this for initialization
    void Start()
        => DontDestroyOnLoad(this);

    public void Save()
        => PlayerIO.Save(Player);
}

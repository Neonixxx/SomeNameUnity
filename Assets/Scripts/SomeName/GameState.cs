using System.Collections;
using System.Collections.Generic;
using SomeName.Core;
using SomeName.Core.Domain;
using UnityEngine;

public class GameState : MonoBehaviour {

    public Player Player { get; set; }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
        Player = PlayerIO.StartNew();
	}
}

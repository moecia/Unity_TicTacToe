using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public static UI_Manager Instance { get; private set; }

    public Text[] playerScores;

    public void Init()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

}

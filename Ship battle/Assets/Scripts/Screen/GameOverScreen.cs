using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour {

    public static GameOverScreen screen;
    void Start()
    {
        screen = this;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

    public Text a, b, c;

    // Use this for initialization
    void Start () {
        a.text = PlayerPrefs.GetString("nome1") + " - " + PlayerPrefs.GetInt("pontos1");
        b.text = PlayerPrefs.GetString("nome2") + " - " + PlayerPrefs.GetInt("pontos2");
        c.text = PlayerPrefs.GetString("nome3") + " - " + PlayerPrefs.GetInt("pontos3");
    }
}

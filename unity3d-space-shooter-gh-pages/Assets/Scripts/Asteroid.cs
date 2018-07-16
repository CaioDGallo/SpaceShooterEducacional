using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public string resposta = "";
    public TextMesh respostaText;
    bool acertou = false;

    private void Awake()
    {
        resposta = Random.Range(0.1f, 99.9f).ToString("#.00"); ;
        respostaText.text = resposta;
    }

    void Update () {
        if(respostaText != null)
        {
            respostaText.text = resposta;
        }
    }

    private void OnDestroy()
    {
        AsteroidManager.Instance.DestroyAsteroid(resposta);
    }
}

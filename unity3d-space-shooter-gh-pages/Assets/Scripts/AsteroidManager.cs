using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : Singleton<AsteroidManager> {

    public bool acertou = false;

    public void DestroyAsteroid(string resposta)
    {
        GameController.Instance.asteroids--;
        PlayerController.Instance.RotateBack();

        Debug.Log(resposta + " | " + GameController.Instance.respostaAtual);

        if (resposta == GameController.Instance.respostaAtual)
        {
            GameController.Instance.isProblemSolved = true;
            GameController.Instance.AddScore(10);
            acertou = true;

            foreach(GameObject g in GameController.Instance.asteroidsInScene)
            {
                Destroy(g);
            }
        }
        else
        {
            if (!acertou)
            {
                GameController.Instance.gameOver = true;
                GameController.Instance.GameOver();
            }
        }
    }
}

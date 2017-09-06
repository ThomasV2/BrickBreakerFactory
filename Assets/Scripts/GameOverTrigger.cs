using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverTrigger : MonoBehaviour {

    public GameObject GameManager;
    public Text Life;

    private int lives = 3;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (--lives <= 0)
            {
                Destroy(other.gameObject);
                GameManager.GetComponent<GameManager>().CheckEndGame(true);
            }
            else
            {
                other.transform.GetComponent<Ball>().Reload();
            }
            Life.text = " " + lives;
        }
    }
}

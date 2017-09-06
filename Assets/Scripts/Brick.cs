using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Brick : MonoBehaviour {

    public Sprite[] Sprites;

    private int solidity;
    public int Solidity{
        get { return solidity; }
        set
        {
            solidity = value;
            if (value != -1)
                this.GetComponent<SpriteRenderer>().sprite = Sprites[value];
            else
                this.GetComponent<SpriteRenderer>().sprite = Sprites[Sprites.Length - 1];
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (solidity != -1)
        {
            Solidity = solidity - 1;
            if (solidity <= 0)
            {
                GameObject.Find("FallingWall").GetComponent<GameManager>().CheckEndGame();
                Destroy(gameObject);
            }
        }
    }
}

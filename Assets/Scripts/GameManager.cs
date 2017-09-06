using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject BrickPrefab;
    public GameObject WinPanel;
    public GameObject LosePanel;

    private bool canFall = true;
    private float waitTime = 15f;
    private int nbBricks = 0;
    private int level;
    private int[,] tab = { {
                            0, 1, 1, 1, 1, 1, 1, 0,
                            0, 3, 3, 3, 3, 3, 3, 0,
                            0, 1, 1, 1, 1, 1, 1, 0,
                            0, 1, 1, 1, 1, 1, 1, 0,
                            0, 2, 2, 2, 2, 2, 2, 0,
                            0, 1, 1, 1, 1, 1, 1, 0,
                            0, 1, 1, 1, 1, 1, 1, 0,
                            0, 0, 0, 0, 0, 0, 0, 0,
                            },
                            {
                            0, 0, 0, 1, 1, 0, 0, 0,
                            0, 0, 1, 1, 1, 1, 0, 0,
                            0, 1, 9, 1, 1, 9, 1, 0,
                            0, 1, 9, 1, 1, 9, 1, 0,
                            0, 0, 3, 3, 3, 3, 0, 0,
                            0, 0, 2, 2, 2, 2, 0, 0,
                            0, 0, 1, 0, 0, 1, 0, 0,
                            0, 1, 0, 0, 0, 0, 1, 0,
                            },
                            {
                            2, 3, 1, 2, 3, 1, 2, 3,
                            1, 2, 3, 1, 2, 3, 1, 2,
                            3, 1, 2, 3, 1, 2, 3, 1,
                            2, 9, 9, 2, 3, 9, 9, 3,
                            1, 9, 9, 1, 2, 9, 9, 2,
                            3, 1, 2, 3, 1, 2, 3, 1,
                            2, 3, 1, 2, 3, 1, 2, 3,
                            1, 2, 3, 1, 2, 3, 1, 2,
                            }};

	// Use this for initialization
	void Start () {
        GameObject tmpBrick;
        this.level = PlayerPrefs.GetInt("CurrentLvl");

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                switch (tab[level - 1, y * 8 + x])
                {

                    case 1:
                        tmpBrick = Instantiate(BrickPrefab, new Vector2(BrickPrefab.transform.position.x + x * 0.7f + 0.15f, BrickPrefab.transform.position.y - (y + 1) * 0.45f), BrickPrefab.transform.rotation) as GameObject;
                        tmpBrick.transform.parent = this.transform;
                        tmpBrick.GetComponent<Brick>().Solidity = 1;
                        nbBricks++;
                        break;
                    case 2:
                        tmpBrick = Instantiate(BrickPrefab, new Vector2(BrickPrefab.transform.position.x + x * 0.7f + 0.15f, BrickPrefab.transform.position.y - (y + 1) * 0.45f), BrickPrefab.transform.rotation) as GameObject;
                        tmpBrick.transform.parent = this.transform;
                        tmpBrick.GetComponent<Brick>().Solidity = 2;
                        nbBricks++;
                        break;
                    case 3:
                        tmpBrick = Instantiate(BrickPrefab, new Vector2(BrickPrefab.transform.position.x + x * 0.7f + 0.15f, BrickPrefab.transform.position.y - (y + 1) * 0.45f), BrickPrefab.transform.rotation) as GameObject;
                        tmpBrick.transform.parent = this.transform;
                        tmpBrick.GetComponent<Brick>().Solidity = 3;
                        nbBricks++;
                        break;
                    case 9:
                        tmpBrick = Instantiate(BrickPrefab, new Vector2(BrickPrefab.transform.position.x + x * 0.7f + 0.15f, BrickPrefab.transform.position.y - (y + 1) * 0.45f), BrickPrefab.transform.rotation) as GameObject;
                        tmpBrick.transform.parent = this.transform;
                        tmpBrick.GetComponent<Brick>().Solidity = -1;
                        nbBricks++;
                        break;
                    default:
                        break;
                }
                
            }
        }
	}
    IEnumerator WaitAndFall(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
		GameObject ball = GameObject.Find ("Ball");
		while (ball.transform.position.y >= -3.5f)
			yield return null;
        this.transform.Translate(new Vector3(0f, -1f, 0f));
		if (this.transform.position.y >= 2)
        	canFall = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFall == true)
        {
            StartCoroutine(WaitAndFall(this.waitTime));
            canFall = false;
        }
    }

    public void CheckEndGame(bool isLose = false)
    {
        if (isLose == true)
        {
            Time.timeScale = 0f;
            LosePanel.SetActive(true);
        }
        else if (--nbBricks <= 0)
        {
            Time.timeScale = 0f;
            if (PlayerPrefs.GetInt("MaxLvl") < level + 1)
            {
                PlayerPrefs.SetInt("MaxLvl", level + 1);
                PlayerPrefs.Save();
            }
            WinPanel.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}

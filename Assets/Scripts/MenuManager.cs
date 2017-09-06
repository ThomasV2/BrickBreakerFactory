using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject Menus;
    public GameObject MainMenu;
    public GameObject LevelMenu;
    public GameObject OptionMenu;
    public Slider VolumeSlider;
    public float speed = 2f;

    private Vector2 previousPos;
    private Vector2 nextPos;
    
    public void Awake()
    {
        LevelMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width, 0f);
        OptionMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width, 0f);
        previousPos = Menus.GetComponent<RectTransform>().anchoredPosition;
        nextPos = previousPos;
        VolumeSlider.value = AudioListener.volume;
    }

    public void MoveMenus(int position)
    {
        previousPos = Menus.GetComponent<RectTransform>().anchoredPosition;
        nextPos = new Vector2(Screen.width * position, 0f);
        StartCoroutine(DoMove());
    }

    public IEnumerator DoMove()
    {
        float i = 0f;
        while (i < 1.0)
        {
            i += Time.deltaTime * speed;
            Menus.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(previousPos, nextPos, i);
            yield return null;
        }
    }

    public void ChangeVolumeSound()
    {
        AudioListener.volume = VolumeSlider.value;
    }

	public void QuitGame()
    {
        Application.Quit();
    }

	public void LoadGame(int level)
	{
        PlayerPrefs.SetInt("CurrentLvl", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
	}
}

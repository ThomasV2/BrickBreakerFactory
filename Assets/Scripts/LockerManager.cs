using UnityEngine;
using System.Collections;

public class LockerManager : MonoBehaviour {

    public GameObject[] Lockers;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("MaxLvl") == false)
        {
            PlayerPrefs.SetInt("MaxLvl", 1);
            PlayerPrefs.Save();
        }
    }

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < Lockers.Length && i < PlayerPrefs.GetInt("MaxLvl"); i++)
        {
            Lockers[i].SetActive(false);
        }
    }
}

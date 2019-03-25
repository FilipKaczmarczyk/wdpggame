using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable 0649

public class GameManager : MonoBehaviour
{
	Text over;

	private static GameManager instance;

	[SerializeField]
	private GameObject soulPrefab;

	[SerializeField]
	private Text soulTxt;

	private int collectedSouls;

	public static GameManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	public GameObject SoulPrefab
	{
		get
		{
			return soulPrefab;
		}

	}

	public int CollectedSouls
	{
		get
		{
			return collectedSouls;
		}
		set
		{
			soulTxt.text = value.ToString();
			this.collectedSouls = value;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		over = GameObject.Find("Win").GetComponent<Text>();
		over.color = new Color32(254, 152, 203, 0);
	}

	IEnumerator ChangeState()
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(0);
	}
	// Update is called once per frame
	void Update()
    {
        if(CollectedSouls == 1)
		{
			over.color = new Color32(254, 152, 203, 255);
			StartCoroutine(ChangeState());
			
		}
	}

}

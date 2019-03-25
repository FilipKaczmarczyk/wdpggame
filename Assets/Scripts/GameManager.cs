using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649

public class GameManager : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

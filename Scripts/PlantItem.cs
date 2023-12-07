using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;
    public Text nameTxt;
    public Text priceTxt;
    public Image icon;

	public Image btnImage;
	public Text btnTxt;

	FarmManager fm;

	void Start()
	{
		fm = FindObjectOfType<FarmManager>();
		InitalizeUI();
	}
	public void BuyPlant()
	{
		Debug.Log("Bought" + plant.plantName);
		fm.SelectPlant(this);
	}

	void InitalizeUI()
	{
		nameTxt.text = plant.plantName;
		priceTxt.text = "$" + plant.buyPrice;
		icon.sprite = plant.icon;
	}
}

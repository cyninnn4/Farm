using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoltManager : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    

    int plantStage = 0;
    float timer;

    PlantObject selectplant;
    SpriteRenderer plot;
    FarmManager fm;

    // Start is called before the first frame update
    void Start()
    {
        plot = GetComponent<SpriteRenderer>();
        fm = transform.parent.GetComponent<FarmManager>();
    }

    // Update is called once per frame
    void Update()
    {
		if (isPlanted)
		{

			timer -= Time.deltaTime;

			if (timer < 0 && plantStage < selectplant.plantStages.Length - 1)
			{
				timer = selectplant.timeBtwStages;
				plantStage++;
				UpdatePlant();
			}
		}

	}


	private void OnMouseDown()
	{
        if (isPlanted)
        {
            if (plantStage == selectplant.plantStages.Length - 1 && !fm.isPlanting)
            {
				Harvest();
			}
        }
        else if (fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money)
        {
            Plant(fm.selectPlant.plant);
        }
        Debug.Log("Clicker");
	}
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
		fm.Transaction(selectplant.sellPrice);
	}
    void Plant(PlantObject newPlant)
    {
        selectplant = newPlant;
        isPlanted= true;

        fm.Transaction(-selectplant.buyPrice);

        plantStage = 0;
        UpdatePlant();
        timer = selectplant.timeBtwStages;
        plant.gameObject.SetActive(true);
    }
    void UpdatePlant()
    {
        plant.sprite = selectplant.plantStages[plantStage];
    }
	private void OnMouseOver()
	{
		if (isPlanted)
		{
			plot.color = Color.red;
		}
		else
		{
			plot.color = Color.green;
		}
	}
	private void OnMouseExit()
	{
		plot.color = Color.white;
	}
}

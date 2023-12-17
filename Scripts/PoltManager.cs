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

    bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;

    float speed = 1f;
    public bool isBought = true;

    // Start is called before the first frame update
    void Start()
    {
        plot = GetComponent<SpriteRenderer>();
        fm = transform.parent.GetComponent<FarmManager>();
        if (isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            plot.sprite = unavailableSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (isPlanted && !isDry)
		{

			timer -= speed*Time.deltaTime;

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
            if (plantStage == selectplant.plantStages.Length - 1 && !fm.isPlanting && !fm.isSelecting)
            {
				Harvest();
			}
        }
        else if (fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money && isBought)
        {
            Plant(fm.selectPlant.plant);
        }
        if (fm.isPlanting)
        {
            switch (fm.selectedTool)
            {
                case 1:
                    if (isBought)
                    {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if(isPlanted)UpdatePlant();
                    }
                    break;
                case 2:
                    if (fm.money>=10)
                    {
                        fm.Transaction(-10);
                        if(speed < 2) speed += .2f;
                    }
                    break;
                case 3:
					if (fm.money >= 100 && !isBought)
					{
						fm.Transaction(-100);
						isBought = true;
                        plot.sprite = drySprite;
					}
					break;
                default:
                    break;
            }
        }
        Debug.Log("Clicker");
	}
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
		fm.Transaction(selectplant.sellPrice);
		isDry = true;
		plot.sprite = drySprite;
		speed = 1f;
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
		if (isPlanted||fm.selectPlant.plant.buyPrice > fm.money||!isBought)
		{
			plot.color = Color.red;
		}
		else
		{
			plot.color = Color.green;
		}
		if (fm.isSelecting)
		{
			switch (fm.selectedTool)
			{
				case 1:
				case 2:
					if (isBought && fm.money >= (fm.selectedTool - 1) * 10)
					{
						plot.color = Color.green;
					}
					else
					{
						plot.color = Color.red;
					}
					break;
				case 3:
					if (!isBought && fm.money >= 100)
					{
						plot.color = Color.green;
					}
					else
					{
						plot.color = Color.red;
					}
					break;
				default:
					plot.color = Color.red;
					break;
			}
		}
	}
	private void OnMouseExit()
	{
		plot.color = Color.white;
	}
}

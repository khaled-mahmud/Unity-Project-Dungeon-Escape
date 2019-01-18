using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public GameObject shopPanel;
	//variable for currentItemSelected
	public int currentSelectedItem;
	public int currentItemCost;

	private Player _player;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			_player = other.GetComponent<Player>();
			if(_player != null)
			{
				UIManager.Instance.OpenShop(_player.diamonds);
			}

			shopPanel.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			shopPanel.SetActive(false);
		}
		
	}

	public void SelectItem(int item)
	{
		//0 = flame sword
		//1 = boots of flight
		//2 = key to castle
		Debug.Log("Select Item() : " + item);

		//switch between item
		//case 0

		switch (item)
		{
			case 0: //flame sword
				UIManager.Instance.UpdateShopSelection(86);
				currentSelectedItem = 0;
				currentItemCost = 200;
				break;
			case 1: //boots of flight
				UIManager.Instance.UpdateShopSelection(-20);
				currentSelectedItem = 1;
				currentItemCost = 400;
				break;
			case 2: //key to castle
				UIManager.Instance.UpdateShopSelection(-126);
				currentSelectedItem = 2;
				currentItemCost = 100;
				break;
		}
	}

	//BuyItem Method
	public void BuyItem()
	{
		//check if player gems is greater than or equal to itemcost
		//if it is, than awardItem & subtract cost from player gems
		if (_player.diamonds >= currentItemCost)
		{
			//award item
			if(currentSelectedItem == 2)
			{
				GameManager.Instance.HasKeyToCastle = true;
			}

			_player.diamonds -= currentItemCost;
			UIManager.Instance.UpdateGemCount(_player.diamonds);
			Debug.Log("Purchased " + currentSelectedItem);
			Debug.Log("Remaining gems: " + _player.diamonds);
			shopPanel.SetActive(false);
		}
		//else cancel sale
		else
		{
			Debug.Log("You do not have enough gems. Closing shop");
			shopPanel.SetActive(false);
		}
	}
}

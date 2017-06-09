using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	GameObject menu;

	void Start()
	{
			menu = GameObject.FindGameObjectWithTag("Menu");
			print(menu);
	}

	public void loadMenu() {
		
			menu.SetActive(true);	

		}
}

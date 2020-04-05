/**************************************************************************************************
* THE OMICRON PROJECT
 *-------------------------------------------------------------------------------------------------
 * Copyright 2010-2018		Electronic Visualization Laboratory, University of Illinois at Chicago
 * Authors:										
 *  Arthur Nishimoto		anishimoto42@gmail.com
 *-------------------------------------------------------------------------------------------------
 * Copyright (c) 2010-2018, Electronic Visualization Laboratory, University of Illinois at Chicago
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted 
 * provided that the following conditions are met:
 * 
 * Redistributions of source code must retain the above copyright notice, this list of conditions 
 * and the following disclaimer. Redistributions in binary form must reproduce the above copyright 
 * notice, this list of conditions and the following disclaimer in the documentation and/or other 
 * materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND 
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE  GOODS OR SERVICES; LOSS OF 
 * USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
 * ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *************************************************************************************************/
 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GroundMenu : MonoBehaviour {

    public Selectable[] menuItems;
    public int currentItem = 0;

    PointerEventData pointerData;

    public bool showMenu;
    float newScale = 0;
    float currentScale;
    public float showMenuSpeed = 5;

    public MenuManager menuManager;
    public bool activeMenu = false;
    public GroundMenu previousMenu;

    public float menuProgress;
    public GameObject instantiatedOriginPlane;

    float maxScale = 1;

    // Use this for initialization
    void Start () {
        menuManager = GetComponentInParent<MenuManager>();
        instantiatedOriginPlane = menuManager.instantiatedOrigin;
        Debug.Log(instantiatedOriginPlane);
        pointerData = new PointerEventData(EventSystem.current);

        if(menuItems.Length > 0)
        {
            menuItems[currentItem].OnSelect(pointerData);
        }
        if(!showMenu)
        {
            transform.localScale = Vector3.zero;
            menuProgress = 0;
            activeMenu = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (showMenu)
        {
            newScale = maxScale;
        }
        else
        {
            newScale = 0;
        }

          UpdateScale();

        if (newScale > 0)
        {
            menuProgress = currentScale / newScale;
            if(showMenu)
                activeMenu = true;
        }

        if (showMenu && activeMenu && menuProgress > 0.5f && CAVE2.IsMaster())
        {
            OnInput();
        }
    }

    void OnInput()
    {
        if (CAVE2.Input.GetButtonDown(menuManager.menuWandID, CAVE2.Button.ButtonDown))
        {
            if (currentItem < menuItems.Length - 1 && menuItems[currentItem + 1].IsActive() )
            {
                this.MenuNextItemDown();
                //CAVE2.SendMessage(gameObject.name, "MenuNextItemDown");
            }
            else if(currentItem >= menuItems.Length - 1)
            {
                this.MenuSetItem(0);
                //CAVE2.SendMessage(gameObject.name, "MenuSetItem", 0);
            }
        }
        if (CAVE2.Input.GetButtonDown(menuManager.menuWandID, CAVE2.Button.ButtonUp))
        {
            if (currentItem > 0 && menuItems[currentItem - 1].IsActive())
            {
                this.MenuNextItemUp();
                //CAVE2.SendMessage(gameObject.name, "MenuNextItemUp");
            }
            else if (currentItem <= 0)
            {
                this.MenuSetItem(menuItems.Length-1);
                //CAVE2.SendMessage(gameObject.name, "MenuSetItem", menuItems.Length - 1);
            }
        }

        if(CAVE2.Input.GetButtonDown(menuManager.menuWandID, menuManager.selectButton))
        {
            // this.CloseMenu();
            // this.MenuSelectItem();
            //CAVE2.SendMessage(gameObject.name, "CloseMenu");
            //CAVE2.SendMessage(gameObject.name, "MenuSelectItem");
            if(menuManager.mainMenu != this ){
                this.CloseMenu();
                menuManager.mainMenu.CloseMenu();
            } 
            else {
                this.CloseMenu();
            }
            this.MenuSelectItem();
        }

        if (CAVE2.Input.GetButtonDown(menuManager.menuWandID, CAVE2.Button.ButtonLeft))
        {
            this.MenuNextItemLeft();
            //CAVE2.SendMessage(gameObject.name, "MenuNextItemLeft");
        }
        if (CAVE2.Input.GetButtonDown(menuManager.menuWandID, CAVE2.Button.ButtonRight))
        {
            this.MenuNextItemRight();
            //CAVE2.SendMessage(gameObject.name, "MenuNextItemRight");
        }
    }

    void UpdateScale()
    {
        currentScale = transform.localScale.x;
        currentScale += (newScale - currentScale) * Time.deltaTime * showMenuSpeed;
        if (Mathf.Abs(currentScale - newScale) > 0.001)
        {
            transform.localScale = Vector3.one * maxScale * currentScale;
        }
        else if (showMenu)
        {
            transform.localScale = Vector3.one * maxScale;
        }
        else
        {
            transform.localScale = Vector3.zero;
            menuProgress = 0;
            activeMenu = false;
        }
    }

    public void SetWandAngle(Vector3 angleOffset, Vector3 distOffset)
    {
        transform.position = Vector3.zero + Quaternion.Euler(angleOffset) * distOffset;
        transform.eulerAngles = angleOffset;
    }

    public void OpenMenu()
    {
       showMenu = true;

        if( showMenu )
        {
            if(menuManager.mainMenu != this )
                previousMenu = menuManager.currentMenu;

            menuManager.currentMenu = this;

            activeMenu = true;

            if (previousMenu)
            {
                previousMenu.showMenu = false;
                transform.position = previousMenu.transform.position;
            }
            newScale = maxScale;
            menuManager.openMenus++;
            menuManager.PlayOpenMenuSound();
        }
    }

     public void CloseMenu()
    {
        showMenu = false;
        if( !showMenu )
        {
            if(previousMenu)
            {
                previousMenu.showMenu = true;
                menuManager.currentMenu = previousMenu;
            }
            newScale = 0;
            activeMenu = false;
            menuManager.openMenus--;
            menuManager.PlayCloseMenuSound();
        }
    }

    public void ToggleMenu()
    {
        if (GetComponent<UndockMenu>() && GetComponent<UndockMenu>().undocked)
        {
            GetComponent<UndockMenu>().undocked = false;
        }

        showMenu = !showMenu;
        if( showMenu )
        {
            if(menuManager.mainMenu != this )
                previousMenu = menuManager.currentMenu;

            menuManager.currentMenu = this;

            activeMenu = true;

            if (previousMenu)
            {
                previousMenu.showMenu = false;
                transform.position = previousMenu.transform.position;
            }

            menuManager.openMenus++;
            menuManager.PlayOpenMenuSound();
        }
        else
        {
            if(previousMenu)
            {
                previousMenu.showMenu = true;
                menuManager.currentMenu = previousMenu;
            }
            activeMenu = false;
            menuManager.openMenus--;
            menuManager.PlayCloseMenuSound();
        }
    }

    public void MenuNextItemDown()
    {
        menuItems[currentItem].OnDeselect(pointerData);
        currentItem++;
        menuItems[currentItem].OnSelect(pointerData);
        menuManager.PlayScrollMenuSound();
    }

    public void MenuNextItemUp()
    {
        menuItems[currentItem].OnDeselect(pointerData);
        currentItem--;
        menuItems[currentItem].OnSelect(pointerData);
        menuManager.PlayScrollMenuSound();
    }

    public void MenuNextItemLeft()
    {
        if (menuItems[currentItem].GetType() == typeof(Slider))
        {
            ((Slider)menuItems[currentItem]).value = ((Slider)menuItems[currentItem]).value - 1;
        }
        menuManager.PlayScrollMenuSound();
    }

    public void MenuNextItemRight()
    {
        if (menuItems[currentItem].GetType() == typeof(Slider))
        {
            ((Slider)menuItems[currentItem]).value = ((Slider)menuItems[currentItem]).value + 1;
        }
        menuManager.PlayScrollMenuSound();
    }

    public void MenuSetItem(int id)
    {
        if (menuItems[id] != null && menuItems[id].IsActive())
        {
            menuItems[currentItem].OnDeselect(pointerData);
            currentItem = id;
            menuItems[currentItem].OnSelect(pointerData);
            menuManager.PlayScrollMenuSound();
        }
    }

    public void MenuSelectItem()
    {
        if (menuItems[currentItem].GetType() == typeof(Button))
        {
            // if plane reference exists
            if (instantiatedOriginPlane) {
                // call correct path generation depending on menu selection
                string buttonText = ((Button)menuItems[currentItem]).GetComponentInChildren<Text>().text;
                if (buttonText.Equals("Push Back >")){
                    Debug.Log(menuManager.instantiatedOrigin);
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().PushToggleButton());
                }
                else if (buttonText.Equals("Taxi >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().TaxiToggleButton());
                }
                else if (buttonText.Equals("Hold Position >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().holdPosition());
                }
                else if (buttonText.Equals("Hold Position >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().holdPosition());
                }
                else if (buttonText.Equals("Take Off >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().TakeOfPermission());
                }
                else if (buttonText.Equals("Runway A >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().RunwayAToggleButton());
                }
                else if (buttonText.Equals("Runway B >")){
                    ((Button)menuItems[currentItem]).onClick.AddListener(() => instantiatedOriginPlane.GetComponent<PathGenerateAuto>().RunwayBToggleButton());
                }
            }
            ((Button)menuItems[currentItem]).OnPointerClick(pointerData);
        }
        menuManager.PlaySelectMenuSound();
    }
}

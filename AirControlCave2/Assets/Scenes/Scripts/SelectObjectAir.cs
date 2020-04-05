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

public class SelectObjectAir : CAVE2Interactable {

    public enum HoldingStyle { ButtonPress };

    [SerializeField]
    public bool grabbed;

    [SerializeField]
    CAVE2.Button grabButton = CAVE2.Button.Button3;

    [SerializeField]
    CAVE2.InteractionType grabStyle = CAVE2.InteractionType.Any;

    [SerializeField]
    HoldingStyle holdInteraction = HoldingStyle.ButtonPress;

    [SerializeField]
    Transform grabber;

    int grabbingWandID;

    [Header("Visuals")]
    GameObject pointingOverHighlight;
    new MeshRenderer renderer;

    [SerializeField]
    bool showPointingOver = true;

    [SerializeField]
    float highlightScaler = 1.05f;

    [SerializeField]
    Mesh defaultMesh;

    [SerializeField]
    Material pointingOverMaterial;

    Color originalPointingMatColor;

    [SerializeField]
    bool showTouchingOver = true;

    [SerializeField]
    Material touchingOverMaterial;

    Color originalTouchingMatColor;
    public GameObject menu;

    AirMenuManager menuManager;

    public string gameObjectName;


    private void Start()
    {
        // Visuals
        pointingOverHighlight = new GameObject("wandHighlight");
        pointingOverHighlight.transform.parent = transform;
        pointingOverHighlight.transform.position = transform.position;
        pointingOverHighlight.transform.rotation = transform.rotation;
        pointingOverHighlight.transform.localScale = Vector3.one * highlightScaler;

        if (defaultMesh == null)
        {
            defaultMesh = GetComponent<MeshFilter>().mesh;
        }
        pointingOverHighlight.AddComponent<MeshFilter>().mesh = defaultMesh;
        MeshCollider wandCollider = gameObject.AddComponent<MeshCollider>();
        wandCollider.inflateMesh = defaultMesh;
        wandCollider.convex = true;
        wandCollider.isTrigger = true;

        renderer = pointingOverHighlight.AddComponent<MeshRenderer>();

        if (pointingOverMaterial == null)
        {
            // Create a basic highlight material
            pointingOverMaterial = new Material(Shader.Find("Standard"));
            pointingOverMaterial.SetColor("_Color", new Color(0, 1, 1, 0.25f));
            pointingOverMaterial.SetFloat("_Mode", 3); // Transparent
            pointingOverMaterial.SetFloat("_Glossiness", 0);
        }
        else
        {
            pointingOverMaterial = new Material(pointingOverMaterial);
        }
        if (touchingOverMaterial == null)
        {
            // Create a basic highlight material
            touchingOverMaterial = new Material(Shader.Find("Standard"));
            touchingOverMaterial.SetColor("_Color", new Color(0, 1, 1, 0.25f));
            touchingOverMaterial.SetFloat("_Mode", 3); // Transparent
            touchingOverMaterial.SetFloat("_Glossiness", 0);
        }
        else
        {
            touchingOverMaterial = new Material(touchingOverMaterial);
        }
        originalPointingMatColor = pointingOverMaterial.color;
        originalTouchingMatColor = touchingOverMaterial.color;

        renderer.sharedMaterial = pointingOverMaterial;

        renderer.enabled = false;

        menu = (GameObject)Instantiate(menu);
        menuManager = menu.GetComponent<AirMenuManager>();
        Debug.Log(gameObject);
        menuManager.instantiatedOrigin = gameObject;
    }
    void Update()
    {
        // Interaction
        UpdateWandOverTimer();

        // Visuals
        if (showPointingOver)
        {
            if (wandPointing)
            {
                renderer.sharedMaterial = pointingOverMaterial;
                pointingOverMaterial.color = originalPointingMatColor;
                renderer.enabled = true;
            }
            else
            {
                renderer.enabled = false;
            }
        }
        if (showTouchingOver)
        {
            if (wandTouching)
            {
                renderer.sharedMaterial = touchingOverMaterial;
                touchingOverMaterial.color = originalTouchingMatColor;
                renderer.enabled = true;
            }
            else if(!wandPointing)
            { 
                renderer.enabled = false;
            }
        }
    }

    new void OnWandButtonDown(CAVE2.WandEvent evt)
    {
        if( evt.button == grabButton)
        {
            if (!grabbed && (evt.interactionType == grabStyle || grabStyle == CAVE2.InteractionType.Any))
            {
                grabber = CAVE2.GetWandObject(evt.wandID).transform;
                OnWandGrab();
                grabbingWandID = evt.wandID;
            }
            else if(grabbed && holdInteraction == HoldingStyle.ButtonPress)
            {
                OnWandGrabRelease();
            }
        }
    }

    void OnWandGrab()
    {
       //Debug.Log(gameObject.name);
       menuManager.OpenMenuManager();
       //GetComponentInChildren<PlaneMenuManager>().OpenMenuManager();
       //gameObjectName = gameObject.name;
       //Debug.Log(gameObjectName);
        grabbed = true;
    }

    void OnWandGrabRelease()
    {
        //Debug.Log("plane not selected onWandGrabRelease");
        menuManager.CloseMenuManager();
        //GetComponentInChildren<PlaneMenuManager>().CloseMenuManager();
        grabbed = false;
    }
}
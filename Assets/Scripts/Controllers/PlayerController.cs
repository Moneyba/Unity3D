﻿using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Debug.Log(hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                // Move our player to what we hit

                // Stop focusing any objects

                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {

                // check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
               //if we did set it as our focus
               if(interactable != null)
                {
                    SetFocus(interactable);
                }
                
            }
        }   


    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {

            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;           
            motor.FollowTarget(newFocus);
            
        }
        
        newFocus.OnFocused(transform);
        //motor.MoveToPoint(newFocus.transform.position);
       
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowingTarget();
    }
}

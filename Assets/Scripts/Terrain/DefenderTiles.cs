using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderTiles : MonoBehaviour
{
    public LayerMask IgnoreMouse; 
    public ButtonManager _buttonManager;
    public MeshRenderer meshRenderer;
    public DefenderSpawnManager _defenderSpawn;

    public bool hasTower;
    // Start is called before the first frame update
    void Start()
    {
        hasTower = false;
        IgnoreMouse = LayerMask.NameToLayer("IgnoreMouse");
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _buttonManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        _defenderSpawn = GameObject.FindGameObjectWithTag("DefenderSpawnManager").GetComponent<DefenderSpawnManager>();
    }

    IEnumerator MouseCheck()
    {
        yield return new WaitForSeconds(1);
    }
    private void OnMouseDown()
    {
       /* meshRenderer.material.color = Color.green;
        _defenderSpawn.SelectTile(this.gameObject);*/
      
    }

    private void OnMouseOver()
    {
        meshRenderer.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = Color.green;
    }

    void OnMouseDownCustom()
    {
        meshRenderer.material.color = Color.green;
        _defenderSpawn.SelectTile(this.gameObject);
    }
    
    void OnMouseOverCustom()
    {
        meshRenderer.material.color = Color.yellow;
    }

    void OnMouseExitCustom()
    {
        meshRenderer.material.color = Color.green;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] clickHits = Physics.RaycastAll(ray, Mathf.Infinity, ~IgnoreMouse);

            foreach (RaycastHit hit in clickHits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    OnMouseDownCustom();
                    /* if (hoverHit.collider.gameObject == gameObject && meshRenderer.material.color == Color.yellow)
                     {
                         OnMouseExitCustom();
                     }*/
                
                }
            }
          
        }
        
        Ray hoverRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        RaycastHit[] hoverHits = Physics.RaycastAll(hoverRay, Mathf.Infinity, ~IgnoreMouse);

        List<GameObject> hoverHitsList = new List<GameObject>();
        foreach (RaycastHit hoverHit in hoverHits)
        {
            hoverHitsList.Add(hoverHit.collider.gameObject);
            /*if (hoverHit.collider.gameObject == gameObject && meshRenderer.material.color == Color.green)
            {
                    OnMouseOverCustom();
            }
           else
            {
                OnMouseExitCustom();
            }*/
        }

        if (hoverHitsList.Contains(gameObject))
        {
            meshRenderer.material.color = Color.yellow;
        }
        else
        {
            meshRenderer.material.color = Color.green;
        }
        hoverHitsList.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Detect : MonoBehaviour, IPointerDownHandler
    {
        public GameObject selectedObject;
        public Board board;

        void Start()
        {
            addPhysics2DRaycaster();
        }

        void Update()
        {
            // Check for mouse input
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
                {
                    if (hitData && Input.GetMouseButtonDown(0))
                    {
                        selectedObject = hitData.transform.gameObject;
                        if (selectedObject.tag == "Place")
                        {
                            Vector3 vec = selectedObject.transform.position;
                            board.afficheTourelle(vec.x, vec.y);
                        }
                    }
                }
            }
        }

        void addPhysics2DRaycaster()
        {
            Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
            if (physicsRaycaster == null)
            {
                Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        }

        //Implement Other Events from Method 1
    }
}
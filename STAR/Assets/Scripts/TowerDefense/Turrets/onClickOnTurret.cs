using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onClickOnTurret : MonoBehaviour
{

    [SerializeField]
    private Canvas turretUI;
    [SerializeField]
    private GameObject sell;
    [SerializeField]
    private GameObject upgrade;
    [SerializeField]
    private GameObject turretManager;

    // Start is called before the first frame update
    void Start()
    {
        turretUI.enabled = false;
    }


    private void OnMouseDown()
    {
 //       turretUI.enabled = true;
    }

    public void onClickUITurretButton(string Name)
    {
        switch (Name)
        {
            case "upgrade":
                Debug.Log("tourelle Upgraded");
                break;
            case "sell":
                Debug.Log("tourelle Sold");
                break;
        }
    }
    private void onEscapeDown()
    {
        turretUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onEscapeDown();
        }
    }
}

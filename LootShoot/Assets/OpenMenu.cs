using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject menuHolder;
    public GameObject menuPrefab;
    public OpenInventory openInv;
    GameObject menuInstance;

    public void Open(bool open)
    {
        openInv.OpenOrClose(open, false); //set all components needed to open inventory without opening inventory

        if (open)
        {
            openInv.open = true; // so inv wont open when closing menu
            Time.timeScale = 0;
            menuInstance = Instantiate(menuPrefab, menuHolder.transform);
        }
        if (!open)
        {
            Time.timeScale = 1;
            Destroy(menuInstance);
        }
    }

    public void Update()
    {
        if (menuInstance != null && Input.GetKeyDown(KeyCode.Tab))
        {
            Open(false);
        }
    }
}

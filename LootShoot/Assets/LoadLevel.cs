using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string LevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inv = other.GetComponent<InventoryInstance>().myInv;
            other.GetComponent<SaveFunctions>().Save(inv);
            SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
        }
    }
}

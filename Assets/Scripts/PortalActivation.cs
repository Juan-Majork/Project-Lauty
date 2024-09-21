using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    bool isEmpty = false;
    GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        portal = GameObject.FindGameObjectWithTag("Portal");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject coin in coins)
        {
            if (coin != null)
            {
                isEmpty = false;
                return;
            }
            isEmpty = true;
        }
        if (isEmpty)
        {
            portal.GetComponent<Portal>().Check(true);
        }
    }
}

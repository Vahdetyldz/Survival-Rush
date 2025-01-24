using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    public GameObject pistol;
    public GameObject heavyMachine;
    public GameObject submachine;
    public Transform playerTransform;
    private PlayerController player;
    public GameManager gameManager;
    public void BuyMaxHealth()
    {
        if (Buy(28))
        {
            player.playerHealth += 2;
        }
    }
    public void BuyPistol()
    {
        if (Buy(30))
        {
            GameObject newWeapon = Instantiate(pistol, playerTransform.position, Quaternion.identity);
            player.AddWeapon(newWeapon);
        }  
    }
    public void BuySubmachineGun()
    {
        if (Buy(37))
        {
            GameObject newWeapon = Instantiate(submachine, playerTransform.position, Quaternion.identity);
            player.AddWeapon(newWeapon);
            
        }
    }
    public void BuySpeed()
    {
        if (Buy(50))
        {
            player.speed += player.speed / 10;
        }
    }
    public void BuyHeavyMachineGun()
    {
        if (Buy(105))
        {
            GameObject newWeapon = Instantiate(heavyMachine, playerTransform.position, Quaternion.identity);
            player.AddWeapon(newWeapon);
        }
    }
    public bool Buy(int cost)
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (cost <= player.playerGold)
        {
            player.playerGold -= cost;
            return true;
        }
        else 
        {
            return false;
        }
    }
    public void DevamEt()
    {
        /*cüzdaný kapat*/
        gameManager.ClosePurchaseMenu();
    }
}

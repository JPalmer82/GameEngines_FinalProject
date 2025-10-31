using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddAmmo : MonoBehaviour
{
    public ThirdPersonController player;

    public WeaponsScript mWeaponsScript;

    private void Start()
    {
        mWeaponsScript = player.GetComponent<WeaponsScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mWeaponsScript.assaultRifle_CurrentAmmo = mWeaponsScript.assaultRifle_MaxAmmo;
            mWeaponsScript.mauler_CurrentAmmo = mWeaponsScript.mauler_MaxAmmo;
            mWeaponsScript.hellWailer_CurrentAmmo = mWeaponsScript.hellWailer_MaxAmmo;
            if (mWeaponsScript.currentWeapon == "AssaultRifle")
            {
                mWeaponsScript.ammoCount.text = "Ammo: " + mWeaponsScript.assaultRifle_CurrentAmmo.ToString() + "/" + mWeaponsScript.assaultRifle_MaxAmmo;
            }
            else if (mWeaponsScript.currentWeapon == "mauler")
            {
                mWeaponsScript.ammoCount.text = "Ammo: " + mWeaponsScript.mauler_CurrentAmmo.ToString() + "/" + mWeaponsScript.mauler_MaxAmmo;
            }
            else if (mWeaponsScript.currentWeapon == "hellwailer")
            {
                mWeaponsScript.ammoCount.text = "Ammo: " + mWeaponsScript.hellWailer_CurrentAmmo.ToString() + "/" + mWeaponsScript.hellWailer_MaxAmmo;
            }

            Destroy(gameObject);
        }
    }
}

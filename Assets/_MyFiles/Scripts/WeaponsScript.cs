using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine.Animations.Rigging;

public class WeaponsScript : MonoBehaviour
{
    public string currentWeapon;
    InventoryScript inventory;

    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;

    [SerializeField] private Rig rig;

    [Header("3rd Person Weapons")]
    public GameObject weaponHolder;
    public GameObject assaultRifle;
    public GameObject mauler;
    public GameObject hellWailer;

    [Header("Weapons & Settings")]
    public GameObject hellWailerBulletPrefab;
    public GameObject bulletPrefab;
    public GameObject maulerBulletPrefab;
    public Transform hellWailerBulletSpawn;
    public Transform bulletSpawn;
    public Transform maulerBulletSpawn;
    public float hellWailerBulletForce = 30f;
    public float bulletForce = 10f;
    public float maulerBulletForce = 15f; 
    public float rayRange = 1000000f;
    public Camera player1_Camera;
    public TrailRenderer BulletTrail;
    public TrailRenderer shotgunBulletTrail;
    public ParticleSystem ImpactParticleSystem;
    public int maulerPellets = 8;
    public AudioSource assaultRifle_Sound;
    public AudioSource hellWailer_Sound;
    public AudioSource mauler_Sound;

    [Header("Weapon Ammo")]
    public int currentWeapon_Ammo = 0;
    public int currentWeapon_MaxAmmo = 0;
    public int assaultRifle_CurrentAmmo = 0;
    public int assaultRifle_MaxAmmo = 100;
    public int hellWailer_CurrentAmmo = 0;
    public int hellWailer_MaxAmmo = 10;
    public int mauler_CurrentAmmo = 0;
    public int mauler_MaxAmmo = 20;

    [Header("Weapon FireRates")]
    public float assaultRifle_FireRate = 1f;
    public float hellWailer_FireRate = 1f;
    public float mauler_FireRate = 1f;
    private float nextFireChance = 0f;

    [SerializeField] public TextMeshProUGUI ammoCount;


    // Start is called before the first frame update
    private void Start()
    {
        inventory = GetComponent<InventoryScript>();
        //assaultRifle.SetActive(true);
        hellWailer.SetActive(false);
        mauler.SetActive(false);
        assaultRifle_CurrentAmmo = 300;
        currentWeapon_MaxAmmo = assaultRifle_MaxAmmo;
        currentWeapon_Ammo = assaultRifle_CurrentAmmo;
        ammoCount.text = "Ammo: " + assaultRifle_CurrentAmmo.ToString() + "/" + assaultRifle_MaxAmmo;
        rig.weight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireChance && currentWeapon_Ammo > 0)
        {
            if(currentWeapon != "mauler")
            {
                Shoot();
            }
            else
            {
                mauler_CurrentAmmo--;
                currentWeapon_Ammo = mauler_CurrentAmmo;
                ammoCount.text = "Ammo: " + mauler_CurrentAmmo.ToString() + "/" + mauler_MaxAmmo;
                for (int i = 0; i< maulerPellets; i++)
                {
                    Shoot();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CheckItem(2);
        }
    }

    private void Shoot()
    {
        if (currentWeapon == "AssaultRifle")
        {
            Debug.Log("Got this far");
            nextFireChance = Time.time + 1f / assaultRifle_FireRate;

            muzzleFlash.Play();
            assaultRifle_Sound.Play();

            RaycastHit hit;
            if(Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, rayRange))
            {
                Debug.DrawRay(bulletSpawn.transform.position, bulletSpawn.transform.forward * rayRange, Color.red, 0.1f);
                Debug.Log(hit.transform.tag);

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Hit enemy");
                    hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(1);
                }
                if (hit.collider.gameObject.CompareTag("Boss"))
                {
                    Debug.Log("Hit Boss");
                    hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(1);
                }

                TrailRenderer trail = Instantiate(BulletTrail, bulletSpawn.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));
            }

            assaultRifle_CurrentAmmo--;
            currentWeapon_Ammo = assaultRifle_CurrentAmmo;
            ammoCount.text = "Ammo: " + assaultRifle_CurrentAmmo.ToString() + "/" + assaultRifle_MaxAmmo;
        }
        if (currentWeapon == "mauler")
        {
            nextFireChance = Time.time + 1f / mauler_FireRate;

            muzzleFlash.Play();
            mauler_Sound.Play();

            Vector3 direction = maulerBulletSpawn.transform.forward;
            Vector3 spread = Vector3.zero;
            spread += maulerBulletSpawn.transform.up * Random.Range(-1f, 1f);
            spread += maulerBulletSpawn.transform.right * Random.Range(-1f, 1f);
            direction += spread.normalized * Random.Range(0f, 0.2f); //This is all making te shotgun bullet spread

            RaycastHit hit;
            if (Physics.Raycast(maulerBulletSpawn.transform.position, direction, out hit, rayRange))
            {
                Debug.DrawRay(maulerBulletSpawn.transform.position, direction * rayRange, Color.red, 0.1f);
                Debug.Log(hit.transform.tag);

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Hit enemy");
                    hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(5);
                }
                if (hit.collider.gameObject.CompareTag("Boss"))
                {
                    Debug.Log("Hit Boss");
                    hit.collider.gameObject.GetComponent<EnemyScript>().TakeDamage(5);
                }

                TrailRenderer trail = Instantiate(shotgunBulletTrail, maulerBulletSpawn.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));

            }
        }
        if (currentWeapon == "hellwailer")
        {
            nextFireChance = Time.time + 1f / hellWailer_FireRate;

            muzzleFlash.Play();
            hellWailer_Sound.Play();

            GameObject hellWailerRocketClone = Instantiate(hellWailerBulletPrefab, hellWailerBulletSpawn.transform.position, hellWailerBulletSpawn.transform.rotation);
            Rigidbody rbody = hellWailerRocketClone.GetComponent<Rigidbody>();
            rbody.AddForce(hellWailerBulletSpawn.transform.forward * hellWailerBulletForce, ForceMode.Impulse);

                hellWailer_CurrentAmmo--;
                currentWeapon_Ammo = hellWailer_CurrentAmmo;
                ammoCount.text = "Ammo: " + hellWailer_CurrentAmmo.ToString() + "/" + hellWailer_MaxAmmo;
        }

    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;

        }
        Trail.transform.position = Hit.point;

        Destroy(Trail.gameObject, Trail.time);

    }

    public void CheckItem(int slotNum)
    {
        string weaponSlot = inventory.itemNames[slotNum];

        switch (weaponSlot)
        {
            case "AssaultRifle":
                currentWeapon = "AssaultRifle";
                hellWailer.SetActive(false);
                mauler.SetActive(false);
                assaultRifle.SetActive(true);
                currentWeapon_Ammo = assaultRifle_CurrentAmmo;
                currentWeapon_MaxAmmo = assaultRifle_MaxAmmo;
                ammoCount.text = "Ammo: " + assaultRifle_CurrentAmmo.ToString() + "/" + assaultRifle_MaxAmmo;
                break;
            case "mauler":
                currentWeapon = "mauler";
                hellWailer.SetActive(false);
                assaultRifle.SetActive(false);
                mauler.SetActive(true);
                currentWeapon_Ammo = mauler_CurrentAmmo;
                currentWeapon_MaxAmmo = mauler_MaxAmmo;
                ammoCount.text = "Ammo: " + mauler_CurrentAmmo.ToString() + "/" + mauler_MaxAmmo;
                break;
            case "hellwailer":
                currentWeapon = "hellwailer";
                assaultRifle.SetActive(false);
                mauler.SetActive(false);
                hellWailer.SetActive(true);
                currentWeapon_Ammo = hellWailer_CurrentAmmo;
                currentWeapon_MaxAmmo = hellWailer_MaxAmmo;
                ammoCount.text = "Ammo: " + hellWailer_CurrentAmmo.ToString() + "/" + hellWailer_MaxAmmo;
                break;
        }


    }
}

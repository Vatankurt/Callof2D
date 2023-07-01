using System;
using Photon.Pun;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class Pistol : MonoBehaviourPun
{
    public const int MAX_AMMO = 20;
    private const int GUN_DAMAGE = 1;
    private const float BULLET_SPEED = 20f;
    
    public Action<int> AmmoChanged;
    public AudioClip ShootSound;

    [SerializeField]
    private int STARTING_AMMO = 20;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Bullet bulletPrefab;

    private PlaySound playSound;
    private readonly float bulletForce = BULLET_SPEED;
    private int ammo;
    private Player player;

    private void Awake()
    {
        playSound = GetComponentInChildren<PlaySound>(true);
    }
    private void Start()
    {
        player = GetComponentInParent<Player>(true);
        UpdateAmmo(STARTING_AMMO);
    }

    private void Update()
    {
        if (UIManager.IsPaused)
            return;

        HandleShooting();
    }
    
    private void ShootMultiplayer()
    {
        object[] objectArray = { GUN_DAMAGE, 
            new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z),
            new Quaternion(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z, firePoint.rotation.w)
        };
        
        photonView.RPC("ShootFromOtherClients", RpcTarget.Others, objectArray as object);
    }
    
    public void AddAmmo(int value)
    {
        UpdateAmmo(ClampMaxAmmo(ammo + value));
    }

    private void UpdateAmmo(int newAmmo)
    {
        ammo = newAmmo;
       
        if ((photonView != null && photonView.IsMine) || GameManager.Instance.IsMultiplayerGame == false)
            AmmoChanged?.Invoke(newAmmo);
    }
    
    private int ClampMaxAmmo(int value)
    {
        return Mathf.Clamp(value, 0, MAX_AMMO);
    }

    [PunRPC]
    private void ShootFromOtherClients(object[] objectArray)
    {
        object[] data = objectArray;
        Debug.Log(data.Length);
        int damage = (int)data[0];
        Vector3 position = (Vector3)data[1];
        Quaternion rotation = (Quaternion)data[2];

        Shoot(damage, position, rotation);
    }
    private void PlaySound(AudioClip audioClip, float volumeScale)
    {
        if (playSound == null)
            return;

        playSound.PlayOneShot(audioClip, volumeScale);
    }

    private void Shoot(int damage, Vector3 position, Quaternion rotation)
    {
        UpdateAmmo(--ammo);
        Bullet bullet = Instantiate(bulletPrefab, position, rotation).GetComponentInChildren<Bullet>();
        if (bullet != null)
            bullet.Fire(damage, firePoint, bulletForce);
        PlaySound(ShootSound, 0.5f);
    }

    private void HandleShooting()
    {
        if (player == null)
            return;

        if (Input.GetButtonDown("Fire1") && ammo > 0)
        {
            if (GameManager.Instance.IsMultiplayerGame)
            {
                if (photonView.IsMine == false)
                {
                    return;
                }
                else
                {
                    ShootMultiplayer();
                }
            }

            Shoot(GUN_DAMAGE, firePoint.position, firePoint.rotation);
        }
    }
}
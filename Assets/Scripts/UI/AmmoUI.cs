using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(101)]
public class AmmoUI : MonoBehaviour
{
    private const string AMMO_PREFIX = "AMMO:";
    
    private Pistol pistol;
    private Text ammoText;

    private void Awake()
    {
        ammoText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        pistol = FindObjectOfType<Pistol>(true);
        if (pistol != null)
            pistol.AmmoChanged += AmmoChanged_Handler;
    }

    private void OnDestroy()
    {
        if (pistol != null)
            pistol.AmmoChanged -= AmmoChanged_Handler;
    }

    private void ChangeAmmoOnUi(int ammo)
    {
        ammoText.text = $"{AMMO_PREFIX} {ammo.ToString()}";
    }

    private void AmmoChanged_Handler(int ammo)
    {
        ChangeAmmoOnUi(ammo);
    }
}
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PickupAmmoBoxTest
{
    private Player player;
    private AmmoBulletBox ammoBulletBox;
    private Pistol pistol;

    [OneTimeSetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Zombies");
    }
    
    // A Test behaves as an ordinary method
    [Test]
    public void HelloWorldSimplePasses()
    {
        // Use the Assert class to test conditions
        string a = "hello world";
        string b = "hello world";
        Assert.IsTrue(string.Equals(a, b));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PickupAmmoBoxTestWithEnumeratorPasses()
    {
        GetPlayer();
        GetAmmoBox();
        GetPistolFromPlayer();
        SetAmmo(5);
        Assert.IsTrue(GetAmmo() == 5);
        yield return null;
        MovePlayerToAmmoBox();
        yield return null;
        Assert.IsTrue(GetAmmo() <= Pistol.MAX_AMMO);
        yield return null;
        int currentBullets = GetAmmo();
        int expectedResult = 5 + AmmoBulletBox.AMMO;
        yield return null;
        yield return null;
        Assert.IsTrue(currentBullets == expectedResult);
        yield return null;
        Assert.IsTrue(ammoBulletBox.IsActive() == false);
    }

    private void GetPistolFromPlayer()
    {
        pistol = player.GetComponentInChildren<Pistol>(true);
        Assert.IsNotNull(pistol);
    }

    private void MovePlayerToAmmoBox()
    {
        Vector3 ammoBoxPosition = ammoBulletBox.gameObject.transform.position;
        Quaternion ammoBoxRotation = ammoBulletBox.gameObject.transform.rotation;
        player.transform.SetPositionAndRotation(ammoBoxPosition, ammoBoxRotation);
        Assert.IsTrue(player.transform.position == ammoBoxPosition);
    }

    private void GetPlayer()
    {
        player = Object.FindObjectOfType<Player>(true);
        Assert.IsNotNull(player);
    }

    private void GetAmmoBox()
    {
        ammoBulletBox = Object.FindObjectsOfType<AmmoBulletBox>(true).FirstOrDefault();
        Assert.IsNotNull(ammoBulletBox);
    }
    
    private void SetAmmo(int newAmmo)
    {
        var prop = pistol.GetType().GetField("ammo", System.Reflection.BindingFlags.NonPublic
                                                     | System.Reflection.BindingFlags.Instance);
        prop.SetValue(pistol, newAmmo);
    }
    
    private int GetAmmo()
    {
        var prop = pistol.GetType().GetField("ammo", System.Reflection.BindingFlags.NonPublic
                                                     | System.Reflection.BindingFlags.Instance);
        return (int)prop.GetValue(pistol);
    }
}

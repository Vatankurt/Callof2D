using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PickupHealthTest
{
    private Player player;
    private HealthItem healthItem;

    [OneTimeSetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("Zombies");
    }
    
    [UnityTest]
    public IEnumerator PickupHealthTestWithEnumeratorPasses()
    {
        // Get the player
        GetPlayer();
        // Get the health
        GetHealth();
        // Set player live to 2
        player.Lives = 2;
        Assert.IsTrue(player.Lives == 2);
        yield return null;
        // Move the player to the health
        MovePlayerToHealth();
        yield return null;
        // Check player health is set to 3
        yield return null;
        Assert.IsTrue(player.Lives == 3);
        // Check player health is not more then MAX_LIVES
        Assert.IsTrue(player.Lives <= Player.MAX_LIVES);
        // Check if the health on the map is disabled
        yield return null;
        Assert.IsTrue(healthItem.IsActive() == false);
    }
    private void GetPlayer()
    {
       player = Object.FindObjectOfType<Player>(true);
       Assert.IsNotNull(player);
    }
    private void GetHealth()
    {
        healthItem = Object.FindObjectOfType<HealthItem>(true);
        Assert.IsNotNull(healthItem);
    }
    private void MovePlayerToHealth()
    { 
        Vector3 healthPosition = healthItem.gameObject.transform.position;
        Quaternion healthRotation = healthItem.gameObject.transform.rotation;
        player.transform.SetPositionAndRotation(healthPosition, healthRotation);
        Assert.IsTrue(player.transform.position == healthPosition);
    }
}
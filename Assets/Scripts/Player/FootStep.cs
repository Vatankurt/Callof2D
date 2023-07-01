using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField]
    private AudioClip walkingSound;
    
    private PlaySound playSound;
    
    private void Awake()
    {
        playSound = GetComponentInChildren<PlaySound>(true);
    }
    
    private void Start()
    {
        playSound.SetupSound(walkingSound, true, 1f);
    }

    public void EnableFootStep(bool isActive)
    {
        playSound.SetActive(isActive);
    }
}

using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(2, 5)]
    private float moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D playerRigidbody;

    private FootStep footStep;
    private Camera playerCamera;
    private Vector2 movement;
    private Vector2 lastMovementPosition;
    private Vector2 mousePosition;
    private Player player;
    private bool isWalking;
    private bool isDoneWalking;

    private void Start()
    {
        playerCamera = Camera.main;
        player = GetComponentInChildren<Player>(true);
        footStep = GetComponentInChildren<FootStep>();
    }

    private void Update()
    {
        if (UIManager.IsPaused || playerCamera == null || player == null)
            return;

        if (GameManager.Instance.IsMultiplayerGame)
        {
            if (player == null || player.PhotonView == null || player.PhotonView.IsMine == false)
                return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            footStep.EnableFootStep(true);
        }
        else
        {
            StopMovement();
        }
    }

    public void StopMovement()
    {
        footStep.EnableFootStep(false);
    }

    private void FixedUpdate()
    {
        if (playerRigidbody == null)
            return;

        playerRigidbody.MovePosition(playerRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);


        Vector2 lookDirection = mousePosition - playerRigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        playerRigidbody.rotation = angle;
    }
}

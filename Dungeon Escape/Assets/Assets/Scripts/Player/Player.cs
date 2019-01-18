using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamagable
{
	//variable for amount of diamond
	public int diamonds;

	//get handle to rigidbody
	private Rigidbody2D _rigid;
	//variable for jumpforce
	[SerializeField]
	private float _jumpForce = 5.0f;
	//variable grounded = false
	private bool _grounded = false;
	[SerializeField]
	private LayerMask _groundLayer;
	private bool _resetJump = false;
	[SerializeField]
	private float _speed = 5.0f;

	//handle to playerAnimation
	private PlayerAnimation _playerAnim;
	private SpriteRenderer _playerSprite;
	private SpriteRenderer _swordArcSprite;

	public int Health { get; set; }

	public AudioClip[] playerSound;

	public Image damageScreen;
	private bool damaged = false;
	Color damageColor = new Color(0f, 0f, 0f, 0.5f);
	private float smoothColor = 1f;

	private LevelManager levelManager;


	// Start is called before the first frame update
	void Start()
    {
		_rigid = GetComponent<Rigidbody2D>();
		//assign handle to playerAnimation
		_playerAnim = GetComponent<PlayerAnimation>();
		_playerSprite = GetComponentInChildren<SpriteRenderer>();
		_swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
		Health = 4;

		damaged = false;

		levelManager = GameObject.FindObjectOfType<LevelManager>();

	}


    // Update is called once per frame
    void Update()
    {
		Movement();

		//if left click & IsGrounded
		if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true)
		{
			//Attack
			_playerAnim.Attack();
			AudioSource.PlayClipAtPoint(playerSound[1], Camera.main.transform.position);
		}

		if (damaged)
		{
			damageScreen.color = damageColor;
			AudioSource.PlayClipAtPoint(playerSound[4], Camera.main.transform.position);
		}
		else
		{
			damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);
		}
		damaged = false;
	}

	/*
	void CheckGrounded()
	{
		//2D raycast to the ground
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.73f, _groundLayer.value);
		//For seeing Raycast
		Debug.DrawRay(transform.position, Vector2.down * 0.73f, Color.green);

		//if hitinfo != null
		if (hitInfo.collider != null)
		{
			Debug.Log("Hit " + hitInfo.collider.name);

			if (resetJumpNeeded == false)
			{
				//grounded = true
				_grounded = true;
			}
		}
	}
	*/

	void Movement()
	{
		//horizontal input for left/right
		float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxisRaw("Horizontal");
		_grounded = IsGrounded();
		Flip(move);



		/*
		 //if space key && grounded == true
		if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
		{
			//jump
			//current velocity = new velocity (current x, jumpforce)
			_rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
			//grounded = false
			_grounded = false;
			//breath
			resetJumpNeeded = true;
			StartCoroutine(ResetJumpNeededRoutine());
		}
		*/

		if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
		{
			//Debug.Log("Jump");
			_rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
			StartCoroutine(ResetJumpNeededRoutine());
			//tell animator to jump
			_playerAnim.Jump(true);
			AudioSource.PlayClipAtPoint(playerSound[0], Camera.main.transform.position);
		}

		//current velocity = new velocity (horizontal input, current velocity.y)
		_rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
		_playerAnim.Move(move);
		
	}

	private void Flip(float move)
	{
		//if move is greater than 0
		if (move > 0)
		{
			//facing right
			_playerSprite.flipX = false;
			_swordArcSprite.flipX = false;
			_swordArcSprite.flipY = false;

			/*
			Vector3 newPos = _swordArcSprite.transform.position;
			newPos.x = 1.01f;
			_swordArcSprite.transform.position = newPos;
			*/
		}
		//else if move < 0
		else if (move < 0)
		{
			//facing left
			_playerSprite.flipX = true;
			_swordArcSprite.flipX = true;
			_swordArcSprite.flipY = true;

			/*
			Vector3 newPos = _swordArcSprite.transform.position;
			newPos.x = -1.01f;
			_swordArcSprite.transform.position = newPos;
			*/
		}
	}

	bool IsGrounded()
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.73f, _groundLayer.value);

		if (hitInfo.collider != null)
		{
			if (_resetJump == false)
			{
				//set animator bool to false
				_playerAnim.Jump(false);
				return true;
			}
		}

		return false;
	}

	
	IEnumerator ResetJumpNeededRoutine()
	{
		_resetJump = true;
		yield return new WaitForSeconds(0.1f);
		_resetJump = false;
	}

	public void Damage()
	{
		if(Health < 1)
		{
			return;
		}
		Debug.Log("Player::Damage()");
		//remove 1 health
		Health--;
		_playerAnim.Hit();
		//update UI Display
		UIManager.Instance.UpdateLives(Health);

		damaged = true;

		//check for dead
		//play death animation
		if (Health < 1)
		{
			_playerAnim.Death();
			AudioSource.PlayClipAtPoint(playerSound[2], Camera.main.transform.position);
			levelManager.LoadLooseLevelAfterDelay();
		}
	}

	public void AddGems(int amount)
	{
		diamonds += amount;
		AudioSource.PlayClipAtPoint(playerSound[3], Camera.main.transform.position);
		UIManager.Instance.UpdateGemCount(diamonds);
	}
	
	
}

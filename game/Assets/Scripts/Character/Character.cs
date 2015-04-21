using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public int lives;

	public GameObject shockwave;
	public GameObject weaponHand;

	public GUIManager GUIVisual;

	public float gracePeriod;

	public float harvestRadius;
	public float pickUpRadius;
	public float speed;
	public float deadZone;
	public int maxWeapons;

	public GameObject[] weapons;
	
	private CharacterController CC;
	private bool canAttack;
	private bool lost;
	private int weaponID;
	private int[] ammunition;
	private bool isAbleToMove;

	Vector3 move;

	Animator animator;

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
		lost = false;
		lives = 99;

		CC = transform.GetComponent<CharacterController>();
		canAttack = true;
		isAbleToMove = true;

		animator = gameObject.GetComponent<Animator>();
		ammunition = new int[maxWeapons];

		//Test
		ammunition[0] = 1;
		ammunition [1] = 5;
		ammunition [2] = 50;

		weaponID = 0;
		GUIVisual.updateWeapon(0);
		GUIVisual.updateAmmo(ammunition[weaponID]);
		GUIVisual.updateLives(lives);
	}

	// Update is called once per frame
	void Update () 
	{
		if (lost == true)
		{
			Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.01f);
		}

		if (!isAbleToMove)
		{
			if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				isAbleToMove = true;
				canAttack = true;
			}
		}

		else if (!canAttack)
		{
			if (!animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
			{
				canAttack = true;
			}
		}

		else
		{
			if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				isAbleToMove = false;
			}
			getMovement();

			transform.LookAt(transform.position + move);
			CC.Move(move * Time.deltaTime * speed);
		}

		pickUps();
		GUIVisual.updatePosition();
		getInput();
	}

	private void getMovement()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if (Mathf.Abs(x) < deadZone)
		{
			x = 0;
		}

		if (Mathf.Abs(y) < deadZone)
		{
			y = 0;
		}

		animator.SetFloat("xVel", Mathf.Abs(x) );
		animator.SetFloat("zVel", Mathf.Abs(y) );

		animator.SetFloat("vel", (Mathf.Abs(y) + Mathf.Abs(x)) / 2);

		move = new Vector3(x, 0, y).normalized;
	}

	private void getInput()
	{
		if (lost == true && Input.anyKeyDown && Time.timeScale < 0.05f)
		{
			Application.LoadLevel(0);
		}

		//Attack detection
		if (Input.GetMouseButtonDown(0) && canAttack && isAbleToMove)
		{
				//If you have that type of ammunition
				if (hasEnoughAmmunition())
				{
					animator.SetTrigger("attack");
					if (weaponID == 0)
					{
						canAttack = false;
					}
				}
		}

		else if (Input.GetMouseButton(0) && canAttack && isAbleToMove && weaponID == 2)
		{
			if (hasEnoughAmmunition())
			{
				animator.SetTrigger("attack");
			}
		}

		//harvest
		if (Input.GetKeyDown(KeyCode.Space) && canAttack)
		{
			animator.SetTrigger("harvest");
			canAttack = false;
		}

		int nw = weaponID;
		int w = weaponID;

		//Weapon selection
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			nw = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			nw = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			nw = 2;
		}

		if (w != nw && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !animator.GetCurrentAnimatorStateInfo(1).IsTag("Attack"))
		{
			weaponID = nw;
			GUIVisual.updateWeapon(weaponID);
			GUIVisual.updateAmmo(ammunition[weaponID]);
			animator.SetInteger("attackType", weaponID);
		}
	}

	private void pickUps()
	{
		GameObject[] fruitLists = GameObject.FindGameObjectsWithTag("fruit");
		foreach (GameObject F in fruitLists)
		{
			if (Vector3.Distance(transform.position, F.transform.position) <= pickUpRadius)
			{

				int a = F.GetComponent<Fruit>().pickedUp();

				if (a != -1)
				{
					if (a == 2)
					{
						ammunition[a] += 10;
					}
					else
					{
						ammunition[a]++;
					}

					GUIVisual.updateAmmo(ammunition[weaponID]);
				}
			}
		}
	}

	private void attack()
	{

		GameObject projectile = (GameObject)Instantiate(weapons[weaponID], weaponHand.transform.position, Quaternion.identity);
		ammunition[weaponID]--;
		GUIVisual.updateAmmo(ammunition[weaponID]);

		switch(weaponID)
		{
			case(0):
				projectile.GetComponent<CoconutBall>().setDirection(transform.forward);
				
				break;

			case(1):
				projectile.GetComponent<BananaBoomerang>().setDirection(transform.forward);
				break;

			case(2):
				projectile.GetComponent<GrapeBullet>().setDirection(transform.forward);
				break;
		}
	}

	public void harvestEffect()
	{
		Camera.main.GetComponent<CameraScript>().shake(harvestRadius);
		GameObject wave = (GameObject)Instantiate(shockwave, transform.position, Quaternion.identity);
		wave.transform.localScale = new Vector3(harvestRadius / 15, 1, harvestRadius / 15);
		wave.GetComponent<Animator>().speed = 10 / harvestRadius;

		harvestFruitInArea(harvestRadius);
	}

	private void harvestFruitInArea(float r)
	{
		GameObject[] plantList = GameObject.FindGameObjectsWithTag("plant");

		foreach (GameObject P in plantList)
		{
			if (Vector3.Distance(transform.position, P.transform.position) <= r)
			{
				Plant plantScript = P.GetComponent<Plant>();
				plantScript.initiateHarvest(transform.position);
			}
		}
	}

	private bool hasEnoughAmmunition()
	{
		if (ammunition[weaponID] > 0)
		{
			return true;
		}

		return false;
	}

	public void hurt(int damage)
	{
		lives -= damage;
		lives = Mathf.Clamp(lives, 0, 1000);
		GUIVisual.updateLives(lives);

		if (lives <= 0)
		{
			lost = true;
			GUIVisual.losing();
		}
	}

	public void win()
	{
		GUIVisual.winning();
	}

	public void test()
	{
		Debug.Log("heyo");
	}
}

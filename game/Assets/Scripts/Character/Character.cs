using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public GameObject shockwave;
	public GameObject weaponHand;

	public GUIManager GUIVisual;

	public float harvestRadius;
	public float pickUpRadius;
	public float speed;
	public float deadZone;
	public int maxWeapons;

	public GameObject[] weapons;
	
	private CharacterController CC;
	private bool canAttack;
	private int weaponID;
	private int[] ammunition;
	private bool isAbleToMove = true;

	Vector3 move;

	Animator animator;

	// Use this for initialization
	void Start () 
	{
		CC = transform.GetComponent<CharacterController>();
		canAttack = true;
		animator = gameObject.GetComponent<Animator>();
		ammunition = new int[maxWeapons];

		//Test
		ammunition[0] = 5;
		weaponID = 0;
		GUIVisual.updateWeapon(0);
		GUIVisual.updateAmmo(ammunition[weaponID]);
	}

	// Update is called once per frame
	void Update () 
	{
		if (!isAbleToMove)
		{
			if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				isAbleToMove = true;
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
		//Attack detection
		if (Input.GetMouseButtonDown(0) && canAttack == true)
		{
			//If you have that type of ammunition
			if (hasEnoughAmmunition())
			{
				animator.SetTrigger("attack");
				canAttack = false;
			}
		}

		//harvest
		if (Input.GetKeyDown(KeyCode.Space) && canAttack == true)
		{
			animator.SetTrigger("harvest");
			canAttack = false;
		}

		int w = weaponID;

		//Weapon selection
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			weaponID = 0;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			weaponID = 1;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			weaponID = 2;
		}

		if (w != weaponID)
		{
			GUIVisual.updateWeapon(weaponID);
			GUIVisual.updateAmmo(ammunition[weaponID]);
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
					ammunition[a]++;
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
				
				break;

			case(2):
				
				break;

			case(3):
				
				break;

			case(4):
				
				break;

			case(5):
				
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

	public void test()
	{
		Debug.Log("heyo");
	}
}

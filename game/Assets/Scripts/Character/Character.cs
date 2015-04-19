using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public GameObject weaponHand;

	public float speed;
	public float deadZone;
	public int maxWeapons;

	public GameObject[] weapons;

	private bool canAttack;
	private int weaponID = 0;
	private int[] ammunition;
	private bool isAbleToMove = true;

	Vector3 move;

	Animator animator;

	// Use this for initialization
	void Start () 
	{
		canAttack = true;
		animator = gameObject.GetComponent<Animator>();
		ammunition = new int[maxWeapons];

		//Test
		ammunition[0] = 5;
	}

	// Update is called once per frame
	void Update () 
	{
		if (!isAbleToMove)
		{
			if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				Debug.Log("free");
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
			transform.Translate(move * Time.deltaTime * speed, Space.World);
		}

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

		//Weapon selection
		float mousewheel = Input.GetAxis("Mouse ScrollWheel");

		if (Mathf.Abs(mousewheel) < deadZone)
		{
			mousewheel = 0;
		}

		if (mousewheel != 0)
		{
			if (mousewheel > 0)
			{
				weaponID++;
			}

			else
			{
				weaponID--;
			}

			weaponID = Mathf.Clamp(weaponID, 0, maxWeapons);
		}
	}

	private void attack()
	{

		GameObject projectile = (GameObject)Instantiate(weapons[weaponID], weaponHand.transform.position, Quaternion.identity);
		ammunition[weaponID]--;

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

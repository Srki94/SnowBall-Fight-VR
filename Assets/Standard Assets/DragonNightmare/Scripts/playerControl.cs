using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour 
{
	public Animator anim;
	int scream;
	int basicAttack;
	int clawAttack;
	int hornAttack;
	int defend;
	int getHit;
	int sleep;
	int walk;
	int run;
	int idle2;
	int walkLeft;
	int walkRight;
	int walkBack;
	int jump;
	int die;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		scream = Animator.StringToHash("Scream");
		basicAttack = Animator.StringToHash("Basic Attack");
		clawAttack = Animator.StringToHash("Claw Attack");
		hornAttack = Animator.StringToHash("Horn Attack");
		defend = Animator.StringToHash("Defend");
		getHit = Animator.StringToHash("Get Hit");
		sleep = Animator.StringToHash("Sleep");
		walk = Animator.StringToHash("Walk");
		run = Animator.StringToHash("Run");
		idle2 = Animator.StringToHash("Idle 2");
		walkLeft = Animator.StringToHash("Walk Left");
		walkRight = Animator.StringToHash("Walk Right");
		walkBack = Animator.StringToHash("Walk Back");
		jump = Animator.StringToHash("Jump");
		die = Animator.StringToHash("Die");
	}


	public void Scream ()
	{
		anim.SetTrigger(scream);
	}

	public void BasicAttack ()
	{
		anim.SetTrigger(basicAttack);
	}

	public void ClawAttack ()
	{
		anim.SetTrigger(clawAttack);
	}

	public void HornAttack ()
	{
		anim.SetTrigger(hornAttack);
	}

	public void Defend ()
	{
		anim.SetTrigger(defend);
	}

	public void GetHit ()
	{
		anim.SetTrigger(getHit);
	}

	public void Sleep ()
	{
		anim.SetTrigger(sleep);
	}

	public void Walk ()
	{
		anim.SetTrigger(walk);
	}

	public void Run ()
	{
		anim.SetTrigger(run);
	}

	public void Idle2 ()
	{
		anim.SetTrigger(idle2);
	}

	public void WalkLeft ()
	{
		anim.SetTrigger(walkLeft);
	}

	public void WalkRight ()
	{
		anim.SetTrigger(walkRight);
	}

	public void WalkBack ()
	{
		anim.SetTrigger(walkBack);
	}

	public void Jump ()
	{
		anim.SetTrigger(jump);
	}

	public void Die ()
	{
		anim.SetTrigger(die);
	}
	
}

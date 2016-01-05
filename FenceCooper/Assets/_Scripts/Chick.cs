using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour 
{
	public int hopDistance;
    public int minJumpDelay;
    public int maxJumpDelay;
    public bool canJump;

    //  Animation
    private Animator animator;
    static int HopTriggerHash = Animator.StringToHash("HopTrigger");

    // Use this for initialization
    void Awake () 
	{
		animator = GetComponentInChildren<Animator>();

	}
	
    void Start()
    {

		StartCoroutine(WaitAndHop());
    }

	// Update is called once per frame
	void Update () 
	{
		if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
		{
	
		}
		else
		{

		}

	}

	public void Hop(Vector3 pos)
	{
		if (canJump)
		{
            StartCoroutine(JumpCooldown());
            Vector3 lookDir = transform.position - pos;
            transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));

            transform.position = transform.position + transform.forward * hopDistance;
			
			animator.SetTrigger(HopTriggerHash);	
		}
	}

	IEnumerator WaitAndHop()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(minJumpDelay, maxJumpDelay));
			transform.eulerAngles = new Vector3(0, Random.Range(0, 360.0f), 0);

            //rigidbody.AddForce(transform.forward * Thrust * Random.Range(1f, 2f), ForceMode.VelocityChange);
            transform.position = transform.position + transform.forward * hopDistance;

            animator.SetTrigger(HopTriggerHash);
		}
	}

    IEnumerator JumpCooldown()
    {
        canJump = false;
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && !animator.IsInTransition(0))
        {
            yield return null;
        }
        canJump = true;
    }
}

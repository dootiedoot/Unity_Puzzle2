using UnityEngine;
using System.Collections;
using PathologicalGames;

public class Chick : MonoBehaviour 
{
    public int CapacityAmount;
	public float hopDistance;
    public float minJumpDelay;
    public float maxJumpDelay;
    public bool canJump;

    bool checkGoal = false;

    //  References
    GameManager _gameManager;
    SphereCollider sphereCol;

    //  Animation
    [HideInInspector] public Animator animator;
    static int isHoppingHash = Animator.StringToHash("isHopping");

    //  Audio
    private AudioSource audioSource;
    public AudioClip ChirpSound;
    public AudioClip JumpSound;

    // Use this for initialization
    void Awake () 
	{
        _gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
        sphereCol = GetComponent<SphereCollider>();
		animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
    void Start()
    {
		StartCoroutine(WaitAndHop());
    }

    public void StartHopAnimation(Vector3 pos)
    {
        Vector3 lookDir = transform.position - pos;

        transform.rotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
        //  Animation
        animator.SetBool(isHoppingHash, true);
        checkGoal = true;
    }

    //  Called from animation
	IEnumerator Hop()
	{
        //  Audio
        if(Chance.Check(.5f))
        {
            audioSource.pitch = Random.Range(.7f, 1f);
            audioSource.PlayOneShot(ChirpSound, .5f);
        }
        audioSource.pitch = Random.Range(.7f, 1.3f);
        audioSource.PlayOneShot(JumpSound, .40f);

        while (animator.GetBool(isHoppingHash))
        {
            transform.Translate(transform.forward * GetFarthestHop() * Time.deltaTime, Space.World);
            if (checkGoal)
                CheckForGoal();
            yield return null;
        }
        if(checkGoal)
            checkGoal = false;
    }

    void CheckForGoal()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If the ray has hit something that is within our collision paramters
        if (Physics.Raycast(ray, out hit, Time.deltaTime + .1f, Layers.Buildings))
        {
            Building _building = hit.transform.GetComponent<Building>();
            if(_building.CanStore(CapacityAmount, Building.AnimalType.Chick))
            {
                _building.AdjustCount(CapacityAmount);
                //  Game Manager
                GameManager.chickCurrentAmt++;
                _gameManager.DecrementAmount();

                PoolManager.Pools["Units"].Despawn(this.transform);
            }
        }
    }
    float GetFarthestHop()
    {
        RaycastHit hit;

        // If the ray has hit something that is within our collision paramters
        if (Physics.Raycast(transform.position, transform.forward, out hit, hopDistance, Layers.Obstacles))
        {
            //Debug.Log(Vector3.Distance(transform.position, hit.point));
            if (Vector3.Distance(transform.position, hit.point) < .5f)
                return 0;
            return Vector3.Distance(transform.position, hit.point);
        }
        return hopDistance;
    }



	IEnumerator WaitAndHop()
	{
		while(true)
		{
			yield return new WaitForSeconds(Random.Range(minJumpDelay, maxJumpDelay));

            transform.eulerAngles = new Vector3(0, Random.Range(0, 360.0f), 0);
            //  Animation
            animator.SetBool(isHoppingHash, true);
        }
	}
}

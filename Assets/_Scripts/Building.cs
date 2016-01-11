using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Building : MonoBehaviour
{
    public AnimalType AcceptedType;
    public enum AnimalType
    {
        Chick,
        Chicken
    }

    public int CurrentCapacity;         // UNPUBLIC LATER
    public int MaxCapacity;

    //  UI
    public Text CapacityText;

    //  Animation
    [HideInInspector]
    private Animator animator;
    static int PopTriggerHash = Animator.StringToHash("PopTrigger");

    //  Audio
    private AudioSource audioSource;
    public AudioClip PopSound;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        UpdateUI();
	}

    public void AdjustCount(int amount)
    {
        if (CurrentCapacity + amount <= MaxCapacity)
        {
            CurrentCapacity += amount;
            
            //  Animation
            animator.SetTrigger(PopTriggerHash);
            //  Audio
            audioSource.pitch += .05f;
            audioSource.PlayOneShot(PopSound, .5f);
            //  UI
            UpdateUI();
        }
    }
    
    public bool CanStore(int amount, AnimalType type)
    {
        if (type == AcceptedType && CurrentCapacity + amount <= MaxCapacity)
            return true;
        return false;
    }

    void UpdateUI()
    {
        CapacityText.text = CurrentCapacity + "/" + MaxCapacity;
    }
}

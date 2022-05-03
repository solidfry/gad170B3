using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerNumber { One = 1, Two = 2, Three = 3, Four = 4 } // the number for our players
/// <summary>
/// The main class of our tank
/// Everything should be run from here.
/// </summary>
public class Tank : MonoBehaviour
{
    public bool enableTankMovement = false;
    public PlayerNumber playerNumber; // the number of our players tank
    public TankControls tankControls = new TankControls(); // creating a new instance of our tank controls
    public TankHealth tankHealth = new TankHealth(); // creating a new instance of our tank health data class.
    public TankMovement tankMovement = new TankMovement(); // creating a new instance of our tank movement script
    public TankMainGun tankMainGun = new TankMainGun(); // creating a new instance of our tank main gun script
    public GameObject explosionPrefab; // the prefab we will use when we have 0 left to make it go boom!
    TankSpawnManager tankSpawnManager;
    private void OnEnable()
    {
        TankGameEvents.OnPreGameEvent += DisableInput;
        TankGameEvents.OnPickUpBoostEvent += tankMovement.ModifySpeed;
        TankGameEvents.OnPickUpBoostResetEvent += ResetBoost;
        TankGameEvents.OnObjectDestroyedEvent += Dead; // add dead function to the event for when a tank is destroyed
        TankGameEvents.OnObjectTakeDamageEvent += TankTakenDamage; // assign our health function to our event so we can take damage
        TankGameEvents.OnGameStartedEvent += EnableInput; // assign our tank movement function to the game started event
        TankGameEvents.OnResetTankEvent += tankHealth.ResetTankHealth;
        TankGameEvents.OnTanksRepositionedEvent += EnableInput;
    }

    private void OnDisable()
    {
        TankGameEvents.OnPreGameEvent -= DisableInput;
        TankGameEvents.OnPickUpBoostEvent -= tankMovement.ModifySpeed;
        TankGameEvents.OnPickUpBoostResetEvent -= ResetBoost; // add dead function to the event for when a tank is destroyed
        TankGameEvents.OnObjectDestroyedEvent -= Dead; // add dead function to the event for when a tank is destroyed
        TankGameEvents.OnObjectTakeDamageEvent -= TankTakenDamage; // assign our health function to our event so we can take damage
        TankGameEvents.OnGameStartedEvent -= EnableInput; // assign our tank movement function to the game started event
        TankGameEvents.OnResetTankEvent -= tankHealth.ResetTankHealth;
        TankGameEvents.OnTanksRepositionedEvent -= EnableInput;
    }

    // Start is called before the first frame update
    void Start()
    {
        tankHealth.SetUp(transform); // call the set up function of our tank health script
        tankMovement.SetUp(transform); // calls the set up function of our tank health script
        tankMainGun.SetUp(); // calls the set up function of our tank main gun script

        if (enableTankMovement)
        {
            EnableInput();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // passes in the values from our key input, to our motor to make it move
        tankMovement.HandleMovement(tankControls.ReturnKeyValue(TankControls.KeyType.Movement), tankControls.ReturnKeyValue(TankControls.KeyType.Rotation));
        tankMainGun.UpdateMainGun(tankControls.ReturnKeyValue(TankControls.KeyType.Fire)); // grab the input from the fire key
    }

    /// <summary>
    /// Enables our tank to recieve input
    /// </summary>
    private void EnableInput()
    {
        Debug.Log("Enable input was run");
        tankMovement.EnableTankMovement(true);
        tankMainGun.EnableShooting(true);
    }

    private void DisableInput()
    {
        enableTankMovement = false;
        tankMovement.EnableTankMovement(false);
        tankMainGun.EnableShooting(false);
    }

    private void EnableTankMovement()
    {
        tankMovement.EnableTankMovement(true);
    }

    /// <summary>
    /// Called when an objects takes damage, if the object taking damage is this tank
    /// deal damage to it, else ignore it.
    /// </summary>
    /// <param name="TankTransform"></param>
    /// <param name="AmountOfDamage"></param>
    private void TankTakenDamage(Transform TankTransform, float AmountOfDamage)
    {
        Debug.Log("Damage Taken");
        // if the Tank transform coming in, isn't this particular tank, ignore it.
        if (TankTransform != transform)
        {
            Debug.Log("Not the right transform");
            return;
        }
        else
        {
            Debug.Log("Damage applied?" + AmountOfDamage);
            tankHealth.ApplyHealthChange(AmountOfDamage);
        }
    }

    /// <summary>
    /// Called when the object destroyed event has been called
    /// </summary>
    /// <param name="ObjectDestroyed"></param>
    private void Dead(Transform ObjectDestroyed)
    {
        if (ObjectDestroyed != transform)
        {
            return;
        }

        GameObject clone = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation); // spawn in our explosion effect
        Destroy(clone, 2); // just cleaning up our particle effect
        // gameObject.SetActive(false); // turn off our tank as we are dead
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IModifyStats>() != null)
        {
            float boostValue = other.gameObject.GetComponent<ModifyStat>().boostValue;
            float originalValue = tankMovement.speed;
            Boost(originalValue, boostValue);
            Debug.Log($"Your new speed is {tankMovement.speed}");
        }
    }

    void Boost(float valueToBoost, float amountToBoost)
    {
        TankGameEvents.OnPickUpBoostEvent?.Invoke(valueToBoost, amountToBoost);
        TankGameEvents.OnPickUpBoostResetEvent.Invoke(valueToBoost);
    }

    void ResetBoost(float valueToReset)
    {
        StartCoroutine(tankMovement.ResetSpeed(valueToReset));
    }
}

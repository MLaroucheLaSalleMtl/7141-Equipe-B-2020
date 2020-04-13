using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponant : MonoBehaviour
{
    #region Variables & Attributs
    [HideInInspector] public Actor caster;
    [Header("Projectile Properties")]
    [SerializeField] private float range = 10f;
    [SerializeField] private float velocity = 15f;
    [SerializeField] private int piercingNumber = 0;
    [SerializeField] private float spreadRatio = 0;

    [Header("Homing Properties")]
    [SerializeField] private bool isHoming = false;
    [SerializeField] public Transform target = null;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;

    private bool isDirty = false;
    private Vector3 projectile_Direction;
    private Vector3 origin;
    Rigidbody rig;

    public Vector3 Projectile_Direction { get => projectile_Direction; set => projectile_Direction = value; }
    #endregion

    #region Unity's Methods

    private void Start()
    {
        HomingProjectile();
        origin = transform.position;
        projectile_Direction = new Vector3(0 + Random.Range(-spreadRatio, spreadRatio), 0, 1);
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Vector3.Distance(origin, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (isHoming)
        {
           // rig.velocity = transform.forward * velocity;
            transform.Translate(projectile_Direction * Time.deltaTime * velocity/2);
            gameObject.transform.LookAt(target);
        }
        else
        {
           // rig.velocity = transform.forward * velocity;
            transform.Translate(projectile_Direction * Time.deltaTime * velocity);

        }
    }

    #endregion

    #region Methods
    private void HomingProjectile()
    {
        if (isHoming && !isDirty)
        {
            
            Collider[] colliders = Physics.OverlapSphere(origin, range*2);
            RandomizeArray(colliders);
            foreach (Collider collider in colliders)
            {
                if(collider.tag == typeOfTarget.ToString())
                {
                    target = collider.gameObject.transform;
                    if (target != null)
                    {
                        isDirty = true;
                        return;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
            return;
        }
        else if (other.gameObject.tag == typeOfTarget.ToString())
        {
            if (isHoming)
            {

                Collider[] colliders = Physics.OverlapSphere(origin, range);
                RandomizeArray(colliders);


                foreach (Collider collider in colliders)
                {
                    if (collider.tag == typeOfTarget.ToString())
                    {
                        target = collider.gameObject.transform;
                    }
                }
            }

            piercingNumber--;

            if (piercingNumber <= 0 )
            {
                Destroy(gameObject);
            }
        }
    }

    void RandomizeArray(Collider[] Array)
    {
        for (int i = 0; i < Array.Length; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            Collider rNB = Array[i];
            int ranNumber = Random.Range(i, Array.Length);
            Array[i] = Array[ranNumber];
            Array[ranNumber] = rNB;
        }
    }
    #endregion
}

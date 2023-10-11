using System;
using UnityEngine;

public class ElfController : MonoBehaviour
{
    [HideInInspector] public bool HandlingCollision;
    [HideInInspector] public string InspectorElfName;
    
    [SerializeField] ElfModel model;
    public ElfModel Model => model;
    
    public event Action<GameObject, Vector3, ElfCollisionType, bool> OnCollision;

    ElfState state;
    Rigidbody rigidBody;
    MeshRenderer meshRenderer;
    Collider selfCollider;
    Vector3 constantVelocity;

    // Start is called before the first frame update
    void OnEnable()
    {
        state = ElfState.Spawning;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        selfCollider = GetComponent<Collider>();
        meshRenderer.material = model.InactiveMaterial;
        HandlingCollision = false;
        rigidBody.isKinematic = true;
        selfCollider.enabled = false;
        gameObject.name = InspectorElfName;
        Invoke(nameof(InitElf), model.SpawningTime);
    }

    void InitElf()
    {
        state = ElfState.Active;
        meshRenderer.material = model.ActiveMaterial;
        rigidBody.isKinematic = false;
        selfCollider.enabled = true;
        SetInitialVelocity();
    }

    void LateUpdate()
    {
        if (!rigidBody.isKinematic)
        {
            rigidBody.velocity = constantVelocity;
        }
    }

    void SetInitialVelocity()
    {
        Vector3 initialDirection = new Vector3
        {
            x = UnityEngine.Random.Range(-0.9f, 0.9f),
            y = 0f,
            z = 1f
        };

        rigidBody.velocity = transform.TransformDirection(initialDirection * model.Speed);
        constantVelocity = rigidBody.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == ElfState.Active)
        {
            if (collision.gameObject.CompareTag("Elf"))
            {
                ElfController otherElfController = collision.gameObject.GetComponent<ElfController>();

                otherElfController.HandlingCollision = true;

                ElfCollisionType collisionType;

                if (model.Color == otherElfController.Model.Color)
                {
                    collisionType = ElfCollisionType.SameColor;
                } else
                {
                    collisionType = ElfCollisionType.DifferentColor;
                }

                var colliderPosition = collision.gameObject.transform.position;

                OnCollision(this.gameObject, colliderPosition, collisionType, HandlingCollision);
            }
        }

        constantVelocity = rigidBody.velocity;

        HandlingCollision = false;
    }

    void OnDisable()
    {
        CancelInvoke(nameof(InitElf));    
    }
}

public enum ElfCollisionType
{
    SameColor,
    DifferentColor
}

public enum ElfState
{
    Inactive,
    Spawning,
    Active
}

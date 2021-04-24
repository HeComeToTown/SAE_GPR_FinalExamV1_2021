using System.Collections;
using UnityEngine;

public interface IPlayerAction
{
    bool IsInAction();
}

public class SpellCastingController : MonoBehaviour, IPlayerAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform castLocationTransform;
    [SerializeField] private ProjectileSpellDescription simpleAttackSpell;
    
    private ProjectileSpellDescription specialAttackSpell;

    private bool inAction;
    private bool castingSimpleAttack;
    private bool castingSpecialAttack;
    private float lastSimpleAttackTimestamp = -100;
    private float lastSpecialAttackTimestamp = -100;

    public SpellDescription SimpleAttackSpellDescription { get => simpleAttackSpell; }

    private void Start()
    {
        Debug.Assert(simpleAttackSpell, "No spell assigned to SpellCastingController.");
    }

    void Update()
    {
        bool simpleAttack = Input.GetButtonDown("Fire1");
        bool specialAttack = Input.GetButtonDown("Fire2");

        if (!inAction)
        {
            if (simpleAttack && GetSimpleAttackCooldown() == 0)
            {
                castingSimpleAttack = true;
                StartCoroutine(SimpleAttackRoutine());

            }
            else if (specialAttack && GetSpecialAttackCooldown() == 0 && specialAttackSpell != null)
            {
                castingSpecialAttack = true;
                StartCoroutine(SpecialAttackRoutine());
            }
        }
    }

    private IEnumerator SimpleAttackRoutine()
    {
        inAction = true;
        animator.SetTrigger(simpleAttackSpell.AnimationVariableName);

        yield return new WaitForSeconds(simpleAttackSpell.ProjectileSpawnDelay);

        Instantiate(simpleAttackSpell.ProjectilePrefab, castLocationTransform.position, castLocationTransform.rotation);

        yield return new WaitForSeconds(simpleAttackSpell.Duration - simpleAttackSpell.ProjectileSpawnDelay);

        lastSimpleAttackTimestamp = Time.time;
        castingSimpleAttack = false;
        inAction = false;
    }

    private IEnumerator SpecialAttackRoutine()
    {
        inAction = true;
        animator.SetTrigger(specialAttackSpell.AnimationVariableName);

        yield return new WaitForSeconds(specialAttackSpell.ProjectileSpawnDelay);

        Instantiate(specialAttackSpell.ProjectilePrefab, castLocationTransform.position, castLocationTransform.rotation);

        yield return new WaitForSeconds(specialAttackSpell.Duration - specialAttackSpell.ProjectileSpawnDelay);

        lastSpecialAttackTimestamp = Time.time;
        castingSpecialAttack = false;
        inAction = false;
    }

    public bool IsInAction()
    {
        return inAction;
    }

    public float GetSimpleAttackCooldown()
    {
        return Mathf.Max(0, lastSimpleAttackTimestamp + simpleAttackSpell.Cooldown - Time.time);
    }

    public float GetSpecialAttackCooldown()
    {
        return Mathf.Max(0, lastSpecialAttackTimestamp + specialAttackSpell.Cooldown - Time.time);
    }

    public bool GetCastingSimpleAttack()
    {
        return castingSimpleAttack;
    }

    public bool GetCastingSpecialAttack()
    {
        return castingSpecialAttack;
    }

    public void SetSpecialAttack(ProjectileSpellDescription specialSpellToSet)
    {
        specialAttackSpell = specialSpellToSet;
    }
}

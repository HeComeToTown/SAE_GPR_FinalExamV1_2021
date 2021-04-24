using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private SpellCastingController spellCastingController;
    [SerializeField] private DropCollector dropCollector;

    [SerializeField] private RectTransform simpleSpellIconRectTransform;
    [SerializeField] private Image simpleSpellIcon;
    [SerializeField] private Outline simpleSpellIconOutline;
    [SerializeField] private TMPro.TMP_Text simpleSpellCooldownText;
    [SerializeField] private GameObject collectUIObject;

    [SerializeField] private RectTransform specialSpellIconRectTransform;
    [SerializeField] private Image specialSpellIcon;
    [SerializeField] private Outline specialSpellIconOutline;
    [SerializeField] private TMPro.TMP_Text specialSpellCooldownText;

    [SerializeField] private float simpleMaxSpellIconSize;
    [SerializeField] private float simpleSpellIconSizeFlatIncreasePerSecond;
    [SerializeField] private float simpleSpellIconSizeFlatDecreasePerSecond;

    [SerializeField] private float specialMaxSpellIconSize;
    [SerializeField] private float specialSpellIconSizeFlatIncreasePerSecond;
    [SerializeField] private float specialSpellIconSizeFlatDecreasePerSecond;

    float simpleCurrentSpellIconSize = 1f;
    float specialCurrentSpellIconSize = 1f;
    
    bool hasSpecialAttack = false;

    private void Start()
    {
        Debug.Assert(spellCastingController != null, "SpellCastingController reference is null");
        Debug.Assert(dropCollector != null, "DropCollector reference is null");

        simpleSpellIcon.sprite = spellCastingController.SimpleAttackSpellDescription.SpellIcon;
        if (simpleSpellIconOutline.IsActive()) { simpleSpellIconOutline.enabled = false; }
        if (specialSpellIconOutline.IsActive()) { specialSpellIconOutline.enabled = false; }

        specialSpellCooldownText.text = "";

        dropCollector.DropsInRangeChanged += OnDropsInRangeChanged;
    }

    private void OnDropsInRangeChanged()
    {
        collectUIObject.SetActive(dropCollector.DropsInRangeCount > 0);
    }

    private void Update()
    {
        float simpleCooldown = spellCastingController.GetSimpleAttackCooldown();
        bool castingSimple = spellCastingController.GetCastingSimpleAttack();

        if (simpleCooldown > 0)
        {
            simpleSpellCooldownText.text = simpleCooldown.ToString("0.0");
            simpleSpellIcon.color = new Color(0.25f, 0.25f, 0.25f, 1);

            if (simpleCurrentSpellIconSize > 1f && !castingSimple)
            {
                simpleCurrentSpellIconSize = Mathf.Max(simpleCurrentSpellIconSize - simpleSpellIconSizeFlatDecreasePerSecond * Time.deltaTime, 1f);
                simpleSpellIconRectTransform.localScale = new Vector3(simpleCurrentSpellIconSize, simpleCurrentSpellIconSize);
            }
        }
        else
        {
            simpleSpellCooldownText.text = "";
            simpleSpellIcon.color = Color.white;

            if (!castingSimple)
            {
                simpleSpellIconRectTransform.localScale = new Vector3(1f, 1f);
                simpleCurrentSpellIconSize = 1f;
            }
        }

        if (castingSimple)
        {
            simpleSpellIconOutline.enabled = true;

            if (simpleCurrentSpellIconSize < simpleMaxSpellIconSize)
            {
                simpleCurrentSpellIconSize = Mathf.Min(simpleCurrentSpellIconSize + simpleSpellIconSizeFlatIncreasePerSecond * Time.deltaTime, simpleMaxSpellIconSize);
                simpleSpellIconRectTransform.localScale = new Vector3(simpleCurrentSpellIconSize, simpleCurrentSpellIconSize);
            }
        }
        else
        {
            simpleSpellIconOutline.enabled = false;
        }

        if (hasSpecialAttack)
        {
            float specialCooldown = spellCastingController.GetSpecialAttackCooldown();
            bool castingSpecial = spellCastingController.GetCastingSpecialAttack();

            if (specialCooldown > 0)
            {
                specialSpellCooldownText.text = specialCooldown.ToString("0.0");
                specialSpellIcon.color = new Color(0.25f, 0.25f, 0.25f, 1);

                if (specialCurrentSpellIconSize > 1f && !castingSpecial)
                {
                    specialCurrentSpellIconSize = Mathf.Max(specialCurrentSpellIconSize - specialSpellIconSizeFlatDecreasePerSecond * Time.deltaTime, 1f);
                    specialSpellIconRectTransform.localScale = new Vector3(specialCurrentSpellIconSize, specialCurrentSpellIconSize);
                }
            }
            else
            {
                specialSpellCooldownText.text = "";
                specialSpellIcon.color = Color.white;

                if (!castingSpecial)
                {
                    specialSpellIconRectTransform.localScale = new Vector3(1f, 1f);
                    specialCurrentSpellIconSize = 1f;
                }
            }

            if (castingSpecial)
            {
                specialSpellIconOutline.enabled = true;

                if (specialCurrentSpellIconSize < specialMaxSpellIconSize)
                {
                    specialCurrentSpellIconSize = Mathf.Min(specialCurrentSpellIconSize + specialSpellIconSizeFlatIncreasePerSecond * Time.deltaTime, specialMaxSpellIconSize);
                    specialSpellIconRectTransform.localScale = new Vector3(specialCurrentSpellIconSize, specialCurrentSpellIconSize);
                }
            }
            else
            {
                specialSpellIconOutline.enabled = false;
            }
        }
    }

    public void SetSpecialSpellIcon(Sprite image)
    {
        specialSpellIcon.sprite = image;
        hasSpecialAttack = true;
    }
}

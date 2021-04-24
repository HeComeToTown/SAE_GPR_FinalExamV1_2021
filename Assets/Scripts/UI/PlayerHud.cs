using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private SpellCastingController spellCastingController;
    [SerializeField] private DropCollector dropCollector;

    [SerializeField] private RectTransform spellIconRectTransform;
    [SerializeField] private Image spellIcon;
    [SerializeField] private Outline spellIconOutline;
    [SerializeField] private TMPro.TMP_Text spellCooldownText;
    [SerializeField] private GameObject collectUIObject;

    [SerializeField] private float maxSpellIconSize;
    [SerializeField] private float spellIconSizeFlatIncreasePerSecond;
    [SerializeField] private float spellIconSizeFlatDecreasePerSecond;

    float currentSpellIconSize = 1f;

    private void Start()
    {
        Debug.Assert(spellCastingController != null, "SpellCastingController reference is null");
        Debug.Assert(dropCollector != null, "DropCollector reference is null");

        spellIcon.sprite = spellCastingController.SimpleAttackSpellDescription.SpellIcon;
        if (spellIconOutline.IsActive()) { spellIconOutline.enabled = false; }

        dropCollector.DropsInRangeChanged += OnDropsInRangeChanged;
    }

    private void OnDropsInRangeChanged()
    {
        collectUIObject.SetActive(dropCollector.DropsInRangeCount > 0);
    }

    private void Update()
    {
        float cooldown = spellCastingController.GetSimpleAttackCooldown();
        bool casting = spellCastingController.GetCasting();

        if (cooldown > 0)
        {
            spellCooldownText.text = cooldown.ToString("0.0");
            spellIcon.color = new Color(0.25f, 0.25f, 0.25f, 1);

            if (currentSpellIconSize > 1f && !casting)
            {
                currentSpellIconSize = Mathf.Max(currentSpellIconSize - spellIconSizeFlatDecreasePerSecond * Time.deltaTime, 1f);
                spellIconRectTransform.localScale = new Vector3(currentSpellIconSize, currentSpellIconSize);
            }
        }
        else
        {
            spellCooldownText.text = "";
            spellIcon.color = Color.white;

            //if (!casting)
            //{
            //    spellIconRectTransform.localScale = new Vector3(1f, 1f);
            //    currentSpellIconSize = 1f;
            //}
        }

        if (casting)
        {
            spellIconOutline.enabled = true;

            if (currentSpellIconSize < maxSpellIconSize)
            {
                currentSpellIconSize = Mathf.Min(currentSpellIconSize + spellIconSizeFlatIncreasePerSecond * Time.deltaTime, maxSpellIconSize);
                spellIconRectTransform.localScale = new Vector3(currentSpellIconSize, currentSpellIconSize);
            }
        }
        else
        {
            spellIconOutline.enabled = false;
        }
    }
}

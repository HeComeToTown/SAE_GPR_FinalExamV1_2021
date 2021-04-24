using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackAsDrop : RuntimeDropInstance
{
    [SerializeField] private ProjectileSpellDescription spellDescription;
    private PlayerHud playerHud;

    private void Awake()
    {
        playerHud = GameObject.FindGameObjectWithTag("PlayerHud").GetComponent<PlayerHud>();
    }

    public override void OnPickUp(GameObject player)
    {
        base.OnPickUp(player);

        playerHud.SetSpecialSpellIcon(spellDescription.SpellIcon);
        player.GetComponent<SpellCastingController>().SetSpecialAttack(spellDescription);
    }
}

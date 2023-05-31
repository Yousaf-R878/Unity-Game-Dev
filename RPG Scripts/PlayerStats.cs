using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {   
        if(newItem!= null)
        {
            Armor.AddModefier(newItem.armorModifier);
            Damage.AddModefier(newItem.damageModifier);
        }

        if(oldItem != null)
        {
            Armor.RemoveModifier(oldItem.armorModifier);
            Damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}

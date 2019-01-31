using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {


    public event System.Action Winner;
    public static bool IsWinner = false;

    // Use this for initialization
    void Start () {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
        
    }
	
	// Update is called once per frame
	void onEquipmentChanged ( Equipment newItem, Equipment oldItem){

        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        
        if(oldItem != null)
        {
            armor.RemoveMmodifier(oldItem.armorModifier);
            damage.RemoveMmodifier(oldItem.damageModifier);
        }   
        
	}
    

  
    [ContextMenu("To win")]
    public void Win()
    {
        IsWinner = true;
        if (Winner != null)
        {                 
            AudioManager.instance.PlaySound2D("win");
            Winner();
        }
    }





}

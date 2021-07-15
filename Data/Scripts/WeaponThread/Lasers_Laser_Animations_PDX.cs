using System.Collections.Generic;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.PartAnimationSetDef.EventTriggers;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.RelMove.MoveType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.RelMove;

namespace WeaponThread
{ // Don't edit above this line
    partial class Weapons
    {
        private AnimationDef PDX_Animations => new AnimationDef
        {
            HeatingEmissiveParts = new[]
            {
                "MissileTurretBarrels",
            },
        };
    }
}

using RoR2;
using RoR2.Skills;
using RoR2.Navigation;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using BepInEx;
using RoR2.ContentManagement;
using System.Runtime.CompilerServices;
using RoR2.ExpansionManagement;
using static UnityEngine.Rendering.DebugUI;
using static RoR2.Console;
using RoR2.Achievements.Artifacts;
using static RoR2.SkinDef;
using static RoR2.CharacterModel;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace DumpAll
{
    public static class Dump_Equipment
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
            List<EquipmentDef> equipmentDefs = ContentManager.equipmentDefs.ToList();
            equipmentDefs.Sort((x, y) => string.Compare(x.name, y.name));

            log = "\nEquipmentDefs\n\n";
            foreach (EquipmentDef def in equipmentDefs)
            {
                log += def.name + "  (" + Language.GetString(def.nameToken) + ")\n";
                log += def.cooldown + "\n";
                log += "-canDrop? " + def.canDrop + "\n";
                log += "\n";
            }
             DumpAll.logger.LogMessage(log);
        }



    }




}
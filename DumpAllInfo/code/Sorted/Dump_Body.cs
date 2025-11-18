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
    public static class Dump_Body
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
 

            log = "\n\n---CharacterBody BodyFlags---\n\n";
            foreach (CharacterBody body in BodyCatalog.allBodyPrefabBodyBodyComponents)
            {
                /* if (body.bodyFlags.HasFlag((CharacterBody.BodyFlags)65536))
                {
                    body.bodyFlags &= ~(CharacterBody.BodyFlags)65536;
                }*/
                if (body.bodyFlags.HasFlag((CharacterBody.BodyFlags)131072))
                {
                    body.bodyFlags &= ~(CharacterBody.BodyFlags)131072;
                }
                if (body.bodyFlags.HasFlag((CharacterBody.BodyFlags)262144))
                {
                    body.bodyFlags &= ~(CharacterBody.BodyFlags)262144;
                }
                if (body.bodyFlags.HasFlag((CharacterBody.BodyFlags)1048576))
                {
                    body.bodyFlags &= ~(CharacterBody.BodyFlags)1048576;
                }

                log += body.gameObject.name + " (" + Language.GetString(body.baseNameToken) + ")\n";
                log += "["+body.bodyFlags.ToString()+"]\n";
                log += string.Format("-health: {0} (+{1})\n", body.baseMaxHealth, body.levelMaxHealth);
                if (body.baseRegen > 0)
                {
                    log += string.Format("-regen: {0} (+{1})\n", body.baseRegen, body.levelRegen);
                }
                if (body.baseDamage > 0)
                {
                    log += string.Format("-damage: {0} (+{1})\n", body.baseDamage, body.levelDamage);
                }
                if (body.baseAttackSpeed != 1)
                {
                    log += string.Format("-attackSpeed: {0}\n", body.baseAttackSpeed);
                }
                if (body.baseMoveSpeed > 0)
                {
                    log += string.Format("-moveSpeed: {0}\n", body.baseMoveSpeed);
                }
                if (body.baseArmor != 0)
                {
                    log += string.Format("-armor: {0}\n", body.baseArmor);
                }

                log += "\n\n";
            }
            DumpAll.logger.LogMessage(log);







            log = "\n\n---EntityStateConfigurations---\n\n";
            foreach (EntityStateConfiguration config in ContentManager.entityStateConfigurations)
            {
                log += "\n-" + config.name + "-";
                log += "\n";
                foreach (var fields in config.serializedFieldsCollection.serializedFields)
                {
                    log += fields.fieldName + " | ";
                    if (fields.fieldValue.objectValue)
                    {
                        log += fields.fieldValue.objectValue;
                    }
                    else
                    {
                        log += fields.fieldValue.stringValue;
                    }
                    log += "\n";
                }
            }
            DumpAll.logger.LogMessage(log);
        }
        


    }




}
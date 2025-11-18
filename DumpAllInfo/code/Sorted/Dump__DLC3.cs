using RoR2;
using RoR2.Projectile;
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
    public static class Dump__DLC3
    {
        public static void DumpInfo()
        {
     
            string log = string.Empty;
            SpecialObjectAttributes[] AllObjectDamages = Resources.FindObjectsOfTypeAll<SpecialObjectAttributes>();
            List<SpecialObjectAttributes> objectAttributes = AllObjectDamages.ToList();
            objectAttributes.Sort((x, y) => string.Compare(x.name, y.name));

            log = "\n\n--SpecialObjectAttributes--";
            log += "\n---------------------------\n";
            /*string logNoGrab = "\n\n---Ungrabbable---\n\n";
            foreach (CharacterBody body in BodyCatalog.allBodyPrefabBodyBodyComponents)
            {
                if (!body.GetComponent<SpecialObjectAttributes>() && body.bodyFlags.HasFlag(CharacterBody.BodyFlags.Ungrabbable))
                {
                    logNoGrab += "--" + body.name + "--\n";
                    logNoGrab += "Un-grabbable\n\n";
                }
            }*/

            /*string logGrabbableBodies = string.Empty;
            foreach (CharacterBody body in BodyCatalog.allBodyPrefabBodyBodyComponents)
            {
                if (!body.bodyFlags.HasFlag(CharacterBody.BodyFlags.Ungrabbable) || (body.TryGetComponent<SpecialObjectAttributes>(out var obj) && obj.grabbable))
                {
                    logGrabbableBodies += "--" + body.name + "--\n";
                    logGrabbableBodies += "Grabbable\n\n";
                }
            }
            Debug.Log(logGrabbableBodies);*/
            foreach (var obj in objectAttributes)
            {
                log += "--" + obj.name + "--\n";
                float mass = 0;
                if (obj.TryGetComponent<IPhysMotor>(out var physMotor))
                {
                    mass = physMotor.mass;
                }
                else
                {
                    mass = obj.massOverride;
                }



                //Default damage is 4
                log += "Mass: " + mass + "\n";
                log += "DmgVal: " + obj.damageOverride + "\n";

                float baggedMass = Mathf.Clamp(mass, 0f, DrifterBagController.maxMass);
                float value = Mathf.Clamp(baggedMass, 1f, DrifterBagController.maxMass);
                float t = Mathf.InverseLerp(1f, DrifterBagController.maxMass, value);
                float movespeedPenalty = Mathf.Lerp(0f, 0.4f, t);
                float walkSpeedPenalty = Mathf.Max(1 - movespeedPenalty, 0.1f);
                log += "MoveSpeedMult: " + walkSpeedPenalty + "\n";
                log += "ThrowDamage: " + 100 * (0.01f * baggedMass + (obj.damageOverride >= 0 ? obj.damageOverride : 3)) + "%\n";
                //log += "DmgType : " + obj.damageTypeOverride.damageType +" "+obj.damageTypeOverride.damageTypeExtended + "\n";
                log += "DmgType: ";
                if (obj.damageTypeOverride.damageType == DamageType.Generic && obj.damageTypeOverride.damageTypeExtended == DamageTypeExtended.Generic)
                {
                    log += DamageType.Generic;
                }
                else if (obj.damageTypeOverride.damageType != DamageType.Generic)
                {
                    log += obj.damageTypeOverride.damageType;
                }
                else if (obj.damageTypeOverride.damageTypeExtended != DamageTypeExtended.Generic)
                {
                    log += obj.damageTypeOverride.damageTypeExtended;
                }
                log += "\n";
                log += "\n";
            }
            //log += logNoGrab;
            DumpAll.logger.LogMessage(log);


 

            log = "\n\n\nAll Crafting Recipes\n";
            var CRAFTING = CraftableCatalog.GetAllRecipes();
            for (int i = 0; CRAFTING.Length > i; i++)
            {

                string Recipe = string.Empty;
                var ingredients = CRAFTING[i].possibleIngredients[0].pickups;
                Recipe += "\nSlot1 : ";
                for (int C1 = 0; ingredients.Length > C1; C1++)
                {
                    Recipe += Language.GetString(ingredients[C1].GetPickupNameToken());
                    if (ingredients.Length > 1)
                    {
                        Recipe += ", ";
                    }
                }
                Recipe += "\nSlot2 : ";
                ingredients = CRAFTING[i].possibleIngredients[1].pickups;
                for (int C2 = 0; CRAFTING[i].possibleIngredients[1].pickups.Length > C2; C2++)
                {
                    if (ingredients.Length > 1 && C2 > 0)
                    {
                        Recipe += ", ";
                    }
                    Recipe += Language.GetString(ingredients[C2].GetPickupNameToken());
                }
                Recipe += "\nResult = " + Language.GetString(CRAFTING[i].result.GetPickupNameToken());
                if (CRAFTING[i].amountToDrop > 1)
                {
                    Recipe += " x" + CRAFTING[i].amountToDrop;
                }
                Recipe += "\n";
                log += Recipe;
            }
            DumpAll.logger.LogMessage(log);

            //Jackpot odds are 1/8

            log = "\n\nDrifter Cleanup Projectile Family\n\n";
            var cleanupProjectileFamily = Addressables.LoadAssetAsync<ProjectileFamily>(key: "1455ae2add391c244986a9aa4c41a5cf").WaitForCompletion();
            float total = cleanupProjectileFamily.totalWeight;


            foreach (var entry in cleanupProjectileFamily.candidates)
            {
                bool jackpot = entry.jackpotProjectilePrefab;
                float chance = (jackpot ? entry.weight * 0.875f : entry.weight);

                log += entry.projectilePrefab.name + "\n";
                log += "-Damage: "+entry.damageCoefficient * 100 + "%\n";
                //log += "-Weight: " + chance + "\n";
                log += "-PullChance: " + (chance / total)*100 + "%\n";

                if (entry.jackpotProjectilePrefab)
                {
                    chance = entry.weight * 0.125f;
                    log += "\n";
                    log += entry.jackpotProjectilePrefab.name +
                        ((entry.jackpotExtraProjectileCount > 0) ? " x" + (1f+entry.jackpotExtraProjectileCount) : "") +
                        "\n";
                    log += "-Damage: " + entry.jackpotDamageCoefficient * 100 + "%\n";
                    //log += "-Weight: " + chance + "\n";
                    log += "-PullChance: " + (chance / total) * 100 + "%\n";
                }
                log += "\n";

            }
            DumpAll.logger.LogMessage(log);

        }



    }



 

}
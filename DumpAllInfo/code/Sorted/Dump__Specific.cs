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
using RoR2.Projectile;


namespace DumpAll
{
    public static class Dump__Specific
    {
        public static void DumpInfo()
        {
            string log = string.Empty;
            //NodeGraph[] AllNodeGraph = Resources.FindObjectsOfTypeAll<NodeGraph>();

            /*foreach (NodeGraph nodeGraph in AllNodeGraph)
            {
                DumpAll.logger.LogMessage(nodeGraph + " " + nodeGraph.GetActiveNodesForHullMaskWithFlagConditions(HullMask.Human, NodeFlags.NoCeiling, NodeFlags.None).Count);
                DumpAll.logger.LogMessage(nodeGraph + " " + nodeGraph.GetActiveNodesForHullMaskWithFlagConditions(HullMask.Golem, NodeFlags.NoCeiling, NodeFlags.None).Count);
                DumpAll.logger.LogMessage(nodeGraph + " " + nodeGraph.GetActiveNodesForHullMaskWithFlagConditions(HullMask.BeetleQueen, NodeFlags.NoCeiling, NodeFlags.None).Count);
            }*/

            DitherlessPlayerSkins();
            DitherlessItemDisplays();
            /*

            DelayBlast[] DelayBlastS = Resources.FindObjectsOfTypeAll<DelayBlast>();
            ProjectileExplosion[] ProjectileExplosionS = Resources.FindObjectsOfTypeAll<ProjectileExplosion>();
            string logSweet = string.Empty;
            string logH2 = string.Empty;
            logSweet = "\n\nAll Sweetspot BlastAttacks\n\n";
            log = "\n\nAll BlastAttack\n\n";
            logSweet += "In DelayBlast\n\n";
            log += "In DelayBlast\n\n";
            foreach (var obj in DelayBlastS)
            {
                log += obj.name + " | "+ obj.falloffModel + "\n";
                if (obj.falloffModel == BlastAttack.FalloffModel.HalfLinear)
                {
                    logSweet += obj.name + "\n";
                }
            }
            log += "\nIn ProjectileExplosion\n\n";
            logSweet += "\nIn ProjectileExplosion\n\n";
            foreach (var obj in ProjectileExplosionS)
            {
                log += obj.name + " | " + obj.falloffModel + "\n";
                if (obj.falloffModel == BlastAttack.FalloffModel.HalfLinear)
                {
                    logSweet += obj.name + "\n";
                }
            }

            DumpAll.logger.LogMessage(log);
            DumpAll.logger.LogMessage(logSweet);*/


            log = string.Empty;
            GenericInteraction[] genericInteraction = Resources.FindObjectsOfTypeAll<GenericInteraction>();
            PurchaseInteraction[] purchaseInteraction = Resources.FindObjectsOfTypeAll<PurchaseInteraction>();
 
            log = "\n\n--RemoteOp Interactables--";
            log += "\n---------------------------\n";
 
   
            foreach (var obj in genericInteraction)
            {
                log += obj.name + " | " + obj.allowRemoteOpInteraction + "\n";
            }
            foreach (var obj in purchaseInteraction)
            {
                log += obj.name + " | " + obj.allowRemoteOpInteraction + "\n";
            }
            DumpAll.logger.LogMessage(log);



        }

        public static void DitherlessPlayerSkins()
        {
            List<Material> materials = new List<Material>();
            List<Material> materialsMASTERLESS = new List<Material>();

            foreach (CharacterBody body in BodyCatalog.allBodyPrefabBodyBodyComponents)
            {
                bool masterLess = body.bodyFlags.HasFlag(CharacterBody.BodyFlags.Masterless);
                SkinDef[] skinDefs = SkinCatalog.GetBodySkinDefs(body.bodyIndex);
                foreach (SkinDef skin in skinDefs)
                {
                    Debug.Log(skin);
                    AsyncOperationHandle<SkinDefParams> loadHandle = default(AsyncOperationHandle<SkinDefParams>);
                    ValueTuple<AssetReferenceT<SkinDefParams>, SkinDefParams> skinParams = skin.GetSkinParams();
                    AssetReferenceT<SkinDefParams> skinParamsAddressRef = skinParams.Item1;
                    SkinDefParams item = skinParams.Item2;
                    SkinDefParams skinDefParams;
                    if (skinParamsAddressRef.RuntimeKeyIsValid())
                    {
                        loadHandle = AssetAsyncReferenceManager<SkinDefParams>.LoadAsset(skinParamsAddressRef, AsyncReferenceHandleUnloadType.AtWill);
                        skinDefParams = loadHandle.Result;
                    }
                    else
                    {
                        skinDefParams = item;
                    }

                    foreach (CharacterModel.RendererInfo render in skinDefParams.rendererInfos)
                    {
                        if (!(render.renderer is ParticleSystemRenderer))
                        {
                            AssetReferenceT<Material> defaultMaterialAddress = render.defaultMaterialAddress;
                            bool flag = defaultMaterialAddress != null;
                            if (flag)
                            {
                                Material material = AssetAsyncReferenceManager<Material>.LoadAsset(defaultMaterialAddress).WaitForCompletion();
                                if (material)
                                {
                                    if (material.HasFloat("_DitherOn"))
                                    {
                                        if (material && !material.IsKeywordEnabled("DITHER"))
                                        {
                                            if (masterLess)
                                            {
                                                if (!materialsMASTERLESS.Contains(material))
                                                {
                                                    materialsMASTERLESS.Add(material);
                                                }
                                            }
                                            else
                                            {
                                                if (!materials.Contains(material))
                                                {
                                                    materials.Add(material);
                                                }
                                            }
                                        }
                                    }
                                    else if (!material.HasFloat("_DitherOn") && material.IsKeywordEnabled("DITHER"))
                                    {
                                        Debug.Log("Material that does not Support Dither but has Dither?? :" + material);
                                    }
                                }
                            }
                        }
                    }

                }
            }

            for (int i = 0; i < BodyCatalog.bodyCount; i++)
            {
              
                
            }
            string log = "\n\n---Monster Materials missing Dither---\n----------------------\n";
            foreach (Material mat in materials)
            {
                log += mat.name + "\n";
            }
            DumpAll.logger.LogMessage(log);

            log = "\n\n---MasterlessBody Materials missing Dither---\n----------------------\n";
            foreach (Material mat in materialsMASTERLESS)
            {
                log += mat.name + "\n";
            }
            DumpAll.logger.LogMessage(log);
        }

        public static void DitherlessItemDisplays()
        {
            string log = "\n\n---ItemDisplay Materials missing Dither---\n\n";
            List<Material> materials = new List<Material>();

            ItemDisplay[] allDisplays = Resources.FindObjectsOfTypeAll<ItemDisplay>();
 
            foreach (ItemDisplay display in allDisplays)
            {
                if (!(display is DrifterProjectileDisplay))
                {
                    //Debug.Log(display);
                    foreach (CharacterModel.RendererInfo render in display.rendererInfos)
                    {
                        if (!(render.renderer is ParticleSystemRenderer))
                        {
                            Material material = render.defaultMaterial;
                           
                            if (!material)
                            {
                                AssetReferenceT<Material> defaultMaterialAddress = render.defaultMaterialAddress;
                                if (defaultMaterialAddress != null)
                                {
                                    material = AssetAsyncReferenceManager<Material>.LoadAsset(defaultMaterialAddress).WaitForCompletion();
                                }
                            }
                            if (material.HasFloat("_DitherOn"))
                            {
                                if (material && !material.IsKeywordEnabled("DITHER"))
                                {
                                    if (!materials.Contains(material))
                                    {
                                        materials.Add(material);
                                    }
                                }
                            }
                            else if (!material.HasFloat("_DitherOn") && material.IsKeywordEnabled("DITHER"))
                            {
                                Debug.Log("Material that does not Support Dither but has Dither?? :" + material);
                            }
                            else
                            {
                                Debug.Log("Material does not support Dither? : "+material);
                            }
                           
                        }

                    }
                }
              
            }


            materials.Sort((x, y) => string.Compare(x.name, y.name));
            foreach (Material mat in materials)
            {
                log += mat.name + "\n";
            }

            DumpAll.logger.LogMessage(log);
        }
 

    }




}
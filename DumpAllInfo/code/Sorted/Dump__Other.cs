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
    public static class Dump__Other
    {
        public static void DumpInfo()
        {
             
            string log = string.Empty;

 

            SceneCollection[] SceneCollectionS = Resources.FindObjectsOfTypeAll<SceneCollection>();
            List<SceneCollection> SceneCollection = SceneCollectionS.ToList();
            SceneCollection.Sort((x, y) => string.Compare(x.name, y.name));
            log = "\n\nScene Groups / Scene Collections\n\n";
            foreach (SceneCollection sg in SceneCollection)
            {
                log += sg.name + "\n";
                foreach (var entry in sg.sceneEntries)
                {
                    log += "-"+entry.sceneDef.cachedName + " | " + entry.weight+ "\n";
                    if (entry.fallbackSceneDef)
                    {
                        log += "--" + entry.fallbackSceneDef.cachedName + "\n";
                    }
                }
                log += "\n";
            }
            DumpAll.logger.LogMessage(log);

        }




    }




}
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
    public static class Dump__Colors
    {
        public static void DumpInfo()
        {
             
            string log = string.Empty;


            TMPro.TMP_StyleSheet TMPStyles = Addressables.LoadAssetAsync<TMPro.TMP_StyleSheet>(key: "54d1085f9a2fdea4587fcfc7dddcd4bc").WaitForCompletion();

            log = "\n\n---ColorCatalog---\n\n";
            for (int i = 0; i < (int)ColorCatalog.ColorIndex.Count; i++)
            {
                log += (ColorCatalog.ColorIndex)i + "\n";
                log += (Color)ColorCatalog.GetColor((ColorCatalog.ColorIndex)i) + "\n";
                log += ColorCatalog.GetColor((ColorCatalog.ColorIndex)i) + "\n";
                log += ColorCatalog.GetColorHexString((ColorCatalog.ColorIndex)i) + "\n";
                log += "\n";
            }
            log += "\n";
            DumpAll.logger.LogMessage(log);


            log = "\n\n---DamageColors---\n\n";
            for (int i = 0; i < (int)DamageColorIndex.Count; i++)
            {
                log += (DamageColorIndex)i + "\n";
                log += DamageColor.FindColor((DamageColorIndex)i) + "\n";
                log += (Color32)DamageColor.FindColor((DamageColorIndex)i) + "\n";
                log += Util.RGBToHex(DamageColor.FindColor((DamageColorIndex)i)) + "\n";
                log += "\n";
            }
            log += "\n";
            DumpAll.logger.LogMessage(log);

            /*log = "\n\n---StyleColors---\n\n";
            for (int i = 0; i < (int)TMPStyles.; i++)
            {
                log += (DamageColorIndex)i + "\n";
                log += DamageColor.FindColor((DamageColorIndex)i) + "\n";
                log += (Color32)DamageColor.FindColor((DamageColorIndex)i) + "\n";
                log += Util.RGBToHex(DamageColor.FindColor((DamageColorIndex)i)) + "\n";
                log += "\n";
            }
            log += "\n";
            DumpAll.logger.LogMessage(log);*/

        }




    }




}
using CG.Ship.Modules.Weapons;
using CG.Space;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IndependentBrains
{
    [HarmonyPatch(typeof(AutoShootingTurretController))]
    internal class AutoShootingTurretControllerPatch
    {
        private static readonly MethodInfo SetActiveTargetMethod = AccessTools.Method(typeof(AutoShootingTurretController), "SetActiveTarget");

        private static Random random = new();

        [HarmonyPrefix]
        [HarmonyPatch("SelectBestTarget")]
        static void SelectBestTargetPrefix(WeaponTarget ___activeTarget, out OrbitObject __state)
        {
            __state = ___activeTarget.TargetedUnit;
        }

        [HarmonyPostfix]
        [HarmonyPatch("SelectBestTarget")]
        static void SelectBestTargetPostfix(AutoShootingTurretController __instance, WeaponTarget ___activeTarget,
            List<WeaponTarget> ___potentialTargets, ref int ___currentTargetPriority, OrbitObject __state)
        {
            //Proceed if the current target is a fighter, a mine, or all config, and a new target was just selected
            if (((___activeTarget.TargetedUnit is not AbstractDrone || !Configs.randomFightersConfig.Value) &&
                (___activeTarget.TargetedUnit is not AbstractProximityMine || !Configs.randomMinesConfig.Value) &&
                !Configs.randomAllConfig.Value) ||
                ___activeTarget.TargetedUnit == __state) return;

            //Reset target priority
            int lastTargetPriority = ___currentTargetPriority;
            ___currentTargetPriority = -1;
            //Randomise target order
            ___potentialTargets = ___potentialTargets.OrderBy((_) => random.Next()).ToList();

            foreach (WeaponTarget weaponTarget in ___potentialTargets)
            {
                //Only select other targets of the same type
                if (weaponTarget.TargetedUnit.GetType() != ___activeTarget.TargetedUnit.GetType() &&
                    !Configs.randomAllConfig.Value) continue;

                if (ProjectileUtils.IsHigherPriorityTarget(weaponTarget.TargetedUnit, ___currentTargetPriority, out int num)) {
                    SetActiveTargetMethod.Invoke(__instance, new object[] { weaponTarget });
                    ___currentTargetPriority = num;
                }
            }

            //If no other targets were found, keep shooting at the same target
            if (___currentTargetPriority == -1)
                ___currentTargetPriority = lastTargetPriority;
        }
    }
}

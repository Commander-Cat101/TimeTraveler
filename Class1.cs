using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using TimeTraveler.Displays.Projectiles;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;

[assembly: MelonModInfo(typeof(TemplateMod.Main), "The TIME TRAVELER", "2.0.0", "Commander__Cat")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace T
{
    public class Main : BloonsTD6Mod
    {

    }
}
namespace TimeTraveler.Displays.Projectiles
{
    public class DinoBlowDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "DinoBlow-Icon");
        }
    }
    public class TRexCrushDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "TrexCrush-Icon");
        }
    }
}
namespace TimeTravelerTower.Displays.Tier5
{
    public class TimeTraveler500 : ModTowerDisplay<TimeTraveler>
    {

        public override string BaseDisplay => GetDisplay(TowerType.DartlingGunner, 0, 4, 0);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[0] == 5;
        }

        public override float Scale => 1.1f;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.SaveMeshTexture();
            SetMeshTexture(node, "TimeTraveler500Display");


        }
    }
    public class TimeTraveler005 : ModTowerDisplay<TimeTraveler>
    {

        public override string BaseDisplay => GetDisplay(TowerType.MortarMonkey);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[2] == 5;
        }

        public override float Scale => 0.9f;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.SaveMeshTexture();
            node.RemoveBone("SuperMonkeyRig:Dart");
            SetMeshTexture(node, "TimeTraveler005Display");
            

        }
    }
    public class TimeTraveler050 : ModTowerDisplay<TimeTraveler>
    {

        public override string BaseDisplay => GetDisplay(TowerType.CaveMonkey);

        public override bool UseForTower(int[] tiers)
        {
            return tiers[1] == 5;
        }

        public override float Scale => 0.9f;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            node.SaveMeshTexture();
            SetMeshTexture(node, "TimeTraveler050Display");


        }
    }
}
namespace TimeTravelerTower
{

    public class TimeTraveler : ModTower
    {

        public override TowerSet TowerSet => TowerSet.Primary;
        public override string BaseTower => TowerType.EngineerMonkey;
        public override int Cost => 100; 
        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;
        public override string Description => "After 15 years of work the engineer monkey has created a time machine to see the past, present, and future *Useless without upgrades";
        public override ParagonMode ParagonMode => ParagonMode.Base555;
        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {

            towerModel.range += 10;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 10;
            attackModel.weapons[0].projectile.GetDamageModel().damage = 0;
        }
        public override IEnumerable<int[]> TowerTiers()
        {
            yield return new[] { 0, 0, 0 };
            for (var i = 1; i < 5; i++)
            {
                yield return new[] { 1, 0, 0 };
                yield return new[] { 2, 0, 0 };
                yield return new[] { 3, 0, 0 };
                yield return new[] { 4, 0, 0 };
                yield return new[] { 5, 0, 0 };
                yield return new[] { 0, 1, 0 };
                yield return new[] { 0, 2, 0 };
                yield return new[] { 0, 3, 0 };
                yield return new[] { 0, 4, 0 };
                yield return new[] { 0, 5, 0 };
                yield return new[] { 0, 0, 1 };
                yield return new[] { 0, 0, 2 };
                yield return new[] { 0, 0, 3 };
                yield return new[] { 0, 0, 4 };
                yield return new[] { 0, 0, 5 };
            }
        }
    }

}
namespace TimeTravelerTower.Upgrades.TopPath
{
    public class AdvancedWeaponry : ModUpgrade<TimeTraveler>    
    {
        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 720;
        public override string Description => "Futuristic weapons allow maximum damage";
        public override string Portrait => "TimeTraveler-Portrait";
        public override void ApplyUpgrade(TowerModel tower)
        {
            
            foreach (var attackModel in tower.GetWeapons())
            {
                attackModel.Rate = .45f;
                
            }
            foreach (var projectile in tower.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                projectile.GetDamageModel().damage += 2;
            }
        }
    }
    public class UpgradedLasers : ModUpgrade<TimeTraveler>
    {

        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 430;
        public override string Portrait => "TimeTraveler-Portrait";

        public override string Description => "Better lasers inflict a shock onto the bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .40f;
            attackModel.weapons[0].projectile = Game.instance.model.GetTowerFromId("DartlingGunner-200").GetAttackModel().weapons[0].projectile.Duplicate();
            attackModel.range = 50f;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 1f;
        }
    }
    public class CloserCombat : ModUpgrade<TimeTraveler>
    {

        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 2430;
        public override string Description => "Gains a plasma sword to damage the bloons at a close range";

        public override string Portrait => "TimeTraveler-Portrait";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .35f;
            var sword = Game.instance.model.GetTowerFromId("Sauda 7").GetAttackModel().Duplicate();
            sword.range = towerModel.range / 2;
            sword.name = "Sword_Weapon";
            towerModel.AddBehavior(sword);
            foreach (var attacks in towerModel.GetAttackModels())
            {
                if (attacks.name.Contains("sword"))
                {
                    attacks.weapons[0].Rate = .2f;
                }

            }
        }
    }
    public class DualSaber : ModUpgrade<TimeTraveler>
    {

        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 5430;

        public override string Description => "This double sided plasma sword is fit for any warrior";

        public override string Portrait => "TimeTraveler-Portrait";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            foreach (var attacks in towerModel.GetAttackModels())
            {
                if (attacks.name.Contains("sword"))
                {
                    attacks.weapons[0] = Game.instance.model.GetTowerFromId("Sauda 13").GetAttackModel().weapons[0].Duplicate();
                    attacks.weapons[0].Rate = .2f;
                }

            }
            attackModel.weapons[0].projectile.GetDamageModel().damage += 2f;
            attackModel.weapons[0].Rate = .30f;
        }
    }
    public class FiveThousandYearsInTheFuture : ModUpgrade<TimeTraveler>
    {
        public override int Path => TOP;
        public override int Tier => 5;
        public override int Cost => 225430;
        public override string Description => "This far into the future anything is possible";

        public override string Portrait => "TimeTraveler-Portrait";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("Etienne 20").GetBehavior<DroneSupportModel>().Duplicate());
            towerModel.range *= 2;
            attackModel.range *= 2;
            towerModel.GetBehavior<DroneSupportModel>().count = 3;
            attackModel.weapons[0].Rate = .2f;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 125f;
            attackModel.weapons[0].Rate = .02f;
            foreach (var attacks in towerModel.GetAttackModels())
            {
                if (attacks.name.Contains("sword"))
                {
                    attacks.weapons[0] = Game.instance.model.GetTowerFromId("Sauda 20").GetAttackModel().weapons[0].Duplicate();
                    attacks.weapons[0].Rate = .1f;
                    attacks.weapons[0].projectile.GetDamageModel().damage *= 10;
                    attacks.weapons[0].projectile.pierce *= 5;
                }

            }
        }
    }
}
namespace TimeTravelerTower.Upgrades.MiddlePath
{
    public class WorldsFirstSpear : ModUpgrade<TimeTraveler>
    {
        public override string Portrait => "TimeTraveler-Portrait";
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 470;


        public override string Description => "This spear carved with rocks and stones, is the first of its kind";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].Rate = .80f;
            attackModel.weapons[0].projectile.pierce = 2;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 2f;
        }
    }
    public class HigherGround : ModUpgrade<TimeTraveler>
    {
        public override string Portrait => "TimeTraveler-Portrait";
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 300;
        public override string Description => "Higher ground allows more vision over the land";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.range *= 1.3f;
            attackModel.range *= 1.3f;
        }
    }
    public class DinoHornSpear : ModUpgrade<TimeTraveler>
    {
        public override string Portrait => "TimeTraveler-Portrait";

        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 960;
        public override string Description => "With a dinosaur horn on the end, most stats are increased";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].projectile.pierce += 1;
            attackModel.weapons[0].projectile.GetDamageModel().damage *= 2f;
        }
    }
    public class DinoBlow : ModUpgrade<TimeTraveler>
    {
        public override string Portrait => "TimeTraveler-Portrait";
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 3640;

        public override string Description => "Ability: Sends a pet dinosaur flying towards the bloons";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].projectile.pierce += 1;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 3f;
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BombShooter-040").GetBehavior<AbilityModel>().Duplicate());
            var attackAbil = towerModel.GetBehavior<AbilityModel>().GetBehavior<ActivateAttackModel>();
            attackAbil.attacks[0].weapons[0].projectile.GetDamageModel().damage = 1000f;
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "DinoBlow-Icon");
            attackAbil.attacks[0].weapons[0].projectile.ApplyDisplay<DinoBlowDisplay>();
            towerModel.GetBehavior<AbilityModel>().cooldown = 15f;
        }
    }
    public class TrexCrush : ModUpgrade<TimeTraveler>
    {
        public override string Portrait => "TimeTraveler-Portrait";
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 47890;

        public override string DisplayName => "T-Rex Crush";

        public override string Description => "Ability: T-Rex goes brrr";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].projectile.pierce += 3;
            attackModel.weapons[0].projectile.GetDamageModel().damage += 20f;
            var attackAbil = towerModel.GetBehavior<AbilityModel>().GetBehavior<ActivateAttackModel>();
            attackAbil.attacks[0].weapons[0].projectile.GetDamageModel().damage = 7500f;
            towerModel.GetBehavior<AbilityModel>().icon = GetSpriteReference(mod, "TrexCrush-Icon");
            attackAbil.attacks[0].weapons[0].projectile.ApplyDisplay<TRexCrushDisplay>();
            attackAbil.attacks[0].weapons[0].projectile.pierce = 1000;
            towerModel.GetBehavior<AbilityModel>().cooldown = 10f;
        }
        public class PowerPunch : ModUpgrade<TimeTraveler>
        {
            public override string Portrait => "TimeTraveler-Portrait";
            public override int Path => BOTTOM;
            public override int Tier => 1;
            public override int Cost => 360;
            public override string Description => "The time traveler punches bloons with their fists";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();
                attackModel.weapons[0] = Game.instance.model.GetTowerFromId("Sauda 4").GetAttackModel().weapons[0].Duplicate();
                attackModel.weapons[0].Rate = .80f;
                towerModel.range = 40f;
                attackModel.range = 40f;
            }
        }
        public class Looting : ModUpgrade<TimeTraveler>
        {
            public override string Portrait => "TimeTraveler-Portrait";
            public override int Path => BOTTOM;
            public override int Tier => 2;
            public override int Cost => 640;
            public override string Description => "The time traveler now gives 50 cash a round";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();
                attackModel.weapons[0].Rate = .70f;
                towerModel.range = 45f;
                attackModel.range = 45f;
                towerModel.AddBehavior(new PerRoundCashBonusTowerModel("merchantmen_pirate_crew", 50.0f, 0.0f, 1.0f, CreatePrefabReference("80178409df24b3b479342ed73cffb63d"), false));
            }
        }
        public class RagingRoar : ModUpgrade<TimeTraveler>
        {
            public override string Portrait => "TimeTraveler-Portrait";
            public override int Path => BOTTOM;
            public override int Tier => 3;
            public override int Cost => 1670;
            public override string Description => "Raging roar makes monkeys attack faster";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();

                towerModel.AddBehavior(new RateSupportModel("RateSupportModel_Support_JungleDrums", 0.70f, true, "Village:Rate", false, 1, null, "JungleDrumsBuff", "BuffIconVillage2xx"));
            }
        }
        public class ExtraLoot : ModUpgrade<TimeTraveler>
        {
            public override string Portrait => "TimeTraveler-Portrait";
            public override int Path => BOTTOM;
            public override int Tier => 4;
            public override int Cost => 4890;
            public override string Description => "Pirate Alliances with the merchant men give them more money gain";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();
                attackModel.weapons[0] = Game.instance.model.GetTowerFromId("Sauda 10").GetAttackModel().weapons[0].Duplicate();
                towerModel.AddBehavior(new CentralMarketBuffModel("ExtraLootBuffModel_", 1.5f, "CentralMarketBuff", "CentralMarketBuff", "BuffIconBananaFarmxx4", 3));
                towerModel.GetBehavior<RateSupportModel>().multiplier = .50f;
                towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound = 100f;
            }
        }
        public class PowerCannon : ModUpgrade<TimeTraveler>
        {
            public override string Portrait => "TimeTraveler-Portrait";
            public override int Path => BOTTOM;
            public override int Tier => 5;
            public override int Cost => 27000;
            public override string Description => "Gains a extra power cannon attack";

            public override void ApplyUpgrade(TowerModel towerModel)
            {
                var attackModel = towerModel.GetAttackModel();
                var cannon = Game.instance.model.GetTowerFromId("BombShooter-500").GetAttackModel().Duplicate();
                cannon.range = towerModel.range;
                cannon.name = "Cannon_Weapon";
                towerModel.AddBehavior(cannon);
                foreach (var attacks in towerModel.GetAttackModels())
                {
                    if (attacks.name.Contains("Cannon"))
                    {
                        attacks.weapons[0].Rate = 1.8f;
                    }

                }

            }
        }
    }


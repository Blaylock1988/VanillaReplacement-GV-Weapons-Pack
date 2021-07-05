using VRageMath;
using System.Collections.Generic;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.ModelAssignmentsDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.HardwareDef.ArmorState;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.Prediction;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.BlockTypes;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.Threat;

namespace WeaponThread
{
    partial class Weapons
    {

		//Common definitions
		
		private TargetingDef Ballistics_Cannons_Targeting => new TargetingDef {
			Threats = new[] {
				Grids,
			},
			SubSystems = new[] {
				Thrust, Utility, Offense, Power, Production, Jumping, Steering, Any,
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 3000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 20, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private UiDef Ballistics_Cannons_Hardpoint_Ui = new UiDef {
			RateOfFire = false,
			DamageModifier = false,
			ToggleGuidance = false,
			EnableOverload =  false,
		};

		private AiDef Ballistics_Cannons_Hardpoint_Ai_Turret = new AiDef {
			TrackTargets = true, //This Weapon will know there are targets in range
			TurretAttached = true, // This enables the ability for players to manually control
			TurretController = true, //The turret in this WeaponDefinition has control over where other turrets aim.
			PrimaryTracking = false, //The turret in this WeaponDefinition selects targets for other turrets that do not have tracking capabilities.
			LockOnFocus = false, // fires this weapon when something is locked using the WC hud reticle
			SuppressFire = false, //prevent automatic firing
			OverrideLeads = false, // Override default behavior for target leads
		};

		private AiDef Ballistics_Cannons_Hardpoint_Ai_Gun = new AiDef {
			TrackTargets = false,
			TurretAttached = false,
			TurretController = false,
			PrimaryTracking = false,
			LockOnFocus = false,
			SuppressFire = true,
			OverrideLeads = false, // Override default behavior for target leads
		};

		private OtherDef Ballistics_Cannons_Hardpoint_Other_Large = new OtherDef {
			GridWeaponCap = 0,
			RotateBarrelAxis = 0,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 2.5f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private OtherDef Ballistics_Cannons_Hardpoint_Other_Small = new OtherDef {
			GridWeaponCap = 0,
			RotateBarrelAxis = 0,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private HardPointAudioDef Ballistics_Cannons_Hardpoint_Audio = new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "ArcWepShipARYXJudge_Fire", // subtype name from sbc
			FiringSoundPerShot = true,
			ReloadSound = "",
			NoAmmoSound = "ArcWepShipGatlingNoAmmo",
			HardPointRotationSound = "WepTurretGatlingRotate",
			BarrelRotationSound = "",
		};

		private HardPointParticleDef Ballistics_Cannons_Hardpoint_Graphics = new HardPointParticleDef {
			Barrel1 = new ParticleDef
			{
				Name = "MXA_SmallCoilgunMuzzleFlash", // OKI_230mm_Muzzle_Flash 
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,-1f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 1,
					Scale = 1.0f,
				}
			},
		};

		//Weapon Definitions

        WeaponDefinition MXA_CoilgunL => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "MXA_CoilgunL",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 0.5f, //this is the GeneralDamageMultiplier of the weapon
                        IconName = ""
                    },
                },
                Barrels = new[] {
                    "muzzle_projectile_1",
                    "muzzle_projectile_2",
                },
                Ejector = "",
                Scope = "scope", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Ballistics_Cannons_Targeting,
			
            HardPoint = new HardPointDef
            {
                WeaponName = "M66 Sentry", // name of weapon in terminal
                DeviateShotAngle = 0.4f,
                AimingTolerance = 0.5f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Ballistics_Cannons_Hardpoint_Ui,
				
                Ai = Ballistics_Cannons_Hardpoint_Ai_Turret,
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0.01f,
                    ElevateRate = 0.005f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -4,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 1.10f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },
                
				Other = Ballistics_Cannons_Hardpoint_Other_Large,
				
                Loading = new LoadingDef
                {
                    RateOfFire = 120, //180 // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 180, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 60, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 0, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 4,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },

                Audio = Ballistics_Cannons_Hardpoint_Audio,
				
                Graphics = Ballistics_Cannons_Hardpoint_Graphics,
				
            },
            Ammos = new[] {
                Ballistics_Cannon,
            },
            Animations = MXA_CoilgunL_Animation,
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };

        WeaponDefinition VehicleTurret122mm => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef 
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "OKI122mmVT",
                        AimPartId = "",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 0.5f,
                        IconName = ""
                    },
                },
                Barrels = new [] {
                    "muzzle_projectile_1",
                },
                Ejector = "",
                Scope = "", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Ballistics_Cannons_Targeting,
			
            HardPoint = new HardPointDef 
            {
                WeaponName = "Vehicle 122mm Assault Gun Turret", // name of weapon in terminal
                DeviateShotAngle = 0.4f,
                AimingTolerance = 0.5f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Ballistics_Cannons_Hardpoint_Ui,
				
                Ai = Ballistics_Cannons_Hardpoint_Ai_Turret,

                HardWare = new HardwareDef {
                    RotateRate = 0.02f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -120,
                    MaxAzimuth = 120,
                    MinElevation = -15,
                    MaxElevation = 45,
                    FixedOffset = false,
                    InventorySize = 0.8f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },

				Other = Ballistics_Cannons_Hardpoint_Other_Small,

                Loading = new LoadingDef {
                    RateOfFire = 120,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 0, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 1,
                    DelayAfterBurst = 120, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },

                Audio = Ballistics_Cannons_Hardpoint_Audio,
				
                Graphics = Ballistics_Cannons_Hardpoint_Graphics,
				
            },
            Ammos = new [] {
                Ballistics_Cannon,
            },
            //Animations = AdvancedAnimation,
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };

        WeaponDefinition SmallCannon122mm => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef 
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "OKI122mmSGfixed",
                        AimPartId = "",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.5f,
                        IconName = ""
                    },

                },
                Barrels = new [] {
                    "muzzle_projectile_1",
                },
                Ejector = "",
                Scope = "", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Ballistics_Cannons_Targeting,
			
            HardPoint = new HardPointDef 
            {
                WeaponName = "122mm Fixed Assault Cannon", // name of weapon in terminal
                DeviateShotAngle = 0.4f,
                AimingTolerance = 0f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Ballistics_Cannons_Hardpoint_Ui,
				
                Ai = Ballistics_Cannons_Hardpoint_Ai_Gun,

                HardWare = new HardwareDef {
                    RotateRate = 0f,
                    ElevateRate = 0f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = false,
                    InventorySize = 0.64f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },

				Other = Ballistics_Cannons_Hardpoint_Other_Small,

                Loading = new LoadingDef {
                    RateOfFire = 120,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 0, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 1,
                    DelayAfterBurst = 120, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },

                Audio = Ballistics_Cannons_Hardpoint_Audio,
				
                Graphics = Ballistics_Cannons_Hardpoint_Graphics,
				
            },
            Ammos = new [] {
                Ballistics_Cannon,
            },
            //Animations = AdvancedAnimation,
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };

        WeaponDefinition LargeCannon230mm => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef 
            {
                MountPoints = new[] {                  
                    new MountPointDef {
                        SubtypeId = "OKI230mmBCfixed",
                        AimPartId = "",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.5f,
                        IconName = ""
                    },

                },
                Barrels = new [] {
                    "muzzle_missile_1",
                },
                Ejector = "",
                Scope = "", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Ballistics_Cannons_Targeting,
			
            HardPoint = new HardPointDef 
            {
                WeaponName = "155mm Fixed Battlecannon", // name of weapon in terminal
                DeviateShotAngle = 0.4f,
                AimingTolerance = 0f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Ballistics_Cannons_Hardpoint_Ui,
				
                Ai = Ballistics_Cannons_Hardpoint_Ai_Gun,

                HardWare = new HardwareDef {
                    RotateRate = 0f,
                    ElevateRate = 0f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 0.96f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },
                
				Other = Ballistics_Cannons_Hardpoint_Other_Large,
				
                Loading = new LoadingDef {
                    RateOfFire = 120,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 0, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 2,
                    DelayAfterBurst = 300, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },

                Audio = Ballistics_Cannons_Hardpoint_Audio,
				
                Graphics = Ballistics_Cannons_Hardpoint_Graphics,
				
            },
            Ammos = new [] {
                Ballistics_Cannon,
            },
            //Animations = AdvancedAnimation,
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
    }
}
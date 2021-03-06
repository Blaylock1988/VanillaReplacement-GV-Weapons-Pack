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
		
		private TargetingDef Missiles_Rocket_Targeting => new TargetingDef {
			Threats = new[] {
				Grids,
			},
			SubSystems = new[] {
				Any,
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private UiDef Missiles_Rocket_Hardpoint_Ui = new UiDef {
			RateOfFire = false,
			DamageModifier = false,
			ToggleGuidance = false,
			EnableOverload =  false,
		};

		private AiDef Missiles_Rocket_Hardpoint_Ai_Turret = new AiDef 
		{
			TrackTargets = true, //This Weapon will know there are targets in range
			TurretAttached = true, // This enables the ability for players to manually control
			TurretController = true, //The turret in this WeaponDefinition has control over where other turrets aim.
			PrimaryTracking = false, //The turret in this WeaponDefinition selects targets for other turrets that do not have tracking capabilities.
			LockOnFocus = false, // fires this weapon when something is locked using the WC hud reticle
			SuppressFire = false, //prevent automatic firing
			OverrideLeads = false, // Override default behavior for target leads
		};

		private AiDef Missiles_Rocket_Hardpoint_Ai_Gun = new AiDef 
		{
			TrackTargets = false,
			TurretAttached = false,
			TurretController = false,
			PrimaryTracking = false,
			LockOnFocus = false,
			SuppressFire = true,
			OverrideLeads = false, // Override default behavior for target leads
		};

		private OtherDef Missiles_Rocket_Hardpoint_Other = new OtherDef {
			GridWeaponCap = 0,
			RotateBarrelAxis = 0,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private HardPointAudioDef Missiles_Rocket_Hardpoint_Audio = new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "WepShipSmallMissileShot", // subtype name from sbc
			FiringSoundPerShot = true,
			ReloadSound = "",
			NoAmmoSound = "ArcWepShipGatlingNoAmmo",
			HardPointRotationSound = "WepTurretGatlingRotate",
			BarrelRotationSound = "",
		};

		private HardPointParticleDef Missiles_Rocket_Hardpoint_Graphics = new HardPointParticleDef {
			Barrel1 = new ParticleDef
			{
				Name = "", // Smoke_LargeGunShot
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,-1f,0f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 200,
					MaxDuration = 1,
					Scale = 1.0f,
				}
			},
		};

		//Weapon Definitions

        WeaponDefinition LargeMissileLauncher => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LargeMissileLauncher",
                        AimPartId = "None",
                        MuzzlePartId = "None",
                        ElevationPartId = "None",
                        AzimuthPartId = "None",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Barrels = new []
                {
                    "muzzle_missile_001",
					"muzzle_missile_002",
					"muzzle_missile_003",
					"muzzle_missile_004",
					"muzzle_missile_005",
					"muzzle_missile_006",
					"muzzle_missile_007",					
                    "muzzle_missile_008",
					"muzzle_missile_009",
					"muzzle_missile_010",
					"muzzle_missile_011",
					"muzzle_missile_012",
					"muzzle_missile_013",
					"muzzle_missile_014",
					"muzzle_missile_015",
					"muzzle_missile_016",
					"muzzle_missile_017",
					"muzzle_missile_018",
					"muzzle_missile_019",
                },
            },
			
			Targeting = Missiles_Rocket_Targeting, //shared targeting def
            
			HardPoint = new HardPointDef
            {
                WeaponName = "LargeMissileLauncher", // name of weapon in terminal
                DeviateShotAngle = 4f,
                AimingTolerance = 0f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = Missiles_Rocket_Hardpoint_Ui,

                Ai = Missiles_Rocket_Hardpoint_Ai_Gun,
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0f,
                    ElevateRate = 0f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 3.52f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
				
                Other = Missiles_Rocket_Hardpoint_Other,
				
                Loading = new LoadingDef
                {
                    RateOfFire = 480,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
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
                    ShotsInBurst = 19,
                    DelayAfterBurst = 480, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },
				
                Audio = Missiles_Rocket_Hardpoint_Audio,
				
                Graphics = Missiles_Rocket_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Missiles_Rocket,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition LargeMissileTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LargeMissileTurret",
                        AimPartId = "MissileTurretBarrels",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                    new MountPointDef
                    {
                        SubtypeId = "LargeMissileTurret_Temporary",
                        AimPartId = "MissileTurretBarrels",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Barrels = new []
                {
					"muzzle_missile_001",
					"muzzle_missile_002",
					"muzzle_missile_003",
					"muzzle_missile_004",
					"muzzle_missile_005",
					"muzzle_missile_006",
                },
            },

			Targeting = Missiles_Rocket_Targeting, //shared targeting def

            HardPoint = new HardPointDef
            {
                WeaponName = "LargeMissileTurret", // name of weapon in terminal
                DeviateShotAngle = 4f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = Missiles_Rocket_Hardpoint_Ui,

                Ai = Missiles_Rocket_Hardpoint_Ai_Turret,

                HardWare = new HardwareDef
                {
                    RotateRate = 0.01f,
                    ElevateRate = 0.005f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -5,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 1.6f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
				
                Other = Missiles_Rocket_Hardpoint_Other,
				
                Loading = new LoadingDef
                {
                    RateOfFire = 480,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
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
                    ShotsInBurst = 6,
                    DelayAfterBurst = 240, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },

                Audio = Missiles_Rocket_Hardpoint_Audio,
				
                Graphics = Missiles_Rocket_Hardpoint_Graphics,

            },

			Ammos = new [] {
                Missiles_Rocket,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallMissileLauncher => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallMissileLauncher",
                        AimPartId = "None",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                    new MountPointDef
                    {
                        SubtypeId = "SmallMissileLauncher_Temporary",
                        AimPartId = "None",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_missile_001",
					"muzzle_missile_002",
					"muzzle_missile_003",
					"muzzle_missile_004",
                },
            },

			Targeting = Missiles_Rocket_Targeting, //shared targeting def

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallMissileLauncher", // name of weapon in terminal
                DeviateShotAngle = 4f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = Missiles_Rocket_Hardpoint_Ui,


                Ai = Missiles_Rocket_Hardpoint_Ai_Gun,

                HardWare = new HardwareDef
                {
                    RotateRate = 0f,
                    ElevateRate = 0f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 0.64f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
				
                Other = Missiles_Rocket_Hardpoint_Other,
				
                Loading = new LoadingDef
                {
                    RateOfFire = 480,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
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
                    ShotsInBurst = 4,
                    DelayAfterBurst = 120, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },

                Audio = Missiles_Rocket_Hardpoint_Audio,
				
                Graphics = Missiles_Rocket_Hardpoint_Graphics,

            },

			Ammos = new [] {
                Missiles_Rocket,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallMissileTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallMissileTurret",
                        AimPartId = "MissileTurretBarrels",
                        MuzzlePartId = "MissileTurretBarrels",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_missile_001",
					"muzzle_missile_002",
                },
            },

			Targeting = Missiles_Rocket_Targeting, //shared targeting def

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallMissileTurret", // name of weapon in terminal
                DeviateShotAngle = 4f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = Missiles_Rocket_Hardpoint_Ui,

                Ai = Missiles_Rocket_Hardpoint_Ai_Turret,

                HardWare = new HardwareDef
                {
                    RotateRate = 0.02f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -8,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0.640f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
				
                Other = Missiles_Rocket_Hardpoint_Other,
				
                Loading = new LoadingDef
                {
                    RateOfFire = 480,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
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
                    DelayAfterBurst = 80, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },

                Audio = Missiles_Rocket_Hardpoint_Audio,
				
                Graphics = Missiles_Rocket_Hardpoint_Graphics,

            },

			Ammos = new [] {
                Missiles_Rocket,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

    }
}
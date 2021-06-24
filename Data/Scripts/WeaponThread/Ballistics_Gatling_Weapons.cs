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

namespace WeaponThread {   
    partial class Weapons {

		//Common definitions
		
		private TargetingDef Ballistics_Gatlings_Targeting => new TargetingDef {
			Threats = new[] {
				Grids, Characters, Projectiles, // threats percieved automatically without changing menu settings
			},
			SubSystems = new[] {
				Offense, Thrust, Utility, Power, Production, Jumping, Steering, Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 2000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 8, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private UiDef Ballistics_Gatlings_Hardpoint_Ui = new UiDef {
			RateOfFire = false,
			DamageModifier = false,
			ToggleGuidance = false,
			EnableOverload =  false,
		};

		private AiDef Ballistics_Gatlings_Hardpoint_Ai_Turret = new AiDef {
			TrackTargets = true,
			TurretAttached = true,
			TurretController = false,
			PrimaryTracking = false,
			LockOnFocus = true,
			SuppressFire = false,
			OverrideLeads = false, // Override default behavior for target leads
		};

		private AiDef Ballistics_Gatlings_Hardpoint_Ai_Gun = new AiDef {
			TrackTargets = false,
			TurretAttached = false,
			TurretController = false,
			PrimaryTracking = false,
			LockOnFocus = false,
			SuppressFire = true,
			OverrideLeads = false, // Override default behavior for target leads
		};

		private OtherDef Ballistics_Gatlings_Hardpoint_Other = new OtherDef {
			GridWeaponCap = 0,
			RotateBarrelAxis = 3,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private LoadingDef Ballistics_Gatlings_Hardpoint_Loading_Turret = new LoadingDef {
			RateOfFire = 1200,
			BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
			BarrelsPerShot = 1,
			TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
			SkipBarrels = 0,
			ReloadTime = 240, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			HeatPerShot = 0, //heat generated per shot
			MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
			Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
			HeatSinkRate = 0, //amount of heat lost per second
			DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
			ShotsInBurst = 28,
			DelayAfterBurst = 120, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			FireFullBurst = false,
			GiveUpAfterBurst = false,
			DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
		};

		private LoadingDef Ballistics_Gatlings_Hardpoint_Loading_Gun = new LoadingDef {
			RateOfFire = 1200,
			BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
			BarrelsPerShot = 1,
			TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
			SkipBarrels = 0,
			ReloadTime = 240, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			HeatPerShot = 0, //heat generated per shot
			MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
			Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
			HeatSinkRate = 0, //amount of heat lost per second
			DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
			ShotsInBurst = 0,
			DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			FireFullBurst = false,
			GiveUpAfterBurst = false,
			DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
		};

		private HardPointAudioDef Ballistics_Gatlings_Hardpoint_Audio = new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "MD_GatlingLoop", // WM_Lightning, WepTurretInteriorFire, ArcWepShipGatlingShot
			FiringSoundPerShot = false,
			ReloadSound = "",
			NoAmmoSound = "WepShipGatlingNoAmmo",
			HardPointRotationSound = "WepTurretGatlingRotate", 
			BarrelRotationSound = "MD_GatlingBarrelLoop",
			FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
		};

		private HardPointParticleDef Ballistics_Gatlings_Hardpoint_Graphics = new HardPointParticleDef {
			Barrel1 = new ParticleDef
			{
				Name = "Muzzle_Flash_Large_Core", // OKI_230mm_Muzzle_Flash 
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,-0.5f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 0,
					Scale = 2f,
				}
			},
		};

		//Weapon Definitions

        WeaponDefinition LargeGatlingTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "LargeGatlingTurret",
                        AimPartId = "",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_projectile_1",
                },
            },
			
            Targeting = Ballistics_Gatlings_Targeting,
			
            HardPoint = new HardPointDef
            {
                WeaponName = "LargeGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 1f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Ballistics_Gatlings_Hardpoint_Ui,
				
                Ai = Ballistics_Gatlings_Hardpoint_Ai_Turret,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.02f,
                    ElevateRate = 0.015f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0.658f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading_Turret,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },
       
			Ammos = new [] {
                GatlingAmmoType2,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallGatlingTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingTurret",
                        AimPartId = "GatlingTurretBase2",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 1f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Accurate, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Ballistics_Gatlings_Hardpoint_Ui,
				
                Ai = Ballistics_Gatlings_Hardpoint_Ai_Turret,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.04f,
                    ElevateRate = 0.02f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0.360f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading_Turret,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                GatlingAmmoType2,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallGatlingGun => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingGun",
                        AimPartId = "Barrel",
                        MuzzlePartId = "Barrel",
                        ElevationPartId = "None",
                        AzimuthPartId = "None",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatlingGun", // name of weapon in terminal
                DeviateShotAngle = 1f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Ballistics_Gatlings_Hardpoint_Ui,
				
                Ai = Ballistics_Gatlings_Hardpoint_Ai_Gun,
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0.00f,
                    ElevateRate = 0.00f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 0.064f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading_Gun,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                GatlingAmmoType2,

            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallAutoGatlingGun => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingTurretAutoGatling",
                        AimPartId = "GatlingTurretBase2",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Barrels = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallAutoGatlingGun", // name of weapon in terminal
                DeviateShotAngle = 1f,
                AimingTolerance = 10f, // 0 - 180 firing angle
                AimLeadingPrediction = Accurate, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Ballistics_Gatlings_Hardpoint_Ui,
				
                Ai = Ballistics_Gatlings_Hardpoint_Ai_Turret,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.02f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -10,
                    MaxAzimuth = 10,
                    MinElevation = -10,
                    MaxElevation = 10,
                    FixedOffset = false,
                    InventorySize = 0.064f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading_Gun,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                GatlingAmmoType2,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

    }
}
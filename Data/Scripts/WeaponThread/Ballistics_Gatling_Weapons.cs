﻿using VRageMath;
using System.Collections.Generic;
using static Scripts.Structure;
using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.ModelAssignmentsDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.Prediction;
using static Scripts.Structure.WeaponDefinition.TargetingDef.BlockTypes;
using static Scripts.Structure.WeaponDefinition.TargetingDef.Threat;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef;
using static Scripts.Structure.WeaponDefinition.HardPointDef.HardwareDef.HardwareType;


/*--namespace WeaponThread {
    partial class Weapons {

		//Common definitions
		
		private TargetingDef Ballistics_Gatlings_Targeting_Turret => new TargetingDef {
			Threats = new[] {
				Projectiles, Characters, Grids,   // threats percieved automatically without changing menu settings
			},
			SubSystems = new[] {
				Offense, Thrust, Utility, Power, Production, Jumping, Steering, Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1500, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 8, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private TargetingDef Ballistics_Gatlings_Targeting_Fixed => new TargetingDef {
			Threats = new[] {
				Grids, // threats percieved automatically without changing menu settings
			},
			SubSystems = new[] {
				Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 8, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private OtherDef Ballistics_Gatlings_Hardpoint_Other = new OtherDef {
			GridWeaponCap = 0,
			RotateBarrelAxis = 3,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private LoadingDef Ballistics_Gatlings_Hardpoint_Loading = new LoadingDef {
			RateOfFire = 1200,
			BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
			BarrelsPerShot = 1,
			TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
			SkipBarrels = 0,
			ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			HeatPerShot = 3, //heat generated per shot
			MaxHeat = 240, //max heat before weapon enters cooldown (70% of max heat)
			Cooldown = 0.75f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
			HeatSinkRate = 25, //amount of heat lost per second
			DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
			ShotsInBurst = 0,
			DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			FireFullBurst = false,
			GiveUpAfterBurst = false,
			DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
		};

		private HardPointAudioDef Ballistics_Gatlings_Hardpoint_Audio = new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "MD_GatlingBlisterFire", // WM_Lightning, WepTurretInteriorFire, ArcWepShipGatlingShot, MD_GatlingLoop
			FiringSoundPerShot = true,
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
					MaxDistance = 500,
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
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Barrels = new []
                {
                    "muzzle_projectile_1",
                },
            },
			
            Targeting = Ballistics_Gatlings_Targeting_Turret,
			
            HardPoint = new HardPointDef
            {
                WeaponName = "LargeGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Common_Weapons_Hardpoint_Ui_ROFOnly,
				
                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.04f,
                    ElevateRate = 0.03f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0.8f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },
       
			Ammos = new [] {
                Ballistics_Gatling,
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

            Targeting = Ballistics_Gatlings_Targeting_Turret,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Common_Weapons_Hardpoint_Ui_ROFOnly,
				
                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.05f,
                    ElevateRate = 0.035f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0.8f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,
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
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Barrels = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting_Fixed,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatlingGun", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Common_Weapons_Hardpoint_Ui_ROFOnly,
				
                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0.00f,
                    ElevateRate = 0.00f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 0.5f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,

            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallGatlingGun_Gimbal => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingGun_Gimbal",
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

            Targeting = Ballistics_Gatlings_Targeting_Fixed,

            HardPoint = new HardPointDef
            {
                WeaponName = "SmallGatling Gimballed", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 5f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = Common_Weapons_Hardpoint_Ui_ROFOnly,
				
                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.01f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -15,
                    MaxAzimuth = 15,
                    MinElevation = -15,
                    MaxElevation = 15,
                    FixedOffset = false,
                    InventorySize = 0.5f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };
    }
}*/

namespace Scripts {   
    partial class Parts {

		//Common definitions
		
		private TargetingDef Ballistics_Gatlings_Targeting_Turret => new TargetingDef {
			Threats = new[] {
				Projectiles, Characters, Grids,   // threats percieved automatically without changing menu settings
			},
			SubSystems = new[] {
				Offense, Thrust, Utility, Power, Production, Jumping, Steering, Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1500, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 8, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private TargetingDef Ballistics_Gatlings_Targeting_Fixed => new TargetingDef {
			Threats = new[] {
				Grids, // threats percieved automatically without changing menu settings
			},
			SubSystems = new[] {
				Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
			LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
			MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 8, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private OtherDef Ballistics_Gatlings_Hardpoint_Other = new OtherDef {
			ConstructPartCap = 0, // Maximum number of these weapons on a grid
			RotateBarrelAxis = 3,
			EnergyPriority = 0,
			MuzzleCheck = false,
			Debug = false,
			RestrictionRadius = 0f, // Meters, radius of sphere disable this gun if another is present
			CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
			CheckForAnyWeapon = true, // if true, the check will fail if ANY gun is present, false only looks for this subtype
		};

		private LoadingDef Ballistics_Gatlings_Hardpoint_Loading = new LoadingDef {
			RateOfFire = 1200,
			BarrelsPerShot = 1,
			TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
			SkipBarrels = 0,
			ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			HeatPerShot = 3, //heat generated per shot
			MaxHeat = 240, //max heat before weapon enters cooldown (70% of max heat)
			Cooldown = 0.75f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
			HeatSinkRate = 25, //amount of heat lost per second
			DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
			ShotsInBurst = 0,
			DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			FireFullBurst = false,
			GiveUpAfterBurst = false,
			BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
			DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
			SpinFree = true, // Spin while not firing
			StayCharged = false, // Will start recharging whenever power cap is not full
		};

		private HardPointAudioDef Ballistics_Gatlings_Hardpoint_Audio = new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "MD_GatlingBlisterFire", // WM_Lightning, WepTurretInteriorFire, ArcWepShipGatlingShot, MD_GatlingLoop
			FiringSoundPerShot = true,
			ReloadSound = "",
			NoAmmoSound = "WepShipGatlingNoAmmo", 
			HardPointRotationSound = "WepTurretGatlingRotate", 
			BarrelRotationSound = "MD_GatlingBarrelLoop",
			FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
		};

		private HardPointParticleDef Ballistics_Gatlings_Hardpoint_Graphics = new HardPointParticleDef {
			Effect1 = new ParticleDef
			{
				Name = "Muzzle_Flash_Large_Core", // OKI_230mm_Muzzle_Flash 
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,-0.5f), //XYZ
				Extras = new ParticleOptionDef
				{
					Restart = false,
					MaxDistance = 500,
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
                        SpinPartId = "",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Muzzles = new []
                {
                    "muzzle_projectile_1",
                },
                Ejector = "GatlingBarrel",
                Scope = "camera", //Where line of sight checks are performed from must be clear of block collision            },
			},
            Targeting = Ballistics_Gatlings_Targeting_Turret,
			
            HardPoint = new HardPointDef
            {
                PartName = "LargeGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = true,
                    SuppressFire = false,
                    OverrideLeads = false, // Overrides default behavior for target lead
                },				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.04f,
                    ElevateRate = 0.03f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 15, // Default resting elevation
                    InventorySize = 0.8f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = false,
                        TerminalControls = true,
                        AmmoRound = "", //name of ammo for explosion effect
                    },
				},					
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },
       
			Ammos = new [] {
                Ballistics_Gatling,
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
                        SpinPartId = "GatlingTurretBase2",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Muzzles = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting_Turret,

            HardPoint = new HardPointDef
            {
                PartName = "SmallGatlingTurret", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = true,
                    SuppressFire = false,
                    OverrideLeads = false, // Overrides default behavior for target lead
                },				
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.05f,
                    ElevateRate = 0.035f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 90,
                    FixedOffset = false,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 15, // Default resting elevation
                    InventorySize = 0.8f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Type = BlockWeapon, // BlockWeapon, HandWeapon, Phantom 
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = false,
                        TerminalControls = true,
                        AmmoRound = "", //name of ammo for explosion effect
                    },
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,
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
                        SpinPartId = "Barrel",
                        MuzzlePartId = "Barrel",
                        ElevationPartId = "None",
                        AzimuthPartId = "None",
                        DurabilityMod = 0.25f,
                        IconName = "TestIcon.dds",
                    },
                },
                Muzzles = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting_Fixed,

            HardPoint = new HardPointDef
            {
                PartName = "SmallGatlingGun", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 4f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = false,
                    TurretAttached = false,
                    TurretController = false,
                    PrimaryTracking = false,
                    LockOnFocus = false,
                    SuppressFire = false,
                    OverrideLeads = false, // Overrides default behavior for target lead
                },				
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0.00f,
                    ElevateRate = 0.00f,
                    MinAzimuth = 0,
                    MaxAzimuth = 0,
                    MinElevation = 0,
                    MaxElevation = 0,
                    FixedOffset = true,
                    InventorySize = 0.5f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = false,
                        TerminalControls = true,
                        AmmoRound = "", //name of ammo for explosion effect
                    },
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,

            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };

        WeaponDefinition SmallGatlingGun_Gimbal => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SmallGatlingGun_Gimbal",
                        SpinPartId = "GatlingTurretBase2",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
                        DurabilityMod = 0.5f,
                        IconName = "TestIcon.dds",
                    },

                },
                Muzzles = new []
                {
                    "muzzle_projectile",
                },
            },

            Targeting = Ballistics_Gatlings_Targeting_Fixed,

            HardPoint = new HardPointDef
            {
                PartName = "SmallGatling Gimballed", // name of weapon in terminal
                DeviateShotAngle = 0.3f,
                AimingTolerance = 5f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = true,
                    SuppressFire = false,
                    OverrideLeads = false, // Overrides default behavior for target lead
                },				
				
                HardWare = new HardwareDef
                {

                    RotateRate = 0.01f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -15,
                    MaxAzimuth = 15,
                    MinElevation = -15,
                    MaxElevation = 15,
                    FixedOffset = false,
                    HomeAzimuth = 0, // Default resting rotation angle
                    HomeElevation = 15, // Default resting elevation
                    InventorySize = 0.5f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    CriticalReaction = new CriticalDef
                    {
                        Enable = false, // Enables Warhead behaviour
                        DefaultArmedTimer = 120,
                        PreArmed = false,
                        TerminalControls = true,
                        AmmoRound = "", //name of ammo for explosion effect
                    },
                },
				
                Other = Ballistics_Gatlings_Hardpoint_Other,
				
                Loading = Ballistics_Gatlings_Hardpoint_Loading,
                
				Audio = Ballistics_Gatlings_Hardpoint_Audio,
				
                Graphics = Ballistics_Gatlings_Hardpoint_Graphics,
				
            },

			Ammos = new [] {
                Ballistics_Gatling,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };
    }
}
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

		//Common Definitions
		
		private TargetingDef Lasers_Laser_Targeting_Turret_Large => new TargetingDef {
			Threats = new[]
			{
				Grids, Characters,  // threats percieved automatically without changing menu settings  Grids, Characters, Projectiles, Meteors,
			},
			SubSystems = new[]
			{
				Thrust, Utility, Offense, Power, Production, Jumping, Steering, Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true,
			LockedSmartOnly = false,
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 2500,
			MinTargetDistance = 0,
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private TargetingDef Lasers_Laser_Targeting_Turret_Small => new TargetingDef {
			Threats = new[]
			{
				Grids, Characters, Projectiles,  // threats percieved automatically without changing menu settings  Grids, Characters, Projectiles, Meteors,
			},
			SubSystems = new[]
			{
				Thrust, Utility, Offense, Power, Production, Jumping, Steering, Any, // subsystems the gun targets
			},
			ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
			IgnoreDumbProjectiles = true,
			LockedSmartOnly = false,
			MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
			MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
			MaxTargetDistance = 1500,
			MinTargetDistance = 0,
			TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
			TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
			StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
		};

		private LoadingDef Lasers_Laser_Loading_Large => new LoadingDef {
			RateOfFire = 3600,
			BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
			BarrelsPerShot = 1,
			TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
			SkipBarrels = 0,
			ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			HeatPerShot = 2, //heat generated per shot
			MaxHeat = 600, //max heat before weapon enters cooldown (70% of max heat)
			Cooldown = .5f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
			HeatSinkRate = 22, //amount of heat lost per second
			DegradeRof = true, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
			ShotsInBurst = 0,
			DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
			FireFullBurst = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
		};
			
		private HardwareDef Lasers_Laser_Hardware_Fixed => new HardwareDef {
			RotateRate = 0.00f,
			ElevateRate = 0.00f,
			MinAzimuth = 0,
			MaxAzimuth = 0,
			MinElevation = 0,
			MaxElevation = 0,
			FixedOffset = true,
			InventorySize = 0f,
			Offset = Vector(x: 0, y: 0, z: 0),
		};

		private HardPointAudioDef Lasers_Laser_Audio_Large => new HardPointAudioDef {
			PreFiringSound = "", //MediumLaserPreFiring
			FiringSound = "HWR_MediumLaserLoop", // subtype name from sbc
			FiringSoundPerShot = false,
			ReloadSound = "",
			NoAmmoSound = "",
			HardPointRotationSound = "WepTurretGatlingRotate",
			BarrelRotationSound = "",
		};

		private HardPointAudioDef Lasers_Laser_Audio_Small => new HardPointAudioDef {
			PreFiringSound = "",
			FiringSound = "MD_SmallLaserFire", // WepShipGatlingShot
			FiringSoundPerShot = false,
			ReloadSound = "",
			NoAmmoSound = "ArcWepShipGatlingNoAmmo",
			HardPointRotationSound = "WepTurretGatlingRotate",
			BarrelRotationSound = "WepShipGatlingRotation",
			FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
		};

		private HardPointParticleDef Lasers_Laser_Hardpoint_Graphics_Large => new HardPointParticleDef {
			Barrel1 = new ParticleDef
			{
				Name = "", // OKI_230mm_Muzzle_Flash   MXA_SmallCoilgunMuzzleFlash
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,1f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 0,
					Scale = 1.0f,
				}
			},
			Barrel2 = new ParticleDef
			{
				Name = "", // OKI_230mm_Muzzle_Flash   MXA_SmallCoilgunMuzzleFlash
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,1f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 0,
					Scale = 1.0f,
				}
			},
		};

		private HardPointParticleDef Lasers_Laser_Hardpoint_Graphics_Small => new HardPointParticleDef {
			Barrel1 = new ParticleDef
			{
				Name = "", // OKI_230mm_Muzzle_Flash   MXA_SmallCoilgunMuzzleFlash
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,1f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 0,
					Scale = 1.0f,
				}
			},
			Barrel2 = new ParticleDef
			{
				Name = "", // OKI_230mm_Muzzle_Flash   MXA_SmallCoilgunMuzzleFlash
				Color = new Vector4(1f,1f,1f,1f), //RGBA
				Offset = new Vector3D(0f,0f,1f), //XYZ
				Extras = new ParticleOptionDef
				{
					Loop = false,
					Restart = false,
					MaxDistance = 800,
					MaxDuration = 0,
					Scale = 1.0f,
				}
			},
		};

		//Weapon Definitions

        WeaponDefinition MA_PDX_T2 => new WeaponDefinition {	
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "MA_T2PDX",
                        AimPartId = "",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
						DurabilityMod = .5f,
                        IconName = "filter_energy.dds"
                    },
					
                },				
				
                Barrels = new []
                {
                    "muzzle_projectile_001",
                },
				Scope = "T2scope", //Where line of sight checks are performed from must be clear of block collision				
            },

            Targeting = Lasers_Laser_Targeting_Turret_Large,
			
            HardPoint = new HardPointDef
            {
                WeaponName = "XFEL Laser", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 0.1f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 20, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Common_Weapons_Hardpoint_Ui_Damage_Overload,

                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,
				
                HardWare = new HardwareDef
                {
                    RotateRate = 0.015f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -20,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                },
                Other = Common_Weapons_Hardpoint_Other_Large,
				
                Loading = Lasers_Laser_Loading_Large,
				
                Audio = Lasers_Laser_Audio_Large,
				
                Graphics = Lasers_Laser_Hardpoint_Graphics_Large,
            },
       
			Ammos = new [] {
                Lasers_Laser_Large
            },
            //Animations = PDX_Animations,
            // Don't edit below this line
        };

        WeaponDefinition MA_Fixed_T2 => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "MA_Fixed_T2",
                        AimPartId = "",
                        MuzzlePartId = "T2_EL",
                        AzimuthPartId = "T2_AZ1",
                        ElevationPartId = "T2_EL",
						DurabilityMod = 0.5f,
                        IconName = "filter_energy.dds"
                    },
                },				
				
                Barrels = new []
                {
                    "T2_muzzle",
                },
				Scope = "T2scope", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Lasers_Laser_Targeting_Turret_Large,

            HardPoint = new HardPointDef
            {
                WeaponName = "XFEL Laser", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 0.1f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 20, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Common_Weapons_Hardpoint_Ui_Damage_Overload,

                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,

                HardWare = new HardwareDef {
                    RotateRate = 0.01f,
                    ElevateRate = 0.01f,
                    MinAzimuth = -3,
                    MaxAzimuth = 3,
                    MinElevation = -3,
                    MaxElevation = 3,
                    FixedOffset = false,
                    InventorySize = 0f,
                    Offset = Vector(x: 0f, y: 0f, z: 0f),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },

                Other = Common_Weapons_Hardpoint_Other_Large,

                Loading = Lasers_Laser_Loading_Large,
				
                Audio = Lasers_Laser_Audio_Large,
				
                Graphics = Lasers_Laser_Hardpoint_Graphics_Large,
            },
            Ammos = new [] {
                Lasers_Laser_Large,
            },

            // Don't edit below this line
        };

        WeaponDefinition MA_Fixed_T2_Naked => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "MA_Fixed_T2_Naked",
                        AimPartId = "",
                        MuzzlePartId = "T2_EL",
                        AzimuthPartId = "T2_AZ",
                        ElevationPartId = "T2_EL",
						DurabilityMod = 0.5f,
                        IconName = "filter_energy.dds"
                    },
                },				
				
                Barrels = new []
                {
                    "T2_muzzle",
                },
				Scope = "T2scope", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Common_Weapons_Targeting_Fixed_NoTargeting,

            HardPoint = new HardPointDef 
            {
                WeaponName = "XFEL Laser", // name of weapon in terminal
                DeviateShotAngle = 0,
                AimingTolerance = 0, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Common_Weapons_Hardpoint_Ui_Damage_Overload,

                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,

                HardWare = Lasers_Laser_Hardware_Fixed,

                Other = Common_Weapons_Hardpoint_Other_Large,

                Loading = Lasers_Laser_Loading_Large,
				
                Audio = Lasers_Laser_Audio_Large,
				
                Graphics = Lasers_Laser_Hardpoint_Graphics_Large,
            },
            Ammos = new [] {
                Lasers_Laser_Large,
            },

            // Don't edit below this line
        };

		WeaponDefinition ReceptorCoilGun => new WeaponDefinition {

            Assignments = new ModelAssignmentsDef 
            {
                MountPoints = new[] {
                    new MountPointDef {
                        SubtypeId = "ReceptorCoilGun",
                        MuzzlePartId = "None",
                        AzimuthPartId = "None",
                        ElevationPartId = "None",
                        DurabilityMod = 0.5f,
                        IconName = "filter_energy.dds"
                    },
                },
                Barrels = new [] {
					"muzzle_1",
                },
                Ejector = "",
                Scope = "", //Where line of sight checks are performed from must be clear of block collision
            },

            Targeting = Common_Weapons_Targeting_Fixed_NoTargeting,

            HardPoint = new HardPointDef 
            {
                WeaponName = "XFEL Laser", // name of weapon in terminal
                DeviateShotAngle = 0,
                AimingTolerance = 0, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,

                Ui = Common_Weapons_Hardpoint_Ui_Damage_Overload,

                Ai = Common_Weapons_Hardpoint_Ai_BasicFixed_NoTracking,

                HardWare = Lasers_Laser_Hardware_Fixed,

                Other = Common_Weapons_Hardpoint_Other_Small,

                Loading = Lasers_Laser_Loading_Large,

                Audio = Lasers_Laser_Audio_Small,
				
                Graphics = Lasers_Laser_Hardpoint_Graphics_Small,
            },
            Ammos = new [] {
                Lasers_Laser_Small,
            },
            //Animations= Receptor_Emissive
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };

		WeaponDefinition ReceptorTurret => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "ReceptorTurret",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 0.5f,
                        IconName = "filter_energy.dds"
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
                },
            },
            Targeting = Lasers_Laser_Targeting_Turret_Small,
			
            HardPoint = new HardPointDef
            {
                WeaponName = "XFEL Turret", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 30f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                AddToleranceToTracking = false,
                DelayCeaseFire = 15, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = Common_Weapons_Hardpoint_Ui_Damage_Overload,

                Ai = Common_Weapons_Hardpoint_Ai_BasicTurret_LockOn,

                HardWare = new HardwareDef
                {
                    RotateRate = 0.015f,
                    ElevateRate = 0.015f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -18,
                    MaxElevation = 45,
                    FixedOffset = false,
                    InventorySize = 0f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },

                Other = Common_Weapons_Hardpoint_Other_Small,

                Loading = Lasers_Laser_Loading_Large,

                Audio = Lasers_Laser_Audio_Small,
				
                Graphics = Lasers_Laser_Hardpoint_Graphics_Small,
            },
            Ammos = new [] {
                Lasers_Laser_Small,
            },
            //Animations= Receptor_Emissive
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
    }
}
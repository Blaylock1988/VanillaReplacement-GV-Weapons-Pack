﻿using System.Collections.Generic;
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
		// Don't edit above this line
        WeaponDefinition MA_PDT => new WeaponDefinition {	
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "MA_PDT",
                        AimPartId = "MissileTurretBarrels",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
						DurabilityMod = 0.25f,
                        IconName = "filter_energy.dds" 
					},
				    new MountPointDef {
                        SubtypeId = "MA_PDT_sm",
                        AimPartId = "MissileTurretBarrels",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
						DurabilityMod = 0.25f,
                        IconName = "filter_energy.dds"
                    },

                },
                Barrels = new []
                {
                    "muzzle_missile_001",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Projectiles,// threats percieved automatically without changing menu settings  Grids, Characters, Projectiles, Meteors,
                },
                SubSystems = new[]
                {
                    Any, // subsystems the gun targets
                },
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 1250, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 10, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 8, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
			},
            HardPoint = new HardPointDef
            {
                WeaponName = "PDT", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 0.5f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 5, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = false,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload =  false,
                },
                Ai = new AiDef 
				{
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = false,
                    LockOnFocus = false,
                    SuppressFire = false,
                    OverrideLeads = false, // Override default behavior for target leads
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.15f,
                    ElevateRate = 0.15f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -20,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 0f,
                    Offset = Vector(x: 0, y: 0, z: 0),
                    Armor = IsWeapon, // IsWeapon, Passive, Active
                },
                Other = new OtherDef {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                    RestrictionRadius = 7.5, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
				},
                Loading = new LoadingDef
                {
                    RateOfFire = 3600,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 6, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 2, //heat generated per shot
                    MaxHeat = 240, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0.99f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 5, //amount of heat lost per second
                    DegradeRof = true, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "PDTFiringSound", // subtype name from sbc
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "",
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 600,
                            MaxDuration = 0,
                            Scale = 1.0f,
                        },
                    },
                    Barrel2 = new ParticleDef
                    {
                        Name = "PDTFlash",//Muzzle_Flash_Large
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: -0.25),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 600,
                            MaxDuration = 0,
                            Scale = 1f,
                        },
                    },
                },
            },
       
			Ammos = new [] {
                MA_Laser_3,
            },
            //Animations = AdvancedAnimation,
            // Don't edit below this line
        };		

    }
}

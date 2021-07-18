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
		// Don't edit above this line
        WeaponDefinition GV_JDDT => new WeaponDefinition
        {	
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef {
                        SubtypeId = "JumpDriveDisruptorTurret",
                        AimPartId = "",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
						DurabilityMod = 0.5f,
                        IconName = ""// filter_energy.dds
                    },
 

                    new MountPointDef {
                        SubtypeId = "JumpDriveDisruptorTurretSM",
                        AimPartId = "",
                        MuzzlePartId = "GatlingBarrel",
                        AzimuthPartId = "GatlingTurretBase1",
                        ElevationPartId = "GatlingTurretBase2",
						DurabilityMod = 0.5f,
                        IconName = ""
                    },				
					
                },				
				
                Barrels = new []
                {
                    "muzzle_projectile_001",
					
                },
            },
            Targeting = new TargetingDef  
            {
                Threats = new[] {
                    Grids,
                },
                SubSystems = new[] {
                    Any,
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = true, // Don't fire at non-smart projectiles.
                LockedSmartOnly = false, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 1500, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 5, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 10, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "GV Disruptor Turret", // name of weapon in terminal
                DeviateShotAngle = 0f,
                AimingTolerance = 5f, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AddToleranceToTracking = false,
                CanShootSubmerged = false,
				
                Ui = new UiDef
                {
                    RateOfFire = true,
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
                    LockOnFocus = true,
                    SuppressFire = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.03f,
                    ElevateRate = 0.02f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -20,
                    MaxElevation = 90,
                    FixedOffset = false,
                    InventorySize = 4.5f,
                    Offset = Vector(x: 0, y: 0, z: 0),
					Armor = IsWeapon, // IsWeapon, Passive, Active
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 3,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                    RestrictionRadius = 2.5, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = true, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 3600,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 1, //10 heat generated per shot
                    MaxHeat = 240, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0.5f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 10, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    BarrelSpinRate = 500, // visual only, 0 disables and uses RateOfFire
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
				},
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "TurretLaserFire", // subtype name from sbc
                    FiringSoundPerShot = false,
                    ReloadSound = "WebberPreFire",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WebberSpinSound",
					FireSoundEndDelay = 0, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef
                    {
						Name = "MA_laserhit_reversed",
						Color = Color(red: 1, green: 3, blue: 5, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0f),
						Extras = new ParticleOptionDef
						{
							Loop = true,
							Restart = false,
							MaxDistance = 800, //meters
							MaxDuration = 0, //ticks 60 = 1 second
							Scale = 5f,
						}
                    },
                },
            },
       
			Ammos = new [] {

                Lasers_JumpNull,
				Lasers_JumpNull_Pattern,
				
            },
            // Animations = JDDT_Animations,
            // Don't edit below this line
        };		
    }
}
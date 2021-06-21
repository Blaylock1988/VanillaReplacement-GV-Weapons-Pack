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
		
		WeaponDefinition Lotus => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "Lotus",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_1",
					"muzzle_missile_2",
					"muzzle_missile_3",
					"muzzle_missile_4",
					"muzzle_missile_5",
					"muzzle_missile_6",
					"muzzle_missile_7",
					"muzzle_missile_8",
					"muzzle_missile_9",
					"muzzle_missile_10",
					"muzzle_missile_11",
					"muzzle_missile_12",
					"muzzle_missile_13",
					"muzzle_missile_14",
					"muzzle_missile_15",
					"muzzle_missile_16",
					"muzzle_missile_17",
					"muzzle_missile_18",
					"muzzle_missile_19",
					"muzzle_missile_20",
					"muzzle_missile_21",
					"muzzle_missile_22",
					"muzzle_missile_23",
					"muzzle_missile_24",
					"muzzle_missile_25",
					"muzzle_missile_26",
					"muzzle_missile_27",
					"muzzle_missile_28",
					"muzzle_missile_29",
					"muzzle_missile_30",
					"muzzle_missile_31",
					"muzzle_missile_32",
					"muzzle_missile_33",
					"muzzle_missile_34",
					"muzzle_missile_35",
					"muzzle_missile_36",
					"muzzle_missile_37",
					"muzzle_missile_38",
					"muzzle_missile_39",
					"muzzle_missile_40",
					"muzzle_missile_41",
					"muzzle_missile_42"
					
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Projectiles, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 2000,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Lotus Mine Layer", // name of weapon in terminal
                DeviateShotAngle = 10f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

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
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -20,
                    MaxElevation = 20,
                    FixedOffset = false,
                    InventorySize = 3.24f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 2,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 2,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 500,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 2100, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 1200, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .16667f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 300, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "Sigma11Launch", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "LotusReload",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "AkiadMuzzle", // Smoke_LargeGunShot
                        Color = Color(red: 20, green: 20, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                    Barrel2 = new ParticleDef //FIXTHIS
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 5, green: 40, blue: 40, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 150,
                            MaxDuration = 6,
                            Scale = 1f,
                        },
                    },
                },
            },

            Ammos = new[] { LotusMissile1,LotusMine1,LotusMissile2,LotusMine2,LotusMissile3,LotusMine3},
             Animations= GetLotusMissile()
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
				
		
    }
}
using VRageMath;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.ModelAssignmentsDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.Prediction;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.BlockTypes;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.Threat;

namespace WeaponThread
{   // Don't edit above this line
    partial class Weapons
    {
        WeaponDefinition PGIFlareLauncherLarge => new WeaponDefinition
        {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "PGIFlareLauncherLarge",
                        AimPartId = "None",
                        MuzzlePartId = "None",
						AzimuthPartId = "None",
						ElevationPartId = "None",
                    },
                },
                Barrels = new []
                {
                    "subpart_barrel_001",
                    "subpart_barrel_002",
                    "subpart_barrel_003",
                    "subpart_barrel_004",
                    "subpart_barrel_005",
                    "subpart_barrel_006",
                    "subpart_barrel_007",
                    "subpart_barrel_008",
                    "subpart_barrel_009",
                    "subpart_barrel_010",
                    "subpart_barrel_011",
                    "subpart_barrel_012",
                    "subpart_barrel_013",
                    "subpart_barrel_014",
                    "subpart_barrel_015",
                    "subpart_barrel_016",
                    "subpart_barrel_017",
                    "subpart_barrel_018",
                    "subpart_barrel_019",
                    "subpart_barrel_020",
                    "subpart_barrel_021",
                    "subpart_barrel_022",
                    "subpart_barrel_023",
                    "subpart_barrel_024",
                    "subpart_barrel_025",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Projectiles,
                },
                SubSystems = new[]
                {
                    Any,
                },
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                IgnoreDumbProjectiles = false, // Don't fire at non-smart projectiles.
                LockedSmartOnly = true, // Only fire at smart projectiles that are locked on to parent grid.
                MinimumDiameter = 0, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                MaxTargetDistance = 3000, // 0 = unlimited, Maximum target distance that targets will be automatically shot at.
                MinTargetDistance = 0, // 0 = unlimited, Min target distance that targets will be automatically shot at.
                TopTargets = 0, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 0, // 0 = unlimited, max number of blocks to randomize between
                StopTrackingSpeed = 0, // do not track target threats traveling faster than this speed
			},
            HardPoint = new HardPointDef
            {
                WeaponName = "Flare Launcher", // name of weapon in terminal
                DeviateShotAngle = 8f,
                AimingTolerance = 180, // 0 - 180 firing angle
                AimLeadingPrediction = Off, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 10, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = false,
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
                    SuppressFire = false,
                    OverrideLeads = false, // Override default behavior for target leads
				},
                HardWare = new HardwareDef
                {
                    RotateRate = 1f,
                    ElevateRate = 1f,
                    MinAzimuth = 0,
                    MaxAzimuth = 360,
                    MinElevation = 0,
                    MaxElevation = 360,
                    FixedOffset = false,
                    InventorySize = 1.25f,
                    Offset = Vector(x: 0, y: 0, z: -2),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = true,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 500,
					BarrelSpinRate = 0,
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 600, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 0, //heat generated per shot
                    MaxHeat = 0, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = 0f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 0, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 5,
                    DelayAfterBurst = 15, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = true,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "FlareShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "FlareReload",
                    NoAmmoSound = "",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef
                    {
                        Name = "", // Smoke_LargeGunShot
                        Color = Color(red: 10, green: 10, blue: 10, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 200,
                            MaxDuration = 30,
                            Scale = 0.5f,
                        },
                    },
                    Barrel2 = new ParticleDef
                    {
                        Name = "",//Muzzle_Flash_Large
                        Color = Color(red: 10, green: 10, blue: 10, alpha: 10),
                        Offset = Vector(x: 0, y: -2, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 50,
                            MaxDuration = 4,
                            Scale = 5f,
                        },
                    },
                },
            },

            Ammos = new [] {
                FlareLarge,
            },
			Animations = PGIFlareLauncherLarge_Anim,
        };
    }
}
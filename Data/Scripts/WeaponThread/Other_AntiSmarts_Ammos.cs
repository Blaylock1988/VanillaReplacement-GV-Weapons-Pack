using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AmmoEjectionDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AmmoEjectionDef.SpawnType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef.EwarFieldsDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef.EwarFieldsDef.PushPullDef.Force;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.AreaDamageDef.AreaEffectType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static WeaponThread.WeaponStructure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
namespace WeaponThread
{ // Don't edit above this line
    partial class Weapons
    {
		private AmmoDef LotusMissile1 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "AMS", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "LotusMine1",
					Fragments = 2, //Number of shrapnel projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 8000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine1 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMine1", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of shrapnel projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = AntiSmart, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 50f, // the sphere of influence of area effects
							EffectStrength = 100f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = true,
							ArmOnlyOnHit = false,
							DetonationDamage = 7500f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect shrapnel spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 25f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus2", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMissile2 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Repel", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "LotusMineRepel",
					Fragments = 2, //Number of shrapnel projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 1000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine2 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMineRepel", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of shrapnel projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = PushField, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 50f, // the sphere of influence of area effects
							EffectStrength = 100000f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = true,
							ArmOnlyOnHit = false,
							DetonationDamage = 7500f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect shrapnel spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileOrigin, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 5f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMissile3 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Explosive", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "LotusMineExp",
					Fragments = 2, //Number of shrapnel projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 1000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine3 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMineExp", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Shrapnel = new ShrapnelDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of shrapnel projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Kinetic,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 25f, // the sphere of influence of area effects
							EffectStrength = 1000f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = false,
							ArmOnlyOnHit = false,
							DetonationDamage = 100f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect shrapnel spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileOrigin, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 25f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus3", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
    }
}
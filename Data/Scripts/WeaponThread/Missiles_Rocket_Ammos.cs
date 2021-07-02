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
        private AmmoDef Missiles_Rocket => new AmmoDef
        {
            
			AmmoMagazine = "Missile200mm",
			AmmoRound = "Missiles_Rocket",
			HybridRound = false, //AmmoMagazine based weapon with energy cost
			EnergyCost = 0f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
			BaseDamage = 1f,
			Mass = 1000f, // in kilograms
			Health = 4, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
			BackKickForce = 5f,
			HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
			EnergyMagazineSize = 0,
			IgnoreWater = false,

			Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
			{
				Shape = LineShape,
				Diameter = 1,
			},
			DamageScales = new DamageScaleDef
			{
				MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
				DamageVoxels = false, // true = voxels are vulnerable to this weapon
				SelfDamage = false, // true = allow self damage.
				HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
				VoxelHitModifier = -1,
				// modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
				Characters = -1f,
				Grids = new GridSizeDef
				{
					Large = -1f,
					Small = 0.25f,
				},
				Armor = new ArmorDef
				{
					Armor = -1f,
					Light = -1f,
					Heavy = -1f,
					NonArmor = -1f,
				},
				Shields = new ShieldDef
				{
					Modifier = -1f,
					Type = Kinetic,
					BypassModifier = -1f,
				},
				// first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
			},
			AreaEffect = new AreaDamageDef
			{
				AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
				Base = new AreaInfluence
				{
					Radius = 10f, // the sphere of influence of area effects, must be at least 1 for missiles
					EffectStrength = 1500f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
				},
				Explosions = new ExplosionDef
				{
					NoVisuals = false,
					NoSound = false,
					NoShrapnel = false,
					NoDeformation = false,
					Scale = 1,
					CustomParticle = "",
					CustomSound = "",
				},
				Detonation = new DetonateDef
				{
					DetonateOnEnd = false,
					ArmOnlyOnHit = false,
					DetonationDamage = 0,
					DetonationRadius = 0,
					MinArmingTime = 10, //Min time in ticks before projectile will arm for detonation (will also affect shrapnel spawning)
				},
			},
			Trajectory = new TrajectoryDef
			{
				Guidance = Smart,
				TargetLossDegree = 1f,
				TargetLossTime = 1, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
				MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
				AccelPerSec = 800f,
				DesiredSpeed = 800,
				MaxTrajectory = 1250f,
				FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				SpeedVariance = Random(start: 0, end: 100), // subtracts value from DesiredSpeed
				RangeVariance = Random(start: 0, end: 50), // subtracts value from MaxTrajectory
				Smarts = new SmartsDef
				{
					Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
					Aggressiveness = 3f, // controls how responsive tracking is.
					MaxLateralThrust = 0.5f, // controls how sharp the trajectile may turn
					TrackingDelay = 250, // Measured in Shape diameter units traveled.
					MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
					OffsetRatio = 0.25f, // The ratio to offset the random dir (0 to 1) 
					OffsetTime = 60, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..)
				},
				Mines = new MinesDef
				{
					DetectRadius =  0,
					DeCloakRadius = 0,
					FieldTime = 0,
					Cloak = false,
					Persist = false,
				},
			},
			AmmoGraphics = new GraphicDef
			{
				ModelName = "\\Models\\Missiles\\MXA_Archer_Missile.mwm",
				VisualProbability = 1f,
				ShieldHitDraw = true,
				Particles = new AmmoParticleDef
				{
                    Hit = new ParticleDef
                    {
                        Name = "MD_HydraRocketExplosion", //MD_HydraRocketExplosion MD_InstallationExplosion
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 0,
                            Scale = 1f,
                            HitPlayChance = 1f,
                        },
                    },
				},
				Lines = new LineDef
				{
                    ColorVariance = Random(start: 0f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.1f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f,
                        Width = 0.3f,
                        Color = Color(red: 30f, green: 22f, blue: 1.5f, alpha: 1f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "MD_MissileThrustFlame",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                    },
					Trail = new TrailDef
					{
						Enable = true,
						Textures = new[] {
							"WeaponLaser",
						},
						TextureMode = Normal,
						DecayTime = 100,
						Color = Color(red: 1, green: 1, blue: 1, alpha: 1f),
						Back = false,
						CustomWidth = 1f,
						UseWidthVariance = true,
						UseColorFade = true,
					},
				},
			},
			AmmoAudio = new AmmoAudioDef
			{
				TravelSound = "MXA_Archer_Travel",
				HitSound = "HWR_LargeExplosion",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 1f,
                HitPlayShield = true,
			}, // Don't edit below this line
        };
    }
}

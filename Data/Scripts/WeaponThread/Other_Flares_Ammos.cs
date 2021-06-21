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
		private AmmoDef FlareLarge => new AmmoDef {
			AmmoMagazine = "PGI_LargeFlareBox",
			AmmoRound = "FlareLarge",
			HybridRound = false, //AmmoMagazine based weapon with energy cost
			EnergyCost = 0f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
			BaseDamage = 25f,
			Mass = 1f, // in kilograms
			Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
			BackKickForce = 0f,
			HardPointUsable = true, // set to false if this is a shrapnel ammoType and you don't want the turret to be able to select it directly.
			EnergyMagazineSize = 25,

			Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
			{
				Shape = LineShape,
				Diameter = 0.1,
			},
			ObjectsHit = new ObjectsHitDef
			{
				MaxObjectsHit = 0, // 0 = disabled
				CountBlocks = true, // counts gridBlocks and not just entities hit
			},
			Shrapnel = new ShrapnelDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
			Pattern = new AmmoPatternDef
            {
                Ammos = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
            },
			DamageScales = new DamageScaleDef
			{
				MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
				DamageVoxels = false, // true = voxels are vulnerable to this weapon
				SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 4, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).

				// modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
				Characters = -1f,
				FallOff = new FallOffDef
                {
                    Distance = 0f, // Distance at which max damage begins falling off.
                    MinMultipler = 1f, // value 1f - 0.00001f.. where 0.1f == 10% max damage.
                },
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
					NonArmor = -1f,
				},
				Shields = new ShieldDef
				{
					Modifier = 5f,
					Type = Energy,
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
				AreaEffectDamage = 1f, // 0 = use spillover from BaseDamage, otherwise use this value.
				AreaEffectRadius = 1000f,
				Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
				{
					Interval = 60,
					PulseChance = 50,
				},
				Explosions = new ExplosionDef
				{
					NoVisuals = false,
					NoSound = false,
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
				},
                EwarFields = new EwarFieldsDef
                {
                    Duration = 0,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
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
				Guidance = None,
				TargetLossDegree = 360f,
				TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
				MaxLifeTime = 420, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
				AccelPerSec = 0f,
				DesiredSpeed = 100,
				MaxTrajectory = 3000f,
				FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0.75f,
				SpeedVariance = Random(start: 0, end: 20), // subtracts value from DesiredSpeed
				RangeVariance = Random(start: 0, end: 50), // subtracts value from MaxTrajectory
				Smarts = new SmartsDef
				{
					Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
					Aggressiveness = 1f, // controls how responsive tracking is.
					MaxLateralThrust = 0.5f, // controls how sharp the trajectile may turn
					TrackingDelay = 0, // Measured in Shape diameter units traveled.
					MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
					MaxTargets = 1,
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
				ModelName = "",
				VisualProbability = 1f,
				ShieldHitDraw = true,
				Particles = new AmmoParticleDef
				{
					Ammo = new ParticleDef
					{
						Name = "MD_FlareSparks", //NewFlare
						ShrinkByDistance = false,
						Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
						Offset = Vector(x: 0, y: 0, z: 0),
						Extras = new ParticleOptionDef
						{
							Loop = true,
							Restart = false,
							MaxDistance = 5000,
							MaxDuration = 0,
							Scale = 1f,
						},
					},
					Hit = new ParticleDef
					{
						Name = "",
						ApplyToShield = false,
						ShrinkByDistance = false,
						Color = Color(red: 20, green: 20, blue: 40, alpha: 1),
						Offset = Vector(x: 0, y: 0, z: 0),
						Extras = new ParticleOptionDef
						{
							Loop = true,
							Restart = false,
							MaxDistance = 5000,
							MaxDuration = 10,
							Scale = 0.03f,
							HitPlayChance = 1f,
						},
					},
				},
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0f, end: 30f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.01f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.5f,
                        Color = Color(red: 50f, green: 40f, blue: 30f, alpha: 1f),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "ProjectileTrailLine",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
                                "",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 0.5f),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Textures = new[] {
                            "WeaponLaser",
                        },
                        TextureMode = Normal,
                        DecayTime = 200,
                        Color = Color(red: 1.1f, green: 1.05f, blue: 1, alpha: 1f),
                        Back = false,
                        CustomWidth = 1.5f,
                        UseWidthVariance = true,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
			AmmoAudio = new AmmoAudioDef
			{
				TravelSound = "FlareLoop",
				HitSound = "",
				HitPlayChance = 1f,
				HitPlayShield = true,
			}, // Don't edit below this line
		};
		
	}
}
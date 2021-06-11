using System.Collections.Generic;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.PartAnimationSetDef.EventTriggers;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.RelMove.MoveType;
using static WeaponThread.WeaponStructure.WeaponDefinition.AnimationDef.RelMove;
namespace WeaponThread
{ // Don't edit above this line
    partial class Weapons
    {
        /// Possible Events ///
        
        //Reloading,
        //Firing,
        //Tracking,
        //Overheated,
        //TurnOn,
        //TurnOff,
        //BurstReload,
        //OutOfAmmo,
        //PreFire,
        //EmptyOnGameLoad,
        //StopFiring,
        //StopTracking

        private AnimationDef AdvancedAnimation => new AnimationDef
        {
            /*
            Emissives = new[]
            {
                Emissive(
                    EmissiveName: "TurnOn",
                    Colors: new []
                    {
                        Color(red:0, green: 0, blue:0, alpha: 1),//will transitions form one color to the next if more than one
                        Color(red:0.05f, green: .021f, blue:0, alpha: .05f),
                    },
                    IntensityFrom:0, //starting intensity, can be 0.0-1.0 or 1.0-0.0, setting both from and to, to the same value will stay at that value
                    IntensityTo:1,
                    CycleEmissiveParts: false,//whether to cycle from one part to the next, while also following the Intensity Range, or set all parts at the same time to the same value
                    LeavePreviousOn: true,//true will leave last part at the last setting until end of animation, used with cycleEmissiveParts
                    EmissivePartNames: new []
                    {
                        "Emissive3"
                    }),
                Emissive(
                    EmissiveName: "TurnOff",
                    Colors: new []
                    {
                        Color(red:0.05f, green: .021f, blue:0, alpha: .05f),
                        Color(red:0, green: 0, blue:0, alpha: 1),//will transitions form one color to the next if more than one
                    },
                    IntensityFrom:1, //starting intensity, can be 0.0-1.0 or 1.0-0.0, setting both from and to, to the same value will stay at that value
                    IntensityTo:0,
                    CycleEmissiveParts: false,//whether to cycle from one part to the next, while also following the Intensity Range, or set all parts at the same time to the same value
                    LeavePreviousOn: true,//true will leave last part at the last setting until end of animation, used with cycleEmissiveParts
                    EmissivePartNames: new []
                    {
                        "Emissive3"
                    }),
            },
            */
            EventParticles = new Dictionary<PartAnimationSetDef.EventTriggers, EventParticle[]>
            {
                [Firing] = new[]{
                    new EventParticle
                    {
                        EmptyNames = Names("muzzle_barrel_001", "muzzle_barrel_002", "muzzle_barrel_003", "muzzle_barrel_004", "muzzle_barrel_005", "muzzle_barrel_006"),
                        MuzzleNames = Names("muzzle_barrel_001", "muzzle_barrel_002", "muzzle_barrel_003", "muzzle_barrel_004", "muzzle_barrel_005", "muzzle_barrel_006"),
                        StartDelay = 0, //ticks 60 = 1 second
                        LoopDelay = 0, //ticks 60 = 1 second
                        ForceStop = false,
                        Particle = new ParticleDef
                        {
                            Name = "ShipWelderArc",
                            Color = Color(red: 20, green: 20, blue: 20, alpha: 1),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 200, //meters
                                MaxDuration = 200, //ticks 60 = 1 second
                                Scale = 1,
                            }
                        }
                    },
                },
            },
            /*
            WeaponAnimationSets = new[]
            {
                new PartAnimationSetDef
                {
                    SubpartId = Names("part1"),
                    BarrelId = "barrel_001", //only used for firing events, use "Any" for all muzzles, muzzle triggers only this animation if not Any
                    AnimationDelays = Delays(FiringDelay : 0, ReloadingDelay: 0, OverheatedDelay: 0, TrackingDelay: 0, LockedDelay: 0, OnDelay: 0, OffDelay: 0, BurstReloadDelay: 0, OutOfAmmoDelay: 0, PreFireDelay: 0),//Delay before animation starts
                    Reverse = Events(),
                    Loop = Events(),
                    EventMoveSets = new Dictionary<PartAnimationSetDef.EventTriggers, RelMove[]>
                    {
                        [Firing] = new[] //Firing, Reloading, etc, see Possible Events,  define a new[] for each
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 1, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Hide,
                                    LinearPoints = new[]
                                    {
                                        Transformation(1, -1, 1), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees, rotation is around CenterEmpty
                                },
                            },
                        [Reloading] = new[] //Firing, Reloading, etc, see Possible Events,  define a new[] for each
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 100, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Delay,
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, 0), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees, rotation is around CenterEmpty
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 1, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Show,
                                    LinearPoints = new XYZ[0], // us this if no linear motion, ex Delay, Hide, Show, Rotations only etc
                                    Rotation = Transformation(10, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 10, 0), //degrees
                                },
                            },
                    }
                },
                new PartAnimationSetDef
                {
                    SubpartId = Names("part2"),
                    BarrelId = "barrel_002", //only used for firing events, use "Any" for all muzzles, muzzle triggers only this animation if not Any
                    AnimationDelays = Delays(FiringDelay : 0, ReloadingDelay: 0, OverheatedDelay: 0, TrackingDelay: 0, LockedDelay: 0, OnDelay: 0, OffDelay: 0, BurstReloadDelay: 0, OutOfAmmoDelay: 0, PreFireDelay: 0),//Delay before animation starts
                    Reverse = Events(),
                    Loop = Events(),
                    EventMoveSets = new Dictionary<PartAnimationSetDef.EventTriggers, RelMove[]>
                    {
                        [Firing] = new[] //Firing, Reloading, etc, see Possible Events,  define a new[] for each
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 1, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Hide,
                                    LinearPoints = new[]
                                    {
                                        Transformation(1, -1, 1), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees, rotation is around CenterEmpty
                                },
                            },
                        [Reloading] = new[] //Firing, Reloading, etc, see Possible Events,  define a new[] for each
                            {
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 100, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Delay,
                                    LinearPoints = new[]
                                    {
                                        Transformation(0, 0, 0), //linear movement
                                    },
                                    Rotation = Transformation(0, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 0, 0), //degrees, rotation is around CenterEmpty
                                },
                                new RelMove
                                {
                                    CenterEmpty = "",
                                    EmissiveName = "", //EmissiveName: from above Emissives definitions, TurnOn TurnOff
                                    TicksToMove = 1, //number of ticks to complete motion, 60 = 1 second
                                    MovementType = Show,
                                    LinearPoints = new XYZ[0], // us this if no linear motion, ex Delay, Hide, Show, Rotations only etc
                                    Rotation = Transformation(10, 0, 0), //degrees
                                    RotAroundCenter = Transformation(0, 10, 0), //degrees, rotation is around CenterEmpty
                                },
                            },
                    }
                },
            }
            */
        };
    }
}

using AutoMapper;
using AutoMapper;
using Castle.DynamicProxy.Generators;
using Domain.Config;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using Models.Config.Simulation;
using Models.Simulations;
using Newtonsoft.Json;
using Services.Configs;
using Services.Identity;
using SimulationInputValues;
using SimulationOutPutValues;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers
{
    [Authorize]
    public partial class SimulationController : BaseController
    {
        private readonly ISimulationService simulationService;
        private readonly IConfigService configService;
        private readonly IAuthService authService;
        private readonly ISimulationUserService simulationUserService;

        public SimulationController(IMapper mapper, IConfigService configService, IUnitService unitService, ISimulationService simulationService = null,
            IAuthService authService = null, ISimulationUserService simulationUserService = null) : base(mapper)
        {
            this.simulationService = simulationService;
            this.authService = authService;
            this.configService = configService;
            this.simulationUserService = simulationUserService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(SimulationModel), StatusCodes.Status200OK)]

        public IActionResult Add(SimulationModel model)
        {
            var dbSimulation = Mapper.Map<Simulation>(model);
            dbSimulation.IsRunning = true;
            
            var dbConfig = JsonConvert.DeserializeObject<ConfigDetails>(configService.GetById(model.ConfigId).ConfigDetails);
            #region Converting Config to Simulation Json
            var simulationFields = new SimulationFeilds
            {                
                InputValues = new InputRoot
                {
                    status=1,
                    speed = 1, // speed > 0 to start
                    Configuration = new Configuration
                    {
                        Pumps = new Pumps
                        {
                            //cement pump
                            CementPumpLinerDiameter = dbConfig.CementPump.PumpLinerDiameter,
                            CementPumpMaximum = dbConfig.CementPump.MaximumStroke,
                            CementPumpMechanicalEfficiency = dbConfig.CementPump.MechanicalEfficiency,
                            CementPumpOutput = dbConfig.CementPump.VolumetricOutput,
                            CementPumpOutputBblStroke = dbConfig.CementPump.PumpOutputBblStroke,
                            CementPumpStroke = dbConfig.CementPump.MaximumStroke,//?
                            CementPumpReliefValveIsSet = true,
                            CementPumpReliefValvePressure = dbConfig.CementPump.ReliefValvePressure,
                            CementPumpVolumetricEfficiency = dbConfig.CementPump.VolumetricEfficiency,
                            CementPumpDelayToShutdown = dbConfig.CementPump.DelayToShutdown,
                            CementPumpMaximumPressure = dbConfig.CementPump.Pressure,
                            CementPumpPumpRateChange= dbConfig.CementPump.PumpRateChange,
                            CementPumpSurfaceLineLength= dbConfig.CementPump.SurfaceLineLenght,
                            CementPumpVolumetricOutput= dbConfig.CementPump.VolumetricOutput,
                            /////////////////////////////
                            ///mud pump 1:
                            MudPump1LinerDiameter = dbConfig.MudPump1.PumpLinerDiameter,
                            MudPump1Maximum = dbConfig.MudPump1.MaximumStroke,
                            MudPump1MechanicalEfficiency = dbConfig.MudPump1.MechanicalEfficiency,
                            MudPump1Output = dbConfig.MudPump1.VolumetricOutput,
                            MudPump1OutputBblStroke = dbConfig.MudPump1.PumpOutputBblStroke,
                            MudPump1Stroke = dbConfig.MudPump1.MaximumStroke,//?
                            MudPump1ReliefValveIsSet = true,
                            MudPump1ReliefValvePressure = dbConfig.MudPump1.Pressure,
                            MudPump1VolumetricEfficiency = dbConfig.MudPump1.VolumetricEfficiency,
                            MudPump1DelayToShutdown = dbConfig.MudPump1.DelayToShutdown,
                            MudPump1MaximumPressure = dbConfig.MudPump1.Pressure,
                            MudPump1PumpRateChange = dbConfig.MudPump1.PumpRateChange,
                            MudPump1SurfaceLineLength = dbConfig.MudPump1.SurfaceLineLenght,
                            MudPump1VolumetricOutput = dbConfig.MudPump1.VolumetricOutput,
                            ////////////////////////////
                            ///mud pump 2:
                            MudPump2LinerDiameter = dbConfig.MudPump2.PumpLinerDiameter,
                            MudPump2Maximum = dbConfig.MudPump2.MaximumStroke,
                            MudPump2MechanicalEfficiency = dbConfig.MudPump2.MechanicalEfficiency,
                            MudPump2Output = dbConfig.MudPump2.VolumetricOutput,
                            MudPump2OutputBblStroke = dbConfig.MudPump2.PumpOutputBblStroke,
                            MudPump2Stroke = dbConfig.MudPump2.MaximumStroke,//?
                            MudPump2ReliefValveIsSet = true,
                            MudPump2ReliefValvePressure = dbConfig.MudPump2.Pressure,
                            MudPump2VolumetricEfficiency = dbConfig.MudPump2.VolumetricEfficiency,
                            MudPump2DelayToShutdown = dbConfig.MudPump2.DelayToShutdown,
                            MudPump2MaximumPressure = dbConfig.MudPump2.Pressure,
                            MudPump2PumpRateChange = dbConfig.MudPump2.PumpRateChange,
                            MudPump2SurfaceLineLength = dbConfig.MudPump2.SurfaceLineLenght,
                            MudPump2VolumetricOutput = dbConfig.MudPump2.VolumetricOutput,
                            /////////////////////////
                            ///
                            ManualPumpPower = false,
                            Valve1 = false,//not used
                            Valve2 = false,//not used
                            Valve3 = false,//not used
                            Valve4 = false,//not used
                            Valve5 = false,//not used
                            /////////////////////////end of pumps
                        },
                        BopStack = new BopStack
                        {
                            AboveAnnularHeight = 10,
                            AnnularPreventerHeight = 10,
                            AnnularPreventerClose = dbConfig.BopModel.AnnularPreventer.Close,
                            AnnularPreventerOpen = dbConfig.BopModel.AnnularPreventer.Open,
                            AnnularStringDrag = dbConfig.BopModel.AnnularStringDrag,
                            BlindRamClose = dbConfig.BopModel.BlindShearRams.Close,
                            BlindRamOpen = dbConfig.BopModel.BlindShearRams.Open,
                            BlindRamHeight = 16.24,
                            KillClose = dbConfig.BopModel.KillChoke.Close,
                            KillOpen = dbConfig.BopModel.KillChoke.Open,
                            KillHeight = 18.8,
                            LowerRamClose = dbConfig.BopModel.LowerRams.Close,
                            LowerRamOpen = dbConfig.BopModel.LowerRams.Open,
                            LowerRamHeight = 21.35,
                            ChokeLineLength = dbConfig.ChockeModel.ChokeLineLength,
                            GroundLevel = 30,
                            RamStringDrag = dbConfig.BopModel.RamStringDrag,
                            ChokeLineId = dbConfig.ChockeModel.ChokeLineId,
                            ChokeClose = dbConfig.BopModel.KillChoke.Close,
                            ChokeOpen = dbConfig.BopModel.KillChoke.Open,
                            UpperRamClose = dbConfig.BopModel.UpperRams.Close,
                            UpperRamOpen = dbConfig.BopModel.UpperRams.Open,
                            UpperRamHeight = 14.632,
                        },
                        Accumulator = new Accumulator
                        {
                            AccumulatorMinimumOperatingPressure = dbConfig.BopControlSystemModel.AccumulatorMinimumOperatingPressure,
                            AccumulatorSystemSize = dbConfig.BopControlSystemModel.AccumulatorSystemSize,
                            AirPlungerPumpOutput = dbConfig.BopControlSystemModel.AccumulatorSystemSize,
                            ElectricPumpOutput = dbConfig.BopControlSystemModel.ElectricPumpOutput,
                            NumberOfBottels = dbConfig.BopControlSystemModel.NumberOfBottels,
                            OilTankVolume = dbConfig.BopControlSystemModel.OilTankVolume,
                            PrechargePressure = dbConfig.BopControlSystemModel.PrechargePressure,
                            StartPressure = dbConfig.BopControlSystemModel.StartPressure,
                            StartPressure2 = dbConfig.BopControlSystemModel.StartPressure2000Psi,
                            StopPressure = dbConfig.BopControlSystemModel.StopPressure,
                            StopPressure2 = dbConfig.BopControlSystemModel.StopPressure2000Psi
                        },
                        CasingLinerChoke = new CasingLinerChoke
                        {
                            CasingCollapsePressure = dbConfig.CasingLiner.CasingCollapsePressure,
                            CasingDepth = dbConfig.CasingLiner.CasingDepth,
                            CasingId = dbConfig.CasingLiner.CasingId,
                            CasingOd = dbConfig.CasingLiner.CasingOd,
                            CasingTensileStrength = dbConfig.CasingLiner.CasingTensileStrength,
                            CasingWeight = dbConfig.CasingLiner.CasingWeight,
                            LinerCollapsePressure = dbConfig.CasingLiner.LinerCollapsePressure,
                            LinerId = dbConfig.CasingLiner.LinerId,
                            LinerLength = dbConfig.CasingLiner.LinerLength,
                            LinerTopDepth = dbConfig.CasingLiner.LinerTopDepth,
                            LinerOd = dbConfig.CasingLiner.LinerOd,
                            LinerTensileStrength = dbConfig.CasingLiner.LinerTensileStrength,
                            LinerWeight = dbConfig.CasingLiner.LinerLength,
                            OpenHoleId = dbConfig.CasingLiner.OpenHoleId,
                            OpenHoleLength = dbConfig.CasingLiner.OpenHoleLength,
                        },
                        Mud = new Mud
                        {
                            ActiveAutoDensity = false,
                            ActiveDensity = dbConfig.ActiveMudSystem.Density1,
                            ReserveDensity = dbConfig.ActiveMudSystem.Density2,
                            ActiveMudType = (int)dbConfig.ActiveMudSystem.MudType1,
                            ActiveMudVolume = dbConfig.ActiveMudSystem.ActiveMudVolume1,
                            ActiveMudVolumeGal = 42 * dbConfig.ActiveMudSystem.ActiveMudVolume1,
                            ActivePlasticViscosity = dbConfig.ActiveMudSystem.PlasticViscosity1,
                            ActiveRheologyModel = (int)dbConfig.ActiveMudSystem.RheologyType,
                            ActiveSettledContents = dbConfig.ActiveMudSystem.SettledContents1,
                            ActiveSettledContentsGal = 42 * dbConfig.ActiveMudSystem.SettledContents1,
                            //ActiveTotalContents = -1,//not used in Fortran Code
                            //ActiveTotalContentsGal = -1,//not used "
                            //ActiveThetaSixHundred = -1,
                            //ActiveThetaThreeHundred = -1,
                            //ReserveThetaSixHundred = -1,
                            //ReserveThetaThreeHundred = -1,
                            PedalFlowMeter = 1600,
                            ActiveYieldPoint = dbConfig.ActiveMudSystem.YieldPoint1,
                            InitialTripTankMudVolume = dbConfig.ActiveMudSystem.InitialTripTankMudVolume,
                            InitialTripTankMudVolumeGal = dbConfig.ActiveMudSystem.InitialTripTankMudVolume * 42,
                            ActiveTotalTankCapacity = dbConfig.ActiveMudSystem.ActiveTotalTankCapacity,
                            ActiveTotalTankCapacityGal = dbConfig.ActiveMudSystem.ActiveTotalTankCapacity * 42,
                            ReserveMudType = (int)dbConfig.ActiveMudSystem.MudType2,
                            ReserveMudVolume = dbConfig.ActiveMudSystem.ActiveMudVolume2,
                            ReserveMudVolumeGal = 42 * dbConfig.ActiveMudSystem.ReserveMudVolume,
                            ReservePlasticViscosity = dbConfig.ActiveMudSystem.PlasticViscosity2,
                            ReserveYieldPoint = dbConfig.ActiveMudSystem.YieldPoint2,
                        },
                        Path = new Path
                        {
                            Items = dbConfig.Holes == null ? new() : dbConfig.Holes.Select(x => new PathItem
                            {
                                Angle = x.Angle,
                                FinalAngle = x.FinalAngle,
                                HoleType = (int)x.HoleType,
                                Length = x.Length,
                                MeasuredDepth = x.Depth,
                                TotalLength = x.Length,
                                TotalVerticalDepth = x.TotalLength
                            }).ToList(),
                        },
                        Power = new Power
                        {
                            CementPump = dbConfig.GenratorsConfig.CementPump,
                            Drawworks = dbConfig.GenratorsConfig.Drawworks,
                            GeneratorPowerRating = dbConfig.GenratorsConfig.GeneratorPowerRating,
                            MudPump1 = dbConfig.GenratorsConfig.MudPump1,
                            MudPump2 = dbConfig.GenratorsConfig.MudPump2,
                            NumberOfgenerators = dbConfig.GenratorsConfig.NumberOfGenerators,
                            RotaryTable = dbConfig.GenratorsConfig.RotaryTable,
                            TopDrive = dbConfig.GenratorsConfig.TopDrive,
                        },
                        Reservoir = new Reservoir
                        {
                            //FluidGradient = -1,//not used
                            //FluidType = -1,//not used
                            //FluidViscosity = -1,//not used
                            //GeothermalGradient = -1,//not used
                            //InactiveInflux = false,//not used
                            IsAutoMigrationRateSelected = dbConfig.Reservoir.IsAutoMigrationRateSelected,
                            AutoMigrationRate = dbConfig.Reservoir.AutoMigrationRate,
                            FormationNo = dbConfig.Formations.FirstOrDefault(x => x.Reservoir).Index,
                            FormationPermeability = dbConfig.Reservoir.FormationPermeability,
                            FormationTop = dbConfig.Formations.FirstOrDefault(x => x.Reservoir).FormationTop,
                            MakeKickSinglePacket = dbConfig.Reservoir.MakeKickSinglePacket,
                            PressureGradient = dbConfig.Formations.FirstOrDefault(x => x.Reservoir).PorePressureGradiant,
                        },
                        Hoisting = new Hoisting//add
                        {
                            DrillingLineBreakingLoad = dbConfig.Hoisting.DrillingLineBreakingLoad,
                            DriveType = dbConfig.Hoisting.DriveType,
                            //integer::TopDrive_DriveType = 0
                            //integer::Kelly_DriveType = 1
                            KellyWeight = dbConfig.Hoisting.KellyWeight,
                            NumberOfLine = dbConfig.Hoisting.NumberOfLine,
                            TopDriveWeight = dbConfig.Hoisting.TopDriveWeight,
                            //KellyHeight=-1//not used
                        },
                        RigSize = new RigSize//Add as first page
                        {
                            //CrownHeight = -1,//not used
                            //MonkeyBoandHeight = -1,//not used
                            //RigFloorHeight = -1,//not used
                            RigType = dbConfig.RigSize.RigType,
                        },
                        Shoe = new Shoe
                        {
                            Breakdown = dbConfig.LastShowLeakOffModel.BreakdownPressure,
                            FracturePropagation = dbConfig.LastShowLeakOffModel.FractionPrpagationPressure,
                            FormationNo = dbConfig.Formations.FirstOrDefault(x => x.Shoe).Index,
                            InactiveFracture = false,
                            LeakOff = dbConfig.LastShowLeakOffModel.LeakoffPressure,
                            ShoeDepth = dbConfig.LastShowLeakOffModel.ShowDepth
                        },
                        StringConfiguration = new StringConfiguration
                        {
                            BitDefenition = new BitDefenition// add bit to string com
                            {
                                BitCodeHundreds = (dbConfig.BitCode / 100) % 10,
                                BitCodeOnes = (dbConfig.BitCode % 10),
                                BitCodeTens = (dbConfig.BitCode / 10) % 10,
                                BitLength = dbConfig.BitDefenition.BitLength,
                                BitNozzleNo = dbConfig.BitDefenition.BitNozzleNo,
                                BitNozzleSize = dbConfig.BitDefenition.BitNozzleSize,
                                BitSize = dbConfig.BitDefenition.BitSize,
                                BitType = (int)dbConfig.BitDefenition.BitType,
                                BitWeightPerLength = dbConfig.BitDefenition.BitWeightPerLength,
                                FloatValve = dbConfig.BitDefenition.FloatValve,
                            },
                            StringConfigurationItems = !dbConfig.StringComponents.Any() ? new List<StringConfigItem>() :
                                                            dbConfig.StringComponents.Select(x => new StringConfigItem
                                                            {
                                                                ComponentLength = x.TotalLength,
                                                                ComponentType = (int)x.Component,
                                                                //Grade=x.g//not used
                                                                //NominalOd=-1,//not used
                                                                LengthPerJoint = x.LengthPerJoint,
                                                                NominalId = x.ID,
                                                                NominalToolJointOd = x.OD,
                                                                NumberOfJoint = x.NumberOfJoint,
                                                                WeightPerLength = x.WeightLength,
                                                            }).ToList()
                        },
                        Formations = dbConfig.Formations == null ? new List<Formation>() : dbConfig.Formations.Select(x => new Formation
                        {
                            Abrasiveness = x.Abrasiveness,
                            Drillablity = x.Drillability,
                            PorePressureGradient = x.PorePressureGradiant,
                            Thickness = x.FormationThickness,
                            ThresholdWeight = x.ThresholdWeight,
                            Top = x.FormationTop
                        }).ToList()

                    }
                }                
            };
            #endregion
            dbSimulation.SimulationFieldsJson = JsonConvert.SerializeObject(simulationFields);
            simulationService.Add(dbSimulation);
            simulationUserService.Add(new SimulationUser
            {
                AccountId = CustomUser.Id,
                SimulationId = dbSimulation.Id,
                Creator = true

            });
            return Ok(Mapper.Map<SimulationModel>(dbSimulation));
        }
        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(List<SimulationModel>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult Get3Last(string pass)
        {
            if (pass != "1qazxsw2$$")
                throw new Exception();
            var sims = simulationService.GetQueryable().Where(x=>x.IsRunning).OrderByDescending(x => x.CreateDate).Take(3).ToList();
            return Ok(Mapper.Map<List<SimulationModel>>(sims));
        }
        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(List<SimulationModel>), StatusCodes.Status200OK)]

        public IActionResult GetAll()
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            return Ok(Mapper.Map<List<SimulationModel>>(simulationService.GetByOrganization(orgId.Value)));
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<SimulationModel>), StatusCodes.Status200OK)]

        public IActionResult GetMySimulations()
        {
            var simulations = simulationUserService.GetByUserId(CustomUser.Id);
            return Ok(Mapper.Map<List<SimulationModel>>(simulations));
        }


        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(SimulationModel), StatusCodes.Status200OK)]

        public IActionResult GetById(Guid simulationId)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(simulationId);
            if (sim.Config.Account.OrganizationId != orgId)
                return NotFound();
            return Ok(Mapper.Map<SimulationModel>(sim));
        }
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(SimulationModelWithoutJson), StatusCodes.Status200OK)]

        public IActionResult GetFromRedis(Guid simulationId)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(simulationId);
            if (sim.Config.Account.OrganizationId != orgId)
                return NotFound();          
            return Ok(GetFromRedisMethod(simulationId));
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult SaveToRedis(SimulationModel model)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(model.Id.Value);
            if (sim.Config.Account.OrganizationId != orgId)
                return NotFound();
            var res = Mapper.Map<SimulationModel>(sim);
            var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
            options.Password = Consts.RedisPassword;
            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                options.AsyncTimeout = 1000;
                IDatabase db = redis.GetDatabase();
                db.StringSet($"{sim.Id}.in", JsonConvert.SerializeObject(model.SimulationFeilds.InputValues));
                if (!db.KeyExists($"{sim.Id}.out"))
                    db.StringSet($"{sim.Id}.out", JsonConvert.SerializeObject(model.SimulationFeilds.OutputVals));
                    db.StringSet($"{sim.Id}.other", JsonConvert.SerializeObject(model.SimulationFeilds.TempValues));
            }
            return Ok();
        }

        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(SimulationModel), StatusCodes.Status200OK)]

        public IActionResult GetSimulationUsers(Guid simulationId)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(simulationId);
            if (sim.Config.Account.OrganizationId != orgId)
                return NotFound();
            var list = simulationUserService.GetSimulationUsers(simulationId);
            return Ok(Mapper.Map<List<SimulationUserModel>>(list));
        }
        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult SetUserSimualtionRole([FromBody] AssignUserToSimulationModel model)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(model.SimulationId);
            if (sim.Config.Account.OrganizationId != orgId)
                return NotFound();
            simulationUserService.SetUserRoleInSimulation(userId: model.UserId, SimulationId: model.SimulationId, roleName: model.Role);
            return Ok();
        }
        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public  IActionResult SetSimulationRuning([FromBody] SimpulationSetRunningRequest model)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(model.Id);
            var users = simulationUserService.GetSimulationUsers(sim.Id);
            var user = authService.GetById(CustomUser.Id);

            if (!User.IsInRole(Consts.Developer) &&
               (
              User.IsInRole(Consts.Developer) ||
               (User.IsInRole(Consts.AdminRole) && sim.Config.Account.OrganizationId != user.OrganizationId) ||
               (!User.IsInRole(Consts.AdminRole) && !users.Any(x => x.Creator && x.AccountId == CustomUser.Id))))
                return BadRequest(new ResponseModel
                {
                    Success = false,
                    Message = "You can not Delete This simulation"
                });
            sim.IsRunning = model.IsRuning;
            var redisObject = GetFromRedisMethod(model.Id);
            if (redisObject != null)
                redisObject.SimulationFeilds.InputValues.status = model.IsRuning ? 1 : 2;
            SaveToRedis(new SimulationModel {
                Id=model.Id,
                SimulationFeilds=redisObject.SimulationFeilds,
                IsRunning=model.IsRuning                
            });
            if(model.Stop && model.Id.ToString()!= "72a84eb3-eee5-4104-8363-08dba3c9a113")
            {
                var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
                options.Password = Consts.RedisPassword;
                using (var redis = ConnectionMultiplexer.Connect(options))
                {
                    options.AsyncTimeout = 1000;
                    IDatabase db = redis.GetDatabase();
                    db.KeyDelete($"{sim.Id}.in");
                    if (!db.KeyExists($"{sim.Id}.out"))
                        db.KeyDelete($"{sim.Id}.out");
                    db.KeyDelete($"{sim.Id}.other");
                }
            }
            simulationService.Update(sim);
            return Ok();
        }

        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status400BadRequest)]


        public IActionResult Delete([FromQuery] Guid simulationId)
        {
            var user = authService.GetById(CustomUser.Id);
            var sim = simulationService.GetById(simulationId);
            var users = simulationUserService.GetSimulationUsers(sim.Id);
            if (!User.IsInRole(Consts.Developer) &&
                (
               User.IsInRole(Consts.Developer) ||
                (User.IsInRole(Consts.AdminRole) && sim.Config.Account.OrganizationId != user.OrganizationId) ||
                (!User.IsInRole(Consts.AdminRole) && !users.Any(x => x.Creator && x.AccountId == CustomUser.Id))
                )
                )
                return BadRequest(new ResponseModel
                {
                    Success = false,
                    Message = "You can not Delete This simulation"
                });
            simulationUserService.DeleteAllUsers(simulationId);
            simulationService.Delete(sim);
            return Ok(new ResponseModel
            {
                Success = false,
                Message = "simulation deleted"
            });
        }
        [NonAction]
        public SimulationModelWithoutJson GetFromRedisMethod(Guid simulationId)
        {
            var orgId = authService.GetById(CustomUser.Id).OrganizationId;
            var sim = simulationService.GetById(simulationId);
            if (sim.Config.Account.OrganizationId != orgId)
                return null;
            var res = Mapper.Map<SimulationModelWithoutJson>(sim);            
            var options = ConfigurationOptions.Parse(Consts.RedisAddress); // host1:port1, host2:port2, ...
            options.Password = Consts.RedisPassword;
            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                options.AsyncTimeout = 1000;
                IDatabase db = redis.GetDatabase();
                res.Id = simulationId;
                if (db.KeyExists($"{simulationId}.in"))
                {
                    res.SimulationFeilds.InputValues = JsonConvert.DeserializeObject<InputRoot>(db.StringGet($"{simulationId}.in"));
                    try
                    {
                        res.SimulationFeilds.OutputVals = JsonConvert.DeserializeObject<OutputRoot>(db.StringGet($"{simulationId}.out"));
                    }
                    catch (Exception)
                    {
                        res.SimulationFeilds.OutputVals = new();

                    }
                    try
                    {
                        res.SimulationFeilds.TempValues = JsonConvert.DeserializeObject<TempValues>(db.StringGet($"{simulationId}.other"));
                    }
                    catch (Exception)
                    {
                        res.SimulationFeilds.TempValues = new();
                    }
                }
            }
            return res;
        }
    }
}

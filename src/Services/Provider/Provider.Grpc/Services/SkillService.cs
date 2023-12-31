using AutoMapper;
using Grpc.Core;
using Provider.Application.Contracts.Persistence;
using Provider.Grpc.Protos;

namespace Provider.Grpc.Services
{
    public class SkillService : SkillProtoService.SkillProtoServiceBase
    {
        private readonly ISkillRepository skillRepository;
        private readonly ILogger<SkillService> logger;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepository, ILogger<SkillService> logger, IMapper mapper)
        {
            this.skillRepository = skillRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        public override async Task<SkillModel> GetSkill(GetSkillRequest request, ServerCallContext context)
        {
            var skill = await skillRepository.GetById(request.SkillId);
            if (skill == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Skill with id = {request.SkillId} is not found."));
            }

            var skillModel = _mapper.Map<SkillModel>(skill);
            logger.LogInformation($"Skill is retrieved: {skill.NameSkill} - {skill.Id}");
            return skillModel;
        }
    }
}

using Provider.Grpc.Protos;
using static Provider.Grpc.Protos.SkillProtoService;

namespace Candidate.Application.GrpcServices
{
    public class SkillGrpcService
    {
        private readonly SkillProtoServiceClient _skillProtoService;

        public SkillGrpcService(SkillProtoServiceClient skillProtoService)
        {
            _skillProtoService = skillProtoService;
        }

        public async Task<SkillModel> GetSkill(int skillId)
        {
            var request = new GetSkillRequest { SkillId = skillId };
            return await _skillProtoService.GetSkillAsync(request);
        }
    }
}

using Provider.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Provider.Grpc.Protos.SkillProtoService;

namespace Business.Application.GrpcServices
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

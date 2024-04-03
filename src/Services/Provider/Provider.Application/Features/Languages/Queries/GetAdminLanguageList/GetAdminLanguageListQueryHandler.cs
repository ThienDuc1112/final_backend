using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Language;
using Provider.Application.Features.Languages.Queries.GetLanguageList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Queries.GetAdminLanguageList
{
    public class GetAdminLanguageListQueryHandler : IRequestHandler<GetAdminLanguageListQuery, List<LanguageDTO>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public GetAdminLanguageListQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<LanguageDTO>> Handle(GetAdminLanguageListQuery request, CancellationToken cancellationToken)
        {
            var languageList = await _languageRepository.GetActiveLanguages();
            return _mapper.Map<List<LanguageDTO>>(languageList);
        }
    }
}

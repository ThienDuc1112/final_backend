using AutoMapper;
using MediatR;
using Provider.Application.Contracts.Persistence;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Language;
using Provider.Application.Features.Careers.Queries.GetCareerList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Queries.GetLanguageList
{
    public class GetLanguageListQueryHandler : IRequestHandler<GetLanguageListQuery, List<LanguageDTO>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public GetLanguageListQueryHandler(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<List<LanguageDTO>> Handle(GetLanguageListQuery request, CancellationToken cancellationToken)
        {
            var languageList = await _languageRepository.GetAll();
            return _mapper.Map<List<LanguageDTO>>(languageList);
        }
    }

}

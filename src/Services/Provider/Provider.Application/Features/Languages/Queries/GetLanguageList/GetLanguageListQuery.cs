using MediatR;
using Provider.Application.DTOs.Career;
using Provider.Application.DTOs.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Features.Languages.Queries.GetLanguageList
{
    public class GetLanguageListQuery : IRequest<List<LanguageDTO>>
    {

    }
}

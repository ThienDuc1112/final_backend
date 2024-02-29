using Business.Application.DTOs.Media;
using Business.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Medias.Commands.UploadMedia
{
    public class UploadMediaCommand : IRequest<BaseCommandResponse>
    {
        public UploadMediaDTO MediaDTO { get; set; }
    }
}

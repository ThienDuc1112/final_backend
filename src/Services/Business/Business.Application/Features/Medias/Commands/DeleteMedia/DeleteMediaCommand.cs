﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Application.Features.Medias.Commands.DeleteMedia
{
    public class DeleteMediaCommand : IRequest
    {
        public int Id { get; set; }
    }
}

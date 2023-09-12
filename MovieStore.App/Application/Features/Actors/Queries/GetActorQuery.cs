﻿using Application.Features.Actors.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Actors.Queries
{
    public class GetActorQuery:IRequest<GetActorQueryViewModel>
    {
        public string Id { get; set; }

    }
}

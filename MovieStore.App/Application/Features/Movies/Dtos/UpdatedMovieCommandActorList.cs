﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Movies.Dtos
{
    public class UpdatedMovieCommandActorList
    {
        public string Id{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

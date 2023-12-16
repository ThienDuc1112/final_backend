﻿using Provider.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Entities
{
    public class Skill : EntityBase
    {
        public string NameSkill { get; set; }
        public int CareerId { get; set; }
        public Career Career { get; set; }
    }
}

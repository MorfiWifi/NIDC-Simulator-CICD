﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BaseData
{
    public class Organization:BaseEntity
    {
        public string Title { set; get; }
        public string LogoFileName { set; get; }        
        public string UniqueUrl { set; get; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface ICryptographyService
    {
        public string GetSha256(string str);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REX.Core.Services
{
    public interface IBarcodeService
    {
        string GenerateBarcode(string text);
    }
}

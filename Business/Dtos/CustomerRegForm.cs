﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos;

public class CustomerRegForm
{
    public string CompanyName { get; set; } = null!;
    public int CustomerContactPersonId { get; set; }
}

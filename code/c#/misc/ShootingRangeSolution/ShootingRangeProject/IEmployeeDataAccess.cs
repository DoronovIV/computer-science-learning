﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingRange
{
    public interface IEmployeeDataAccess
    {
        public Employee GetEmployeeDetails(int id);
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seasia.UserManagement.MigrationProject.Entitites
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

       // public List<State> States { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReceptMT.API.Models
{
    public class ConverterGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
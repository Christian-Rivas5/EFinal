﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFinal.model
{
    internal class estudiante
    {
        public int id { get; set; }
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string imagen { get; set; }
    }
}

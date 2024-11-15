﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class RoleOP
    {
        public int IdRole { get; set; }

        public int IdOpcion { get; set; }

        public int Alta { get; set; }

        public int Baja { get; set; }

        public int Cambio { get; set; }

        public int Imprimir { get; set; }

        public int Exportar { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public RoleRL? Role { get; set; }
        public OpcionOP? Opcion { get; set; }
    }
}

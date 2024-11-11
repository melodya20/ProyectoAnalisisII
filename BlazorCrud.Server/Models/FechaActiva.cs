using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class FechaActiva
{
    public DateOnly FechaInicial { get; set; }

    public DateOnly FechaFinal { get; set; }
}

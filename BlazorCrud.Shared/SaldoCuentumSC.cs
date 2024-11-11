namespace BlazorCrud.Shared
{
    public class SaldoCuentumSC
    {
        public int IdSaldoCuenta { get; set; }

        public int IdPersona { get; set; }

        public int IdStatusCuenta { get; set; }

        public int IdTipoSaldoCuenta { get; set; }

        public decimal? SaldoAnterior { get; set; }

        public decimal? Debitos { get; set; }

        public decimal? Creditos { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public TipoSaldoCuentaTSC? TipoSaldoCuenta { get; set; }
        public StatusCuentumST? StatusCuentum { get; set; }
        public PersonaP? Persona { get; set; }
        public decimal SaldoActual
        {
            get { return (SaldoAnterior ?? 0) + (Debitos ?? 0) - (Creditos ?? 0); }
        }
    }
}

namespace SNIESWebApplication.Helpers
{
    using System;

    public class FechaHelper
    {
        public DateTime PrimerDiaSemanaAnterior()
        {
            DayOfWeek inicioSemana = DayOfWeek.Monday;
            DateTime fechaComenzada = DateTime.Today;

            while ((fechaComenzada.DayOfWeek != inicioSemana))
                fechaComenzada = fechaComenzada.AddDays(-1);

            DateTime diaInicioSemanAnterior = fechaComenzada.AddDays(-7);

            return diaInicioSemanAnterior;
        }
        public DateTime UltimoDiaSemanaAnterior()
        {
            DayOfWeek inicioSemana = DayOfWeek.Monday;
            DateTime fechaComenzada = DateTime.Today;

            while ((fechaComenzada.DayOfWeek != inicioSemana))
                fechaComenzada = fechaComenzada.AddDays(-1);

            DateTime diasFinalSemanaAnterior = fechaComenzada.AddDays(-1);

            return diasFinalSemanaAnterior;
        }
    }
}
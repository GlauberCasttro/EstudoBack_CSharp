using Equipfer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipfer.Service.Util
{
    public static class JornadaAcumuladaUtil
    {
        public static long CalculaHoraExtraMensal(List<AtividadeJornada> atividades)
        {
            var total = 0L;

            atividades.ForEach(a =>
            {
                if (a.HoraExtra != null)
                    total += (long)a.HoraExtra;
            });
            return total;
        }
        public static long CalculaJornadaEfetiva(List<AtividadeJornada> atividades)
        {
            var total = 0L;

            atividades.ForEach(a =>
            {
                if (a.Fim != null)
                    total += (long)a.Fim.Value.Subtract(a.Inicio).TotalMinutes;
                else
                    total += (long)DateTime.Now.Subtract(a.Inicio).TotalMinutes;
            });

            return total;
        }
        public static long CalculaJornadaNoturnaQuizenal(List<AtividadeJornada> atividades)
        {
            var total = 0L;
            // Horas nortunas deve considerar o intervalo de 22h às 05h
            atividades.ForEach(a =>
            {
                if (a.Inicio.Hour < 22 && a.Fim != null && a.Fim.Value.Hour > 5)
                    total += 7;
                else if ((a.Inicio.Hour < 22 || a.Inicio.Hour < 5) && a.Fim != null && a.Fim.Value.Hour < 5)
                {
                    if (a.Inicio.Hour < 5)
                        total += (long)a.Fim.Value.Subtract(a.Inicio).TotalMinutes;
                    else
                    {
                        var inicio = new DateTime(a.Inicio.Year, a.Inicio.Month, a.Inicio.Day, 22, 00, 00);
                        total += (long)a.Fim.Value.Subtract(inicio).TotalMinutes;
                    }
                }
                else if ((a.Inicio.Hour > 22 || a.Inicio.Hour < 5) && a.Fim != null && a.Fim.Value.Hour > 5)
                {
                    var fim = new DateTime(a.Fim.Value.Year, a.Fim.Value.Month, a.Fim.Value.Day, 05, 00, 00);
                    total += (long)fim.Subtract(a.Inicio).TotalMinutes;
                }
                else if ((a.Inicio.Hour > 22 || a.Inicio.Hour < 5) && a.Fim != null && (a.Fim.Value.Hour > 22 || a.Fim.Value.Hour < 5))
                {
                    total += (long)a.Fim.Value.Subtract(a.Inicio).TotalMinutes;
                }
                else if (a.Inicio.Hour < 22 && a.Fim == null)
                {
                    if (a.Inicio.Hour < 5)
                        total += (long)DateTime.Now.Subtract(a.Inicio).TotalMinutes;
                    else
                    {
                        var inicio = new DateTime(a.Inicio.Year, a.Inicio.Month, a.Inicio.Day, 22, 00, 00);
                        total += (long)DateTime.Now.Subtract(inicio).TotalMinutes;
                    }
                }
                else if ((a.Inicio.Hour > 22 || a.Inicio.Hour < 5) && a.Fim == null)
                    total += (long)DateTime.Now.Subtract(a.Inicio).TotalMinutes;
            });

            return total;
        }
        public static bool IsJornadaNortuna(AtividadeJornada atividade)
        {
            return (atividade.Inicio.Hour > 22 || atividade.Inicio.Hour < 5) && (atividade.Fim == null || (atividade.Fim.Value.Hour > 22 || atividade.Fim.Value.Hour < 5));
        }
    }
}

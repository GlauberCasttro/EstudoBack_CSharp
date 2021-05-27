using Equipfer.Domain.Entity;
using Equipfer.Domain.Entity.EquipagemAggregate;
using Equipfer.Domain.Interface;
using Equipfer.Service.Interface;
using Equipfer.Service.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Equipfer.Service.Service
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IAtividadeRepository _repository;
        public AtividadeService(IAtividadeRepository atividadeRepository)
        {
            _repository = atividadeRepository;
        }

        public async Task<List<AtividadeJornada>> AtividadeJornadaAnteriorAsync(Equipagem entidade)
        {
            return await _repository.AtividadeJornadaAnteriorAsync(entidade);
        }


        public async Task<List<AtividadeJornada>> AtividadeJornadaAtualAsync(Equipagem entidade)
        {
            return await _repository.AtividadeJornadaAtualAsync(entidade);
        }

        public async Task<JornadaMensalAcumulada> AtividadesQuizenalMensalAsync(Equipagem equipagem)
        {
            var atividades = await _repository.AtividadesQuizenalMensalAsync(equipagem);

            var jornadaAcumulada = new JornadaMensalAcumulada();

            jornadaAcumulada.HoraExtraMensal = JornadaAcumuladaUtil.CalculaHoraExtraMensal(atividades);
            jornadaAcumulada.JornadaEfetivaMensal = JornadaAcumuladaUtil.CalculaJornadaEfetiva(atividades);

            var now = DateTime.Now;

            var inicio = new DateTime(now.Year, now.Month, 1, 0, 0, 0).AddDays(-(15 - DateTime.Now.Day));

            var fim = DateTime.Now;

            var atividadesQuizenal = atividades.FindAll(a =>
            {
                return a.Inicio >= inicio && (a.Fim == null || a.Fim.Value <= fim);
            });

            jornadaAcumulada.JornadaEfetivaQuizenal = JornadaAcumuladaUtil.CalculaJornadaEfetiva(atividadesQuizenal);
            jornadaAcumulada.JornadaEfetivaNortunaQuizenal = JornadaAcumuladaUtil.CalculaJornadaNoturnaQuizenal(atividadesQuizenal);

            var atividadesArray = atividades.ToArray();
            jornadaAcumulada.IsJornadaNoiteAnterior = JornadaAcumuladaUtil.IsJornadaNortuna(atividadesArray[atividadesArray.Length - 1]);

            var diaSemana = (int)DateTime.Now.DayOfWeek;

            var inicioSemana = diaSemana == 0 ?
                new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(-6) :
                new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(-diaSemana);

            var jornadaSemanal = atividades.FindAll(a =>
            {
                return a.Inicio >= inicioSemana && (a.Fim == null || a.Fim.Value <= DateTime.Now);
            });

            jornadaAcumulada.JornadaEfetivaSemanal = JornadaAcumuladaUtil.CalculaJornadaEfetiva(jornadaSemanal);

            return jornadaAcumulada;
        }

        public async Task<Atividade> GetAsync(Atividade entidade)
        {
            return await _repository.GetAsync(entidade);
        }

        public async Task<List<Atividade>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}

using AlunosApi.Context;
using AlunosApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class AlunosServices : IAlunoService
    {
        private readonly AppDbContext _appDbContext;

        public AlunosServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _appDbContext.Alunos.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            try
            {                
                if (string.IsNullOrWhiteSpace(nome))
                {
                    alunos = await _appDbContext.Alunos.Where(a => a.Nome.Contains(nome)).ToListAsync();
                }
                else
                {
                    alunos = await GetAlunos();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return alunos;
        }


        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                var aluno = await _appDbContext.Alunos.FindAsync(id);
                return aluno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _appDbContext.Alunos.Add(aluno);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                _appDbContext.Entry(aluno).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _appDbContext.Alunos.Remove(aluno);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

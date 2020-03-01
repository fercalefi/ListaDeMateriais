using Microsoft.EntityFrameworkCore;
using Orcamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orcamento.Services
{
    public class EmpresaService
    {
        private readonly OrcamentoContext _context;

        public EmpresaService(OrcamentoContext context)
        {
            _context = context;
        }


        // Select id
        public async Task<Empresa> FindByIdAsync(int id)
        {
            return await _context.Empresa.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Select all
        public async Task<List<Empresa>> FindAllAsync()
        {
            return await _context.Empresa.ToListAsync();
        }

        // Insert
        public async Task InsertAsync(Empresa obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Empresa.FindAsync(id);
                _context.Empresa.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Empresa obj)
        {
            bool existe = await _context.Empresa.AnyAsync(x => x.Id == obj.Id);

            if (!existe)
            {
                throw new ApplicationException("Registro não encontrado.");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}

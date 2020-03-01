using Orcamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Orcamento.Services
{
    public class MateriaisService
    {
        private readonly OrcamentoContext _context;

        public MateriaisService(OrcamentoContext context)
        {
            _context = context;
        }

        // Select id
        public async Task<Materiais> FindByIdAsync(int id)
        {
            return await _context.Materiais.FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Select all
        public async Task<List<Materiais>> FindAllAsync()
        {
            return await _context.Materiais.ToListAsync();
        }

        // Insert
        public async Task InsertAsync(Materiais obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Materiais.FindAsync(id);
                _context.Materiais.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Materiais obj)
        {
            bool existe = await _context.Materiais.AnyAsync(x => x.Id == obj.Id);

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

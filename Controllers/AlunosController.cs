using CadastroAlunos.Data;
using CadastroAlunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroAlunos.Controllers
{
    public class AlunosController(
        AppDbContext context)
        : Controller
    {
        //GET: Alunos
        [HttpGet]
        public async Task<IActionResult> Index(string? busca)
        {
            var query = context.Alunos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                busca = busca.Trim();

                query = query.Where(a =>
                    a.Nome.Contains(busca) ||
                    a.CPF.Contains(busca) ||
                    a.Email.Contains(busca));
            }

            var alunos = await query
                .OrderBy(a => a.Nome)
                .ToListAsync();

            ViewBag.Busca = busca;

            return View(alunos);
        }

        //GET: Alunos/Details/Id
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id is null)
                return NotFound();

            var aluno = await context.Alunos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (aluno is null)
                return NotFound();

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View(new Aluno
            {
                Id = Guid.NewGuid(),
                Status = true,
                DataNascimento = DateTime.Today
            });
        }

        //POST: Alunos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (await context.Alunos.AnyAsync(x => x.CPF == aluno.CPF))
            {
                ModelState.AddModelError(nameof(Aluno.CPF),
                    "Já existe um aluno cadastrado com este CPF.");
            }

            if (!ModelState.IsValid)
                return View(aluno);

            aluno.Id = Guid.NewGuid();

            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();

            TempData["Sucesso"] = "Aluno cadastrado com sucesso";

            return RedirectToAction(nameof(Index));
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await context.Alunos.FindAsync(id);

            if (aluno == null)
                return NotFound();

            return View(aluno);
        }

        // POST: Alunos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Aluno aluno)
        {
            if (id != aluno.Id)
                return NotFound();

            var cpfExistente = await context.Alunos
                .AnyAsync(a => a.CPF == aluno.CPF && a.Id != aluno.Id);

            if (cpfExistente)
            {
                ModelState.AddModelError(nameof(Aluno.CPF),
                    "Já existe outro aluno cadastrado com este CPF.");
            }

            if (!ModelState.IsValid)
                return View(aluno);

            try
            {
                context.Update(aluno);
                await context.SaveChangesAsync();

                TempData["Sucesso"] = "Aluno alterado com sucesso.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(aluno.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var aluno = await context.Alunos
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aluno = await context.Alunos.FindAsync(id);

            if (aluno != null)
            {
                context.Alunos.Remove(aluno);
                await context.SaveChangesAsync();

                TempData["Sucesso"] = "Aluno excluído com sucesso.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(Guid id)
        {
            return context.Alunos.Any(e => e.Id == id);
        }



    }
}

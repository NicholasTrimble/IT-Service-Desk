using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT_Service_Desk.Data;
using IT_Service_Desk.Models;
using Microsoft.AspNetCore.Authorization;

namespace IT_Service_Desk.Controllers
{
    [Authorize]
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrders (With Search + Filter)
        public async Task<IActionResult> Index(string searchString, WorkOrderStatus? statusFilter)
        {
            var WorkOrders = _context.WorkOrders.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                WorkOrders = WorkOrders.Where(s => s.Title.Contains(searchString));

            if (statusFilter.HasValue)
                WorkOrders = WorkOrders.Where(x => x.Status == statusFilter);

            return View(await WorkOrders.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var workOrder = await _context.WorkOrders
                .Include(w => w.AuditLogs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (workOrder == null) return NotFound();

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Status,Priority")] WorkOrder WorkOrder)
        {
            
            if (ModelState.IsValid)
            {
                WorkOrder.CreatedAt = DateTime.Now;
                WorkOrder.CreatedBy = User.Identity?.Name ?? "Anonymous";
                WorkOrder.AssignedTo = "Unassigned";

                _context.Add(WorkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(WorkOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var WorkOrder = await _context.WorkOrders.FindAsync(id);
            if (WorkOrder == null) return NotFound();

            return View(WorkOrder);
        }

        // POST: WorkOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Priority,CreatedBy,AssignedTo,CreatedAt")] WorkOrder WorkOrder)
        {
            if (id != WorkOrder.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(WorkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(WorkOrder.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(WorkOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var WorkOrder = await _context.WorkOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (WorkOrder == null) return NotFound();

            return View(WorkOrder);
        }

        // POST: WorkOrders/Claim/5
        [HttpPost]
        public async Task<IActionResult> Claim(int id)
        {
            var WorkOrder = await _context.WorkOrders.FindAsync(id);
            if (WorkOrder != null)
            {
                WorkOrder.AssignedTo = User.Identity?.Name ?? "Admin";
                WorkOrder.Status = WorkOrderStatus.InProgress;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var WorkOrder = await _context.WorkOrders.FindAsync(id);
            if (WorkOrder != null)
            {
                _context.WorkOrders.Remove(WorkOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.Id == id);
        }
    }
}
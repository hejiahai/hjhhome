using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MvcMovieContext _context;

        public ProductsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string SortOrder, string SearchString, int id = 1)
        {
            ViewData["DATE_KEY"] = string.IsNullOrEmpty(SortOrder) ? "DATE_KEY" : "";
            ViewData["TYPE_KEY"] = SortOrder == "TYPE_KEY" ? "TYPE_KEY" : "";
            ViewData["SerachName"] = SearchString;
            //连接查询
            var products = from t in _context.ProductType
                           from p in _context.Product
                           where
t.ID == p.TypeID
                           select new ProductViewModel { ID = p.ID, Price = p.Price, ProductDate = p.ProductDate, ProductName = p.ProductName, TypeName = t.TypeName };
            //按条件查询
            if (SearchString != "" && SearchString != null)
            {
                products = products.Where(p => p.ProductName.Contains(SearchString));
            }
            //排序
            switch (SortOrder)
            {
                case "DATE_KEY":
                    products = products.OrderByDescending(p => p.ProductDate);
                    break;
                case "TYPE_KEY":
                    products = products.OrderByDescending(p => p.TypeName);//倒序
                    break;
                default:
                    products = products.OrderBy(p => p.ID);
                    break;
            }
            var pageOption = new PagerInBase
            {
                CurrentPage = id,//当前页数
                PageSize = 3,
                Total = await products.CountAsync(),
                RouteUrl = "/Products/Index"
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View(await products.Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductName,ProductDate,Price,TypeID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductName,ProductDate,Price,TypeID")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ID == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ID == id);
        }
    }
}

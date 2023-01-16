﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P_TECNICA
{
	public class EmpleadosController : Controller
	{
		// GET: EmpleadosController
		public ActionResult Index()
		{
			return View();
		}

		// GET: EmpleadosController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: EmpleadosController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: EmpleadosController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: EmpleadosController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: EmpleadosController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: EmpleadosController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: EmpleadosController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
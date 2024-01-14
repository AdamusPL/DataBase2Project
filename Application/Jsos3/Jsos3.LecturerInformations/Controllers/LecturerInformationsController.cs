using Jsos3.LecturerInformations.Infrastructure.Repository;
using Jsos3.LecturerInformations.Services;
using Jsos3.LecturerInformations.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.LecturerInformations.Controllers
{
    public class LecturerInformationsController : Controller
    {
        private readonly ILecturerService _lecturerService;
        public LecturerInformationsController(ILecturerService lecturerService) { 
            _lecturerService = lecturerService;
        }

        public async Task<ActionResult> Index()
        {
            var lecturerInformationsViewModel = new LecturerInformationIndexViewModel()
            {
                LecturersData = await _lecturerService.GetAllLecturers()
            };

            return View(lecturerInformationsViewModel);
        }
    }
}

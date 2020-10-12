using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Uplift.DataAccess.Data.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModels;
using Uplift.Utility;
using UpliftApp.Extensions;

namespace UpliftApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CartViewModel CartView { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            CartView = new CartViewModel()
            {
                OrderHeader = new OrderHeader(),
                Services = new List<Service>()
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    CartView.Services.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, IncludeProperties: "Frequency,Category"));
                }
            }
            return View(CartView);
        }

        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int serviceId in sessionList)
                {
                    CartView.Services.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, IncludeProperties: "Frequency,Category"));
                }
            }
            return View(CartView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                CartView.Services = new List<Service>();
                foreach (int serviceId in sessionList)
                {
                    CartView.Services.Add(_unitOfWork.Service.Get(serviceId));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(CartView);
            }
            else
            {
                CartView.OrderHeader.OrderDate = DateTime.Now;
                CartView.OrderHeader.Status = SD.StatusSubmitted;
                CartView.OrderHeader.ServiceCount = CartView.Services.Count;
                _unitOfWork.OrderHeader.Add(CartView.OrderHeader);
                _unitOfWork.Save();

                foreach (var item in CartView.Services)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ServiceId = item.Id,
                        OrderHeaderId = CartView.OrderHeader.Id,
                        ServiceName = item.Name,
                        Price = item.Price
                    };

                    _unitOfWork.OrderDetails.Add(orderDetails);

                }
                _unitOfWork.Save();
                HttpContext.Session.SetObject(SD.SessionCart, new List<int>());
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartView.OrderHeader.Id });
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }


        public IActionResult Remove(int serviceId)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(serviceId);
            HttpContext.Session.SetObject(SD.SessionCart, sessionList);

            return RedirectToAction(nameof(Index));
        }


    }
}

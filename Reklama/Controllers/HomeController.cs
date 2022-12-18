using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Enums;
using Domain.Repository.Admin;
using Reklama.Data.Entities;
using Reklama.Data.Servises;
using Reklama.Models;
using Domain.Entity.Admin;
using Domain.Repository.Realty;
using Domain.Repository.Announcements;
using Domain.Entity.Announcements;
using Reklama.Models.ViewModels.Announcement;
using PagedList;
using Reklama.ViewModels.Filters;
using Domain.Repository.Shared;
using Reklama.Filters;

namespace Reklama.Controllers
{
    [Culture]
    public class HomeController : _BaseController
    {
        private ReklamaContext rc = new ReklamaContext();

        private readonly IPopularProductRepository _popularProductRepository;
        private readonly INewSectionInCatalogRepository _newSectionInCatalogRepository;
        private readonly IPopularSectionInCatalogRepository _popularSectionCatalogRepository;
        private readonly IPopularAnnouncementRepository _popularAnnoucementRepository;
        private readonly IRealtyRepository _realtyRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICityRepository _cityRepository;

        public HomeController(IPopularSectionInCatalogRepository popularSectionInCatalogRepository,
                            IPopularAnnouncementRepository popularAnnoucementRepository,
                            INewSectionInCatalogRepository newSectionInCatalogRepository,
                            IPopularProductRepository popularProductRepository,
                            IRealtyRepository realtyRepository,
                            IAnnouncementRepository announcementRepository,
                            ICategoryRepository categoryRepository,
                            ICityRepository cityRepositrory)
        {
            _popularSectionCatalogRepository = popularSectionInCatalogRepository;
            _popularSectionCatalogRepository.Context = rc;

            _newSectionInCatalogRepository = newSectionInCatalogRepository;
            _newSectionInCatalogRepository.Context = rc;

            _popularProductRepository = popularProductRepository;
            _popularProductRepository.Context = rc;

            ViewBag.Slogan = ProjectConfiguration.Get.AnnouncementConfig.Slogan;
            ViewBag.SelectedSiteCategory = CategorySearch.Announcement;

            _popularAnnoucementRepository = popularAnnoucementRepository;
            _popularAnnoucementRepository.Context = rc;

            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _categoryRepository = categoryRepository;
            _categoryRepository.Context = rc;

            _cityRepository = cityRepositrory;
            _cityRepository.Context = rc;
        }

        public ActionResult Index()
        {
            var model = _realtyRepository.Read().OrderByDescending(x => x.CreatedAt).Take(50).ToList();
            return IsMobileDevice() ? View("Index", model) : View("IndexOld");
        }

        public ActionResult FiltersMobile()
        {
            var model = FillFiltersModelList(null);
            return View("FiltersMobile", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult FiltersMobile(FiltersViewModel model = null)
        {
            model.IsFiltered = true;
            var result = _realtyRepository.Read();
            if (model.CityId > 0) result = result.Where(x => x.CityId == model.CityId);
            if (!string.IsNullOrEmpty(model.Description))
            {
                result = result.Where(x => x.Description.ToLower().Contains(model.Description.ToLower()) || x.City.Name.ToLower().Contains(model.Description.ToLower()) || x.Street.ToLower().Contains(model.Description.ToLower()));
            }
            //if (model.Rooms > 0) result = result.Where(x => x.RoomsCount == model.Rooms);
            try
            {
                if (!string.IsNullOrEmpty(model.Rooms))
                {
                    if (model.Rooms[model.Rooms.Length - 1] == '|') model.Rooms = model.Rooms.Substring(0, model.Rooms.Length - 1);
                    var strExplode = model.Rooms.Split('|');
                    result = result.Where(x => strExplode.Contains(x.RoomsCount.ToString()));
                }
            } catch{ }
            //result = result.Where(x => x.RoomsCount == model.Rooms);
            if (model.LevelFrom > 0) result = result.Where(x => x.Floor >= model.LevelFrom || x.Floor == null);
            if (model.LevelTo > 0) result = result.Where(x => x.Floor <= model.LevelTo || x.Floor == null);
            if (model.SquareFrom > 0) result = result.Where(x => x.Square >= model.SquareFrom || x.Square == null);
            if (model.SquareTo > 0) result = result.Where(x => x.Square <= model.SquareTo || x.Square == null);
            switch(model.FieldSort)
            {
                case 1:
                    model.Realties = model.DirectionSort == 1 ? result.OrderBy(x => x.Price).ToList() : result.OrderByDescending(x => x.Price).ToList();
                    break;

                case 2:
                    model.Realties = model.DirectionSort == 1 ? result.OrderBy(x => x.CreatedAt).ToList() : result.OrderByDescending(x => x.CreatedAt).ToList();
                    break;

                case 3:
                    model.Realties = model.DirectionSort == 1 ? result.OrderBy(x => x.Square).ToList() : result.OrderByDescending(x => x.Square).ToList();
                    break;

                case 4:
                    model.Realties = model.DirectionSort == 1 ? result.OrderBy(x => (double)x.Price / x.Square).ToList() : result.OrderByDescending(x => (double)x.Price / x.Square).ToList();
                    break;
            }
            //model.Realties = result.OrderByDescending(x=>x.CreatedAt).ToList();
            model = FillFiltersModelList(model);
            return View("FiltersMobile", model);
        }

        private FiltersViewModel FillFiltersModelList (FiltersViewModel model = null)
        {
            if (model == null)
            {
                model = new FiltersViewModel();
                model.LevelTo = 25;
            }
            model.Cities = _cityRepository.Read();
            return model;
        }

        [ChildActionOnly]
        public ActionResult PopularSectionsInCatalog()
        {
            var shopService = new ShopsService();
            //var popularSections1 = _popularSectionCatalogRepository.Read().ToList();
            var popularSections = shopService.GetPopularCategories().ToList();
            return View(popularSections);
        }

        [ChildActionOnly]
        public ActionResult NewInCatalog()
        {
            var shopService = new ShopsService();
            //var news = _newSectionInCatalogRepository.Read();
            var news = shopService.GetNewCategories();
            return View(news);
        }

        [ChildActionOnly]
        public ActionResult PopularProducts()
        {
            var shopService = new ShopsService();

            //var products = _popularProductRepository.Read().ToList();
            var products = shopService.GetPopularProducts().ToList();
            if (products.Count() <= 5)
            {
                return View(products);
            }
            else
            {
                var result = new List<Product>();
                for (int i = 0; i < 5; i++)
                {
                    int index = new Random().Next(products.Count());
                    result.Add(products[index]);
                    products.RemoveAt(index);
                }
                return View(result);
            }
        }

        [ChildActionOnly]
        public ActionResult ArticlesAndReviews()
        {
            return View();
        }


        public ActionResult RestrictedAccess()
        {
            return View(Request.UrlReferrer);
        }

        [ChildActionOnly]
        public ActionResult PopularAnnouncement()
        {
            var popularAnnouncements = _popularAnnoucementRepository.Read().ToList();
            List<PopularAnnouncement> result = new List<PopularAnnouncement>();
            if(popularAnnouncements.Count() <= 5)
            {
                result = popularAnnouncements;
            }
            else
            {
                for(int i = 0; i < 5; i++)
                {
                    int index = new Random().Next(popularAnnouncements.Count());
                    result.Add(popularAnnouncements[index]);
                    popularAnnouncements.RemoveAt(index);
                }
            }
            return View(result);
        }

        public ActionResult RealtyBlock()
        {
            var realty = _realtyRepository.Read().OrderByDescending(x => x.CreatedAt).Take(4).ToList();
            return View(realty);

        }

        public ActionResult AnnouncementBlock()
        {
            var query= _announcementRepository.ReadActivesQuery().OrderByDescending(x => x.CreatedAt).Where(x => x.SectionId != 3).Take(4);
            var list = query.ToList();
            return View(list);
            
        }

        public ActionResult AutoBlock(){
            
            var query = _announcementRepository.ReadActivesQuery().OrderByDescending(x => x.CreatedAt).Where(x => x.SectionId == 3).Take(4);
            var auto = query.ToList();
            return View(auto);
        }

        public ActionResult AnnouncementList(FilterParams filterParams = null)
        {
            var popularAnnouncements = _popularAnnoucementRepository.Read();
            List<Announcement> announcements = new List<Announcement>();
            foreach(var popularAnnouncement in popularAnnouncements)
            {
                announcements.Add(popularAnnouncement.Announcement);
            }
            var announcementsToSort = _announcementRepository.Sort(announcements.AsQueryable(), filterParams.SortOrder, filterParams.SortOptions, filterParams.SectionId,
                                     filterParams.SubsectionId, filterParams.CategoryId);
            ViewBag.Categories = _categoryRepository.Read(); 
            ViewBag.FilterParams = filterParams;
            return View("AnnouncementList", announcementsToSort.ToPagedList((filterParams.CurrentPage.HasValue) ? filterParams.CurrentPage.Value : 1, filterParams.PageSize));
        }

    }
}

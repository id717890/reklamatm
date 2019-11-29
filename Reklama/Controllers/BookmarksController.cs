﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository.Admin;
using Domain.Repository.Announcements;
using Domain.Repository.Realty;
using Domain.Entity.Realty;
using Reklama.Models.SortModels;
using WebMatrix.WebData;
using PagedList;
using Reklama.Models;
using Domain.Repository.Catalogs;
using System.Web.Security;
using Domain.Repository.Shared;

namespace Reklama.Controllers
{
    [Authorize]
    public class BookmarksController : _BaseController
    {
        private ReklamaContext rc = new ReklamaContext();

        private IAnnouncementBookmarkRepository _bookmarkRepository;
        private IAnnouncementRepository _announcementRepository;
        private IRealtyRepository _realtyRepository;
        private IRealtyBookmarkRepository _realtyBookmarksRepository;
        private readonly IConfigRepository _configRepository;
        private readonly IProductBookmarkRepository _productBookmarkRepository;
        private readonly IProfileRepository _profileRepository;

        public BookmarksController(
            IAnnouncementBookmarkRepository bookmarkRepository,
            IAnnouncementRepository announcementRepository,
            IConfigRepository configRepository,
            IRealtyBookmarkRepository realtyBookmarksRepository,
            IRealtyRepository realtyRepository, IProductBookmarkRepository productBookmarkrepository,
            IProfileRepository profileRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _bookmarkRepository.Context = rc;

            _announcementRepository = announcementRepository;
            _announcementRepository.Context = rc;

            _configRepository = configRepository;
            _configRepository.Context = rc;

            _realtyBookmarksRepository = realtyBookmarksRepository;
            _realtyBookmarksRepository.Context = rc;

            _realtyRepository = realtyRepository;
            _realtyRepository.Context = rc;

            _productBookmarkRepository = productBookmarkrepository;
            _productBookmarkRepository.Context = rc;

            _profileRepository = profileRepository;
            _profileRepository.Context = rc;
        }


        //
        // GET: /Bookmarks/Announcements

        public ActionResult Announcements(PagerSortModel sortModel = null)
        {
            var announcements = _bookmarkRepository.ReadAnnouncementsByUser(WebSecurity.CurrentUserId).Where(a => a.IsActive == true);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои закладки";

            return View(announcements.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
        }

        //
        // GET: /Bookmarks/Realty

        public ActionResult Realty(PagerSortModel sortModel = null)
        {
            var realty = _realtyBookmarksRepository.ReadRealtyByUser(WebSecurity.CurrentUserId).Where(r => r.IsActive == true);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои закладки";

            return View(realty.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
        }


        public ActionResult Products(PagerSortModel sortModel = null)
        {
            var products = _productBookmarkRepository.ReadProductsByUser(WebSecurity.CurrentUserId).Where(p => p.IsActive == true);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои закладки";

            return View(products.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
        }


        public ActionResult MyAnnouncementsMobile(PagerSortModel sortModel = null)
        {
            //var myAnnouncements = _announcementRepository.ReadByUser(_announcementRepository.Read(), WebSecurity.CurrentUserId);
            ViewBag.UpTimeAnnouncement = int.Parse(_configRepository.ReadByName("UpTimeAnnouncement").Value);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои объявления";
            ViewBag.IsAdmin = false;
            try
            {
                var user = _profileRepository.Read(WebSecurity.CurrentUserId);
                if (user != null)
                {
                    ViewBag.UserName = user.Name;
                    ViewBag.UserSurName = user.Surname;
                    ViewBag.Site = user.Site;
                    ViewBag.Skype = user.Skype;
                    ViewBag.Icq = user.Icq;
                }
            } catch { }
            if (Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
            {
                ViewBag.IsAdmin = true;
                var model = _realtyRepository.Read().OrderByDescending(x => x.CreatedAt);
                return View("MyAnnouncementsMobile", model.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
            } else
            {
                var model = _realtyRepository.Read().Where(x => x.UserId == WebSecurity.CurrentUserId).OrderByDescending(x => x.CreatedAt);
                return View("MyAnnouncementsMobile", model.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
            }
        }

        //
        // GET: /Bookmarks/MyAnnouncements
        public ActionResult MyAnnouncements(PagerSortModel sortModel = null)
        {
            var myAnnouncements = _announcementRepository.ReadByUser(_announcementRepository.Read(),
                                                                     WebSecurity.CurrentUserId);
            ViewBag.UpTimeAnnouncement = int.Parse(_configRepository.ReadByName("UpTimeAnnouncement").Value);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои объявления";
            return View(myAnnouncements.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
        }

        //
        // GET: /Bookmarks/MyRealty
        public ActionResult MyRealty(PagerSortModel sortModel = null)
        {
            var myRealty = _realtyRepository.ReadByUser(_realtyRepository.Read(),
                                                                     WebSecurity.CurrentUserId);
            ViewBag.UpTimeRealty = int.Parse(_configRepository.ReadByName("UpTimeRealty").Value);
            ViewBag.SortModel = sortModel;
            ViewBag.Title = "Мои объявления неджвижимости";
            return View(myRealty.ToPagedList(sortModel.CurrentPage.Value, sortModel.PageSize));
        }

        protected override void Dispose(bool disposing)
        {
            rc.Dispose();
            base.Dispose(disposing);
        }
    }
}

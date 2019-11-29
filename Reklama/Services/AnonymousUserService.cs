﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Repository.Other;
using Ninject;
using Reklama.Infrastructure;
using Reklama.Models;
using Reklama.Models.Other;

namespace Reklama.Services
{
    public class AnonymousUserService
    {
        [Inject]
        private readonly IComputerRepository _computerRepository;
        //[Inject]
        //private readonly IComputerAnnouncementRefRepository _computerAnnouncementRefRepository;

        public AnonymousUserService()
        {
            _computerRepository = DependencyResolver.Current.GetService<IComputerRepository>();
            _computerRepository.Context = new ReklamaContext();
        }

        public bool IsUserCanEdit(int announcementID)
        {
            var cookieName = "ann" + announcementID;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            //if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
            //{
            //    var arr = cookie.Value.Split(',');
            //    return arr.Contains(announcementID.ToString());
            //}

            return cookie != null && !String.IsNullOrEmpty(cookie.Value) && cookie.Value == announcementID.ToString();
            //var compKey = Domain.Utils.FingerPrint.Value();
            //return _computerRepository.Read()
            //        .Any(
            //            q =>
            //                q.Key.Equals(compKey) &&
            //                q.ComputerAnnouncementRefs.Any(w => w.AnnouncementsId == announcementID));
        }

        public bool IsUserCanEditRealty(int realtyID)
        {
            var cookieName = "realty" + realtyID;
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            //if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
            //{
            //    var arr = cookie.Value.Split(',');
            //    return arr.Contains(realtyID.ToString());
            //}

            return cookie != null && !String.IsNullOrEmpty(cookie.Value) && cookie.Value == realtyID.ToString();
            //var compKey = Domain.Utils.FingerPrint.Value();
            //return _computerRepository.Read()
            //        .Any(
            //            q =>
            //                q.Key.Equals(compKey) &&
            //                q.ComputerRealtyRefs.Any(w => w.RealtyId == realtyID));
        }
    }
}
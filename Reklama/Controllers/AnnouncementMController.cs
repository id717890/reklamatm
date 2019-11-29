using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Domain.Entity.Announcements;
using Domain.Enums;
using Domain.Repository.Admin;
using Domain.Repository.Announcements;
using LinqKit;
using Reklama.Models;
using Reklama.Models.AnnouncementsM;
using WebMatrix.WebData;

namespace Reklama.Controllers
{
    public class AnnouncementMController : Controller
    {
        private readonly ReklamaContext rc = new ReklamaContext();
        private readonly IAnnouncementRepository announcementRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IConfigRepository configRepository;
        public const int ItemsPerPage = 20;

        public AnnouncementMController(IAnnouncementRepository announcementRepository,
            ISectionRepository sectionRepository, IConfigRepository configRepository)
        {
            this.announcementRepository = announcementRepository;
            this.sectionRepository = sectionRepository;
            this.configRepository = configRepository;
            this.configRepository.Context = rc;
            this.sectionRepository.Context = rc;
            this.announcementRepository.Context = rc;
        }

        public ViewResult Index(GetSearchFilterModel model)
        {
            BindFilterCookie(model);
            IList<AnnouncementView> annList = GetFilteredItems(model);
            IList<SectionViewModel> sectionsList = GetSectionViewModels();

            var result = new AnnouncementIndexViewModel
            {
                SectionsList = sectionsList,
                AnnouncementList = annList,
                SearchString = model.SearchStr, // todo or get from cokie
                UserName = User.Identity.Name, //todo get Current User Id
                UserPhone = "+375333148904", //todo get  Curent User phone
                CurrentRegion = model.Region,
                CurrentSectionId = model.SectionId
            };
            SetFiltersCookie(model);

            return View(result);
        }

        [HttpPost]
        public JsonResult GetLatestItems(PostSearchFilterModel model)
        {
            var result = new object();
            if (ModelState.IsValid)
            {
                result = GetFilteredItems(model);
                SetFiltersCookie(model);
            }
            else
            {
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetAnnouncement(int id)
        {
            Announcement ann = announcementRepository.Read(id);
            AnnouncementView annView = null;
            if (ann != null)
            {
                annView = new AnnouncementView
                {
                    Id = ann.Id,
                    Currency = ann.Currency,
                    Description = ann.Description,
                    Images = ann.Images.Select(img => new ImageViewModel
                    {
                        Link = img.Link,
                        IsTitular = img.IsTitular,
                        CreatedAt = img.CreatedAt
                    }).ToList(),
                    PhoneNumber = ann.Phone,
                    Price = ann.Price,
                    PublishDate = ann.CreatedAt.ToString(),
                    ViewsCount = ann.ViewsCount
                };
            }

            return Json(annView, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult CreateAnnouncement()
        {
            return View();
        }


        [HttpPost]
        public JsonResult CreateAnnouncement(AnnouncementEdit modelAnnouncementEdit)
        {
            var model = new Announcement();
            string imageNames = SaveImages(modelAnnouncementEdit);
            model = MapAnnouncement(modelAnnouncementEdit);
            int newAnnId = announcementRepository.Save(model, imageNames);

            return Json(new {AnnouncementId = newAnnId});
        }

        private string SaveImages(AnnouncementEdit announcementEdit)
        {
            var imageUploaders = new List<ImageUploader>();
            foreach (HttpPostedFileBase httpPostedFileBase in announcementEdit.Images)
            {
                imageUploaders.Add(new ImageUploader(httpPostedFileBase));
            }
            foreach (ImageUploader uploader in imageUploaders)
            {
                uploader.Convert(530, 350);
                uploader.Save("users");
                uploader.Convert(ProjectConfiguration.Get.AnnouncementImageWidth,
                    ProjectConfiguration.Get.AnnouncementImageHeight);
                uploader.Save("announcement_thumb");
            }

            string[] names = imageUploaders.Select(imgUp => imgUp.UniqueName).ToArray();
            return string.Join(",", names);
        }

        private Announcement MapAnnouncement(AnnouncementEdit announcementEdit)
        {
            AnnouncementEdit a = announcementEdit;
            DateTime now = DateTime.Now;


            string name = a.Description.Length > 180 ? a.Description.Substring(0, 180) : a.Description;
            string smalDescr = a.Description.Length > 500 ? a.Description.Substring(0, 500) : a.Description;

            var ann = new Announcement
            {
                Name = name,
                SectionId = a.SectionId,
                SmallDescription = smalDescr,
                SubsectionId = 169, //todo
                CategoryId = 1,
                Phone = a.PhoneNumber,
                Price = a.Price,
                CurrencyId = a.CurrencyId,
                ContactEmail = null,
                ContactName = null,
                CreatedAt = now,
                CityId = a.Region == Regions.All ? null : (int?) ((int) a.Region),
                UpTime = now,
                ExpiredAt = DateTime.Now.AddDays(int.Parse(configRepository.ReadByName("ExpiredAtAnnouncement").Value)),
                IsActive = true,
                UserId = WebSecurity.CurrentUserId,
                ViewsCount = 0,
                IsDisplayPhone = true,
                //Description = Helper.RemoveTextFromText(model.Description, "width", ";");
                Description = a.Description
            };

            return ann;
        }

        #region[Temp Models]

        public abstract class BaseSearchFilterModel
        {
            public string SearchStr { get; set; }

            public Regions Region { get; set; }

            public int SectionId { get; set; }
        }

        public class PostSearchFilterModel : BaseSearchFilterModel
        {
            public bool IsNewSearch { get; set; }

            public int? SkipCount { get; set; }
        }

        public class GetSearchFilterModel : BaseSearchFilterModel
        {
        }

        #endregion

        #region[Temp private methodes]

        private string[] StringToArray(string input)
        {
            var trimmer = new Regex(@"\s\s+");
            input = trimmer.Replace(input, " ");
            string[] stringList = input.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return stringList;
        }

        private Expression<Func<Announcement, bool>> GetLikeExpression(string searchStr)
        {
            string[] searchWords = StringToArray(searchStr);
            Expression<Func<Announcement, bool>> likeExpression = PredicateBuilder.True<Announcement>();
            likeExpression.Or(a => a.Description.Contains(searchStr));
            foreach (string searchItem in searchWords)
            {
                // var searchPattern = "%" + searchItem + "%";
                likeExpression = likeExpression.And(a => a.Description.Contains(searchItem));
            }
            return likeExpression;
        }

        private IList<AnnouncementView> GetFilteredItems(BaseSearchFilterModel filter)
        {
            IQueryable<Announcement> query = announcementRepository.ReadActivesQuery();

            if (filter.Region != Regions.All)
            {
                query = query.Where(a => a.City != null && a.City.Id == (int) filter.Region);
            }

            if (filter.SectionId != 0)
            {
                query = query.Where(a => a.Section != null && a.Section.Id == filter.SectionId);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchStr))
            {
                query = query.AsExpandable().Where(GetLikeExpression(filter.SearchStr));
            }

            query = query.OrderByDescending(a => a.UpTime);
            if (filter is PostSearchFilterModel)
            {
                var postFilter = filter as PostSearchFilterModel;

                if (!postFilter.IsNewSearch && postFilter.SkipCount.HasValue)
                {
                    query = query.Skip(postFilter.SkipCount.Value);
                }
            }
            query = query.Take(ItemsPerPage);
            List<AnnouncementView> list = query.Select(a => new AnnouncementView
            {
                Id = a.Id,
                Currency = a.Currency,
                Description = a.Description,
                Images = a.Images.Select(img => new ImageViewModel
                {
                    Link = img.Link,
                    IsTitular = img.IsTitular,
                    CreatedAt = img.CreatedAt
                }).ToList(),
                PhoneNumber = a.Phone,
                Price = a.Price,
                PublishDate = a.CreatedAt.ToString(),
                ViewsCount = a.ViewsCount
            })
                .ToList();
            list.ForEach(a => a.PublishDate = DateTime.Parse(a.PublishDate).ToString("s"));
            return list;
        }


        private IList<SectionViewModel> GetSectionViewModels()
        {
            List<SectionViewModel> sections = sectionRepository.GetAsQueryable().Select(s => new SectionViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return sections;
        }

        private void SetFiltersCookie(BaseSearchFilterModel filter)
        {
            Response.Cookies.Set(new HttpCookie("Rk_Region", filter.Region.ToString())
            {
                Expires = DateTime.Now.AddYears(1)
            });
            Response.Cookies.Set(new HttpCookie("Rk_SectionId", filter.SectionId.ToString())
            {
                Expires = DateTime.Now.AddYears(1)
            });
            Response.Cookies.Set(new HttpCookie("Rk_SearchStr", HttpUtility.UrlEncode(filter.SearchStr))
            {
                Expires = DateTime.Now.AddYears(1)
            });
        }

        private void BindFilterCookie(BaseSearchFilterModel filter)
        {
            if (filter.Region == Regions.All)
            {
                HttpCookie cookie = Request.Cookies.Get("Rk_Region");
                if (cookie != null)
                {
                    filter.Region = (Regions) Enum.Parse(typeof (Regions), cookie.Value);
                }
            }
            if (filter.SectionId == 0)
            {
                HttpCookie cookie = Request.Cookies.Get("Rk_SectionId");
                if (cookie != null)
                {
                    filter.SectionId = int.Parse(cookie.Value);
                }
            }

            if (string.IsNullOrEmpty(filter.SearchStr))
            {
                HttpCookie cookie = Request.Cookies.Get("Rk_SearchStr");
                if (cookie != null)
                {
                    filter.SearchStr = HttpUtility.UrlDecode(cookie.Value);
                }
            }
        }

        #endregion
    }
}

public static class DateTimeExtensions
{
    public static DateTime TimeSpanBefore(this DateTime date)
    {
        return date.AddDays(1);
    }
}
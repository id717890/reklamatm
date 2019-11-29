using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Reklama.ViewModels.Filters
{
    public class FiltersViewModel
    {
        public int? CityId { get; set; }
        public string Description { get; set; }
        public int? Rooms { get; set; } = 1;
        public int? SquareFrom { get; set; }
        public int? SquareTo { get; set; }
        public int? LevelFrom { get; set; }
        public int? LevelTo { get; set; }
        public bool IsFiltered { get; set; } = false;
        public int DirectionSort { get; set; } = 2;
        public int FieldSort { get; set; } = 2;

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<Domain.Entity.Realty.Realty> Realties { get; set; }

        public List<SelectListItem> FlorsList()
        {
            var result = new List<SelectListItem>();
            for (int i = 1; i <= 25; i++)
            {
                result.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            return result;
        }
    }
}

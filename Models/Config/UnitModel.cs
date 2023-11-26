using System.Collections.Generic;
using System.Linq;
using Infrastructure;

namespace Models.Config
{
    public class UnitModel : BaseModel
    {
        public bool IsDefault { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public double Coefficient { get; set; } = 1;
        public string System { get; set; }
        public string Template { set; get; }
    }


    public static class UnitModelExtension
    {
        public static string GetSystem(this List<UnitModel> units, Enums.UnitCategory cat)
        {
            return units.FirstOrDefault(x => x.Category == cat.ToString())?.System ?? "UNK";
        }
    }
}
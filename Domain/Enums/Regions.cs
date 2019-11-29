using System.ComponentModel;

namespace Domain.Enums
{
    public enum Regions
    {
        [Description("Везде")]
        All = 0,
        [Description("Lebap(Лебап)")]
        Lb = 22,
        [Description("Mary(Мары)")]
        Mr = 17,
        [Description("Balkan(Балкан)")]
        Bn = 23,
        [Description("Dašoguz(Дашогуз)")]
        Dz = 2,
        [Description("Aśgabat(Ашхабат)")]
        Ag = 1
    }
}
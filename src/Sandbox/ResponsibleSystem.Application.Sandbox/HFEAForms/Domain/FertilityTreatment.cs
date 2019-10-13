
using System.ComponentModel;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public enum FertilityTreatment
    {
        [Description("IVF/ICSI Short")]
        Ivf_Short = 0,

        [Description("IVF/ICSI Long")]
        Ivf_Long = 1,

        [Description("IVF/ICSI Antagonist")]
        Ivf_Antagonist = 2,

        [Description("IVF/ICSI Freeze All Short")]
        Ivf_Icsi_Short = 3,

        [Description("IVF/ICSI Freeze All Long")]
        Ivf_Icsi_Long = 4,

        [Description("IVF/ICSI Freeze All Antagonist")]
        Ivf_Icsi_Antagonist = 5,

        [Description("OI/IUI")]
        Iui = 6,

        [Description("IVF/ICSI Natural")]
        Ivf_Natural = 7,

        [Description("IVF/ICSI Egg Recipient")]
        Ivf_Egg_Recipient = 8,

        [Description("IVF/ICSI Freeze All Natural")]
        Ivf_Icsi_Natural = 9,

        [Description("IVF/ICSI Freeze All Egg Recipient")]
        Ivf_Icsi_Egg_Recipient = 10,

        [Description("Frozen Embryo Transfer")]
        Frozen_Embryo_Transfer = 11,

        [Description("Fertility Preservation")]
        Fertility_Preservation = 12
    }
}

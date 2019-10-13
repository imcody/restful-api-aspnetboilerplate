using System.Text;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public class FertilityTreatmentMultiSelect
    {
        public bool Ivf_Short { get; set; }
        public bool Ivf_Long { get; set; }
        public bool Ivf_Antagonist { get; set; }
        public bool Ivf_Icsi_Short { get; set; }
        public bool Ivf_Icsi_Long { get; set; }
        public bool Ivf_Icsi_Antagonist { get; set; }
        public bool Iui { get; set; }
        public bool Ivf_Natural { get; set; }
        public bool Ivf_Egg_Recipient { get; set; }
        public bool Ivf_Icsi_Natural { get; set; }
        public bool Ivf_Icsi_Egg_Recipient { get; set; }
        public bool Frozen_Embryo_Transfer { get; set; }
        public bool Fertility_Preservation { get; set; }

        public override string ToString()
        {
            var res = new StringBuilder();
            var sep = "";

            if (Ivf_Short)
            {
                res.Append("IVF/ICSI Short");
                sep = ", ";
            }

            if (Ivf_Long)
            {
                res.Append(sep + "IVF/ICSI Long");
                sep = ", ";
            }

            if (Ivf_Antagonist)
            {
                res.Append(sep + "IVF/ICSI Antagonist");
                sep = ", ";
            }

            if (Ivf_Natural)
            {
                res.Append(sep + "IVF/ICSI Natural");
                sep = ", ";
            }

            if (Ivf_Egg_Recipient)
            {
                res.Append(sep + "IVF/ICSI Egg Recipient");
                sep = ", ";
            }

            if (Ivf_Icsi_Short)
            {
                res.Append(sep + "IVF/ICSI Freeze All Short");
                sep = ", ";
            }

            if (Ivf_Icsi_Long)
            {
                res.Append(sep + "IVF/ICSI Freeze All Long");
                sep = ", ";
            }

            if (Ivf_Icsi_Antagonist)
            {
                res.Append(sep + "IVF/ICSI Freeze All Antagonist");
                sep = ", ";
            }

            if (Ivf_Icsi_Natural)
            {
                res.Append(sep + "IVF/ICSI Freeze All Natural");
                sep = ", ";
            }

            if (Ivf_Icsi_Egg_Recipient)
            {
                res.Append(sep + "IVF/ICSI Freeze All Egg Recipient");
                sep = ", ";
            }

            if (Iui)
            {
                res.Append(sep + "OI/IUI");
                sep = ", ";
            }

            if (Frozen_Embryo_Transfer)
            {
                res.Append(sep + "Frozen Embryo Transfer");
                sep = ", ";
            }

            if (Fertility_Preservation)
            {
                res.Append(sep + "Fertility Preservation");
            }

            return res.ToString();
        }
    }
}

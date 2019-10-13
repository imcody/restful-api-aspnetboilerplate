using System.Text;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public class MaritalStatusMultiSelect
    {
        public bool Married { get; set; }
        public bool Unmarried { get; set; }

        public override string ToString()
        {
            var res = new StringBuilder();
            var sep = "";
            if (Married)
            {
                res.Append("Married");
                sep = ", ";
            }

            if (Unmarried)
            {
                res.Append(sep + "Unmarried");
            }
            return res.ToString();
        }
    }
}

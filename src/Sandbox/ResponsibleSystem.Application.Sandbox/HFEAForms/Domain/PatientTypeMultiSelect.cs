using System.Text;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public class PatientTypeMultiSelect
    {
        public bool SingleFemalePatient { get; set; }
        public bool FemalePatient { get; set; }
        public bool FemalePartner { get; set; }
        public bool MalePartner { get; set; }

        public override string ToString()
        {
            var res = new StringBuilder();
            var sep = "";
            if (SingleFemalePatient)
            {
                res.Append(sep + "Female Only (patient)");
                sep = ", ";
            }

            if (FemalePatient)
            {
                res.Append(sep + "Female (patient)");
                sep = ", ";
            }

            if (FemalePartner)
            {
                res.Append(sep + "Female (partner)");
                sep = ", ";
            }

            if (MalePartner)
            {
                res.Append(sep + "Male (partner)");
            }

            return res.ToString();
        }
    }
}

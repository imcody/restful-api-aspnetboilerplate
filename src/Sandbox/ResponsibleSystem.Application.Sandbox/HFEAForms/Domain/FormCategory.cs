using System.ComponentModel;

namespace ResponsibleSystem.Sandbox.HFEAForms.Domain
{
    public enum FormCategory
    {
        [Description("Disclosure of Information")]
        DisclosureOfInformation = 0,

        [Description("Parenthood")]
        Parenthood = 1,

        [Description("Treatment & Storage")]
        TreatmentAndStorage = 2,

        [Description("Donation")]
        Donation = 3,

        [Description("Surrogacy")]
        Surrogacy = 4,

        [Description("Withdraw / Lack of Consent")]
        WithdrawLackOfConsent
    }
}

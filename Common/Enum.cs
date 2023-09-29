using System.ComponentModel;

namespace SampleProject.Common
{
    public enum GenderEnum
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
        [Description("Diverse")]
        Diverse = 3
    }

    public enum ReasonEnum
    {
        [Description("Issue")]
        Issue = 1,
        [Description("Question")]
        Question = 2,
        [Description("Suggestion")]
        Suggestion = 3,
        [Description("Feedback")]
        Feedback = 4
    }

    public enum ActionStatus
    {
        Successfull = 1,
        Error = 2,
        LoggedOut = 3,
        Unauthorized = 4,
        AlreadyExist = 5
    }
}

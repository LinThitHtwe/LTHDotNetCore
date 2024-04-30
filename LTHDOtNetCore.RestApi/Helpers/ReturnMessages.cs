using static LTHDOtNetCore.RestApi.Enums.Enum;

namespace LTHDOtNetCore.RestApi.Helpers
{
    public class ReturnMessages
    {
        public static string ManipulatedStatusMessage(int result, ManipulationMethods manipulationMethods)
        {
            string status = result > 0 ? "Successfully" : "Fail";

            return manipulationMethods switch
            {
                (ManipulationMethods.create) => ($"Create {status}"),
                (ManipulationMethods.update) => ($"Update {status}"),
                (ManipulationMethods.delete) => ($"Delete {status}"),
                _ => ("Something Went Wrong"),
            };
        }
    }
}

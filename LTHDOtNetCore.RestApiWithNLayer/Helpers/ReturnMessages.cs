using LTHDOtNetCore.RestApiWithNLayer.Enums;

namespace LTHDOtNetCore.RestApiWithNLayer.Helpers
{
    public class ReturnMessages
    {
        public string ManipulatedStatusMessage(int result, ManipulationMethods manipulationMethods)
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

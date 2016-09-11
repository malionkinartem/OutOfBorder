using System;
using OOB.ServiceApi.Attributes;

namespace OOB.ServiceApi.UserServices
{
    [GlobalErrorHandler(typeof(ErrorHandler))]
    public class UserService : IUserService
    {
        public string GetData(int value)
        {
            throw new Exception("test exc");
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

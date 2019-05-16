using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi
{
    public class HomeController: BaseApiController
    {
        public BaseResult Get()
        {
            try
            {
                return new BaseResult() { IsSuccess = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

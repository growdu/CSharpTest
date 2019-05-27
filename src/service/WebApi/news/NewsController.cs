using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi.news
{
    public class NewsController: ApiController
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Models.Model
{
    public class APIResult
    {
        public bool IsSuccess { get; set; }

        public APIResult()
        {
        }

        public APIResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}

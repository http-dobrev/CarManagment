using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagment.Logic.Services
{
    public class ServiceResult
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<string> Errors => _errors;

        private readonly List<string> _errors = new();

        private ServiceResult(bool isSuccess, IEnumerable<string>? errors = null)
        {
            IsSuccess = isSuccess;

            if (errors != null)
            {
                _errors.AddRange(errors);
            }
        }

        public static ServiceResult Success()
        {
            return new ServiceResult(true);
        }

        public static ServiceResult Failure(params string[] errors)
        {
            return new ServiceResult(false, errors);
        }

        public static ServiceResult Failure(IEnumerable<string> errors)
        {
            return new ServiceResult(false, errors);
        }
    }
}

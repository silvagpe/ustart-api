using System;
using System.Collections.Generic;

namespace UStart.Domain.Workflows.ErrorTracking
{
    public abstract class ErrorTracker
    {
        public bool isDevelopment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        private readonly List<Error> _errors;

        public ErrorTracker()
        {
            _errors = new List<Error>();
        }

        protected List<Error> Errors => _errors;

        public void AddErrors(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddError(Error error)
        {
            _errors.Add(error);
        }

        public void AddError(string property, string message, Object value = null)
        {
            _errors.Add(new Error(property, message, value));
        }


        public List<Error> GetErrors()
        {
            return Errors;
        }

        public bool IsValid()
        {
            return Errors.Count == 0;
        }


        public void AddException(string entiy, Exception exp)
        {
            if (this.isDevelopment())
            {
                AddError(entiy, exp.Message);
                if (exp.InnerException != null)
                {
                    AddError(entiy, exp.InnerException.Message);
                }
            }
            else
            {
                throw exp;
            }
        }
    }

}
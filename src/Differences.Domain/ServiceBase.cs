using System;
using System.Collections.Generic;
using System.Text;
using Differences.Common.Resources;
using Microsoft.Extensions.Localization;

namespace Differences.Domain
{
    public abstract class ServiceBase
    {
        private readonly IStringLocalizer<Errors> _localizer;

        public ServiceBase(IStringLocalizer<Errors> localizer)
        {
            _localizer = localizer;
        }

        protected string GetLocalizedResource(string name)
        {
            return _localizer.GetString(name);
        }
    }
}

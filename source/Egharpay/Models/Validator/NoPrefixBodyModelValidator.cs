using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Egharpay.Models.Validator
{
    public class NoPrefixBodyModelValidator : IBodyModelValidator
    {
        private readonly IBodyModelValidator _innerBodyModelValidator;

        public NoPrefixBodyModelValidator(IBodyModelValidator innerBodyModelValidator)
        {
            _innerBodyModelValidator = innerBodyModelValidator ?? throw new ArgumentNullException(nameof(innerBodyModelValidator));
        }

        public bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix)
        {
            return _innerBodyModelValidator.Validate(model, type, metadataProvider, actionContext, string.Empty);
        }
    }
}
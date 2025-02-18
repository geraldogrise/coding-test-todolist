using Todo.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Todo.Domain.Aggreagates.Core
{
    public abstract class EntityCore<T> : AbstractValidator<T> where T : EntityCore<T>
    {
        public FluentValidation.Results.ValidationResult ValidationResult { get; protected set; }

        protected EntityCore()
        {
            ValidationResult = new FluentValidation.Results.ValidationResult();
        }

        public bool IsValid(T entity)
        {
            ValidationResult = Validate(entity);
            if (!this.ValidationResult.IsValid)
            {
                return this.ValidationResult.IsValid;
            }

            var resultadoValidacao = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var contexto = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, contexto, resultadoValidacao, true);

            if (resultadoValidacao.Count > 0)
            {
                StringBuilder msgs = new StringBuilder();
                foreach (var validacao in resultadoValidacao)
                {
                    msgs.Append(validacao.ToString() + "<br/>");
                }
                throw new ValidacaoException(msgs.ToString());
            }
            return true;
        }

        public void AddValidationResult(FluentValidation.Results.ValidationResult validationResult)
        {
            if (validationResult != null)
                foreach (var error in validationResult.Errors)
                    ValidationResult.Errors.Add(error);
        }



    }
}

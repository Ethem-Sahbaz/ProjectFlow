using FluentValidation;

namespace ProjectFlow.Api.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var arg = context.Arguments.FirstOrDefault(a => a is T) as T;

        if (arg is null)
        {
            return Results.BadRequest("Invalid request payload");
        }

        var validationResult = await _validator.ValidateAsync(arg);

        if (!validationResult.IsValid)
        {
            var dictionary = new Dictionary<string, string[]>();

            foreach (var result in validationResult.Errors)
            {
                dictionary.Add(result.PropertyName, new[] { result.ErrorMessage });
            }

            return Results.ValidationProblem(dictionary);
        }

        return await next(context);

    }
}

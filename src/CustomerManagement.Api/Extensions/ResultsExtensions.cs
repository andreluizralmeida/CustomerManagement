namespace CustomerManagement.Api.Extensions;

using System.Globalization;
using System.Linq;
using Ardalis.GuardClauses;
using CustomerManagement.Domain.Common;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Extension methods for <see cref="Result"/>
/// </summary>
public static class ResultsExtensions
{
    private const string Type = "type";

    /// <summary>
    /// Try get the result value object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    public static IActionResult AsActionResult<T>(this Result<T> result, string routeName = default, object routeValues = default)
    {
        Guard.Against.Null(result, nameof(result));

        if (result.IsFailed || result.Value is null)
        {
            return GetError(result.ToResult());
        }

        if (result.Value is Guid)
        {
            var created = result.Successes.Find(success =>
                success.Metadata.ContainsKey(Type)
                && success.Metadata.ContainsValue("created"));

            return new CreatedAtRouteResult(routeName, routeValues, new { created.Message}) ;
        }

        return new OkObjectResult(result.Value);            
    }
    public static IActionResult AsActionResult(this Result result)
    {
        Guard.Against.Null(result, nameof(result));

        if (result.IsFailed)
        {
            return GetError(result);
        }

        return new NoContentResult();
    }

    /// <summary>
    /// Returns a BadRequest action result using ApplicationError format
    /// </summary>
    /// <param name="errorMessages"></param>
    /// <returns></returns>
    public static IActionResult BadRequestError(params string[] errorMessages)
    {
        return new BadRequestObjectResult(new ApplicationErrorCollection(errorMessages
            .Select(message => 
                new ApplicationError(
                    StatusCodes.Status400BadRequest.ToString(CultureInfo.InvariantCulture),
                    message: message
                    ))
            .ToArray()));
    }        

    private static ObjectResult GetError(Result result)
    {
        var notFound = result.Errors.Find(error =>
            error.Metadata.ContainsKey(Type)
            && error.Metadata.ContainsValue(Errors.NotFoundMetadata));

        if (notFound != null)
        {
            return new NotFoundObjectResult(new { notFound.Message });
        }

        var errorMessages = result.Errors.Select(e => e.Message).ToArray();
        return (ObjectResult)BadRequestError(errorMessages);
    }

}
using Agora.Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Agora.Common.Api;

/// <summary>
/// Base controller class that extends the functionality of ControllerBase in ASP.NET Core.
/// Provides standardized responses wrapped in an Envelope for consistency.
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// Creates an Ok (200) response with a standard envelope.
    /// </summary>
    /// <returns>An Ok (200) ActionResult with an empty envelope.</returns>
    protected new IActionResult Ok()
    {
        return base.Ok(Envelope.Ok());
    }

    /// <summary>
    /// Creates an Ok (200) response with a specific result wrapped in an envelope.
    /// </summary>
    /// <typeparam name="T">The type of the result to be included in the envelope.</typeparam>
    /// <param name="result">The result to include in the response.</param>
    /// <returns>An Ok (200) ActionResult with the specified result.</returns>
    protected IActionResult Ok<T>(T result)
    {
        return base.Ok(Envelope.Ok(result));
    }

    /// <summary>
    /// Creates a BadRequest (400) response with a specific error message wrapped in an envelope.
    /// </summary>
    /// <param name="errorMessage">The error message to include in the response.</param>
    /// <returns>A BadRequest (400) ActionResult with the specified error message.</returns>
    protected IActionResult BadRequest(string errorMessage)
    {
        return BadRequest(Envelope.Error(errorMessage));
    }
}
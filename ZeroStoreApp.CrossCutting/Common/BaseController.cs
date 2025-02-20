using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroStoreApp.CrossCutting.Common;

public abstract class BaseController : ControllerBase
{
    protected IActionResult Ok<T>(T data, string message, params object[] args) =>
        base.Ok(new ApiResponseWithData<T> 
        { 
            Data = data, 
            Success = true,
            Message = string.Format(message, args)
        });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult BadRequest(IEnumerable<ValidationFailure> errors, string message = "") =>
        base.BadRequest(new ApiResponse { Errors = errors, Success = false , Message = message});

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult Ok<T>(PaginatedList<T> pagedList, string message, params object[] args) =>
            base.Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Success = true,
                Message = string.Format(message, args)
            });
}

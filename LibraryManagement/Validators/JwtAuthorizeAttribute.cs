using LibraryManagement.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Validators;

public class JwtAuthorizeAttribute() : TypeFilterAttribute(typeof(JwtAuthorizationFilter));

using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Policy
{
    public class HasScopeRequirement: IAuthorizationRequirement
    {
        public string Issuer { get; set; }
        public string Scope { get; set; }

        public HasScopeRequirement(string issuer, string scope)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }

    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if(!context.User.HasClaim(c =>c.Type=="scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var scopes= context.User.FindFirst(c=>
            c.Type=="scope" && c.Issuer == requirement.Issuer)
                .Value.Split(' ');

            if (scopes.Any(s => s == requirement.Scope)) context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace PartyWebAppServer.Handlers;

public class IsAdminRequirement: IAuthorizationRequirement {
}

public class IsAdminHandler: AuthorizationHandler<IsAdminRequirement> {
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement) {
		if (context.User.HasClaim(claim => claim.Type == "IsAdmin" && claim.Value == "true")) context.Succeed(requirement);
		else context.Fail();
		
		Console.WriteLine(context.HasSucceeded);

		return Task.CompletedTask;
	}
}
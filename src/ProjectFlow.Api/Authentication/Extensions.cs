namespace ProjectFlow.Api.Authentication;

public static class Extensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "userid");

        if (Guid.TryParse(userId?.Value, out var id))
            return id;

        return default;
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace AlgoCode.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Create roles
        foreach (var role in Enum.GetValues(typeof(UserRoles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.ToString(),
                });
            }
        }

        // Seed admin user
        if (await _userManager.FindByNameAsync("admin") == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@app.com"
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin12345678*");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }

            await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin.ToString());

            Session defaultSession = new()
            {
                Title = "Default",
                UserId = adminUser.Id,
                IsActive = true
            };

            _context.Sessions.Add(defaultSession);
            await _context.SaveChangesAsync();
        }

        await SeedTagsAsync();
        await SeedProblemsAsync();
    }

    private async Task SeedTagsAsync()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure", "Data", "SeedData");
        if (!_context.Tags.Any())
        {
            var json = await File.ReadAllTextAsync(path + @"/tags.json");
            var tags = JsonConvert.DeserializeObject<List<Tag>>(json);

            if (tags is null) return;

            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();
        }
    }

    //TODO: Edit problem.json file, note that relationships are not being seeded
    private async Task SeedProblemsAsync()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure", "Data", "SeedData");

        if (!_context.Problems.Any())
        {
            var json = await File.ReadAllTextAsync(path + @"/problems.json");
            var products = JsonConvert.DeserializeObject<List<Problem>>(json);

            if (products is null) return;

            await _context.Problems.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }
    }

}
using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
        
    private Context context { get; set; }
        
    private readonly ILogger<CommunityController> _logger;

    public CommunityController(ILogger<CommunityController> logger, Context context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    [Route("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Community>>> GetAll([FromQuery] int userId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        if (user == null)
        {
            return Forbid();
        }
        var communities = await context.UserCommunities
            .Where(uc => uc.User.ID == userId)
            .Select(uc => uc.Community)
            .ToListAsync();
        if (communities == null)
        {
            return BadRequest();
        }
        return Ok(communities);
    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Community>> GetCommunity([FromQuery] int id)
    {
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == id);
        if (community == null)
        {
            return BadRequest();
        }
        return Ok(community);
    }

    [HttpPut]
    [Route("join")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> JoinCommunity([FromQuery] int userId, [FromQuery] int communityId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == communityId);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        var userCommunity = new UserCommunity
        {
            User = user,
            Community = community
        };
        context.UserCommunities.Add(userCommunity);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"User {user.Name} {user.Surname} joined community {community.Name}");
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("leave")]
    public async Task<ActionResult> LeaveCommunity([FromQuery] int userId, [FromQuery] int communityId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == communityId);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        var userCommunity = await context.UserCommunities
            .FirstOrDefaultAsync(uc => uc.User.ID == userId && uc.Community.ID == communityId);
        if (userCommunity == null)
        {
            return BadRequest("User is not a member of this community");
        }
        context.UserCommunities.Remove(userCommunity);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"User {user.Name} {user.Surname} left community {community.Name}");
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCommunity([FromBody] string communityName, [FromBody] int userId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        var community = new Community
        {
            Calendar = new Calendar(),
            Name = communityName,
            Creator = user,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };
        context.Communities.Add(community);
        var userCommunity = new UserCommunity
        {
            User = user,
            Community = community
        };
        try
        {
            await context.SaveChangesAsync();  
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteCommunity([FromQuery] int communityId)
    {
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == communityId);
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        context.Communities.Remove(community);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"Community {community.Name} successfully deleted");
    }
}


using codit_school_core_ms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly SchoolContext _context;

    public LoginController(SchoolContext context)
    {
        _context = context;
    }

    // GET: api/Login
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
    {
        return await _context.Logins.ToListAsync();
    }

    // GET: api/Login/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Login>> GetLogin(int id)
    {
        var login = await _context.Logins.FindAsync(id);
        if (login == null)
            return NotFound();

        return login;
    }

    // POST: api/Login/LoginUser
    [HttpPost("LoginUser")]
    public async Task<ActionResult> LoginUser([FromBody] LoginUserRequest request)
    {
        var login = await _context.Logins
            .FirstOrDefaultAsync(l => l.Username == request.UserName && l.Password == request.Password);

        if (login == null)
            return Unauthorized(new { message = "Invalid username or password" });

        // Login successful - return user info without password
        return Ok(new
        {
            message = "Login successful",
            userId = login.Id,
            userName = login.Username,
            userRole = login.UserRole,
            createdDate = login.CreatedDate
        });
    }

    // Alternative: GET method for login with parameters
    [HttpGet("LoginUser")]
    public async Task<ActionResult> LoginUserGet([FromQuery] string userName, [FromQuery] string password)
    {
        var login = await _context.Logins
            .FirstOrDefaultAsync(l => l.Username == userName && l.Password == password);

        if (login == null)
            return Unauthorized(new { message = "Invalid username or password" });

        // Login successful - return user info without password
        return Ok(new
        {
            message = "Login successful",
            userId = login.Id,
            userName = login.Username,
            userRole = login.UserRole,
            createdDate = login.CreatedDate
        });
    }

    // POST: api/Login
    [HttpPost]
    public async Task<ActionResult<Login>> PostLogin(Login login)
    {
        _context.Logins.Add(login);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLogin), new { id = login.Id }, login);
    }

    // PUT: api/Login/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLogin(int id, Login login)
    {
        if (id != login.Id)
            return BadRequest();

        _context.Entry(login).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Logins.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/Login/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLogin(int id)
    {
        var login = await _context.Logins.FindAsync(id);
        if (login == null)
            return NotFound();

        _context.Logins.Remove(login);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

// Request DTO for login
public class LoginUserRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
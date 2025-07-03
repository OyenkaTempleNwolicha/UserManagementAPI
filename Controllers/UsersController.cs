using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = _service.Get(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        _service.Create(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        if (id != user.Id) return BadRequest();
        _service.Update(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}

public class UserService
{
    private readonly List<User> _users = new List<User>();
    private int _nextId = 1;

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User? Get(int id)
    {
        return _users.Find(u => u.Id == id);
    }

    public void Create(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
    }

    public void Update(User user)
    {
        var index = _users.FindIndex(u => u.Id == user.Id);
        if (index != -1)
        {
            _users[index] = user;
        }
    }

    public void Delete(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }
}

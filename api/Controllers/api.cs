using api.Models;
using api.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class api : ControllerBase
    {
        private readonly Menu _menu;

        public api(Menu menu) =>
        _menu = menu;

        [HttpGet]
        public async Task<List<sandwich>> Get() =>
        await _menu.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<sandwich>> Get(string id)
        {
            var sandwich = await _menu.GetAsync(id);

            if (sandwich is null)
            {
                return NotFound();
            }

            return sandwich;
        }

        [HttpPost]
        public async Task<IActionResult> Post(sandwich newSandwich)
        {
            await _menu.CreateAsync(newSandwich);

            return CreatedAtAction(nameof(Get), new { id = newSandwich.Id }, newSandwich);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, sandwich updatedSandwich)
        {
            var book = await _menu.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedSandwich.Id = book.Id;

            await _menu.UpdateAsync(id, updatedSandwich);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var sandwich = await _menu.GetAsync(id);

            if (sandwich is null)
            {
                return NotFound();
            }

            await _menu.RemoveAsync(id);

            return NoContent();
        }
    }
}


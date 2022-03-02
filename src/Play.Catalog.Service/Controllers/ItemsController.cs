using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    // private static readonly List<ItemDto> items = new()
    // {
    //     new(Guid.NewGuid(), "Potion", "Restore a small amount of HP", 5, DateTimeOffset.UtcNow),
    //     new(Guid.NewGuid(), "Antidote", "Cures poision", 7, DateTimeOffset.UtcNow),
    //     new(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow)
    // };

    private readonly IItemsRepository _itemsRepository;

    public ItemsController(IItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
        return (await _itemsRepository.GetAllAsync()).Select(item => item.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        //  var item = (await itemsRepository.GetAsync(id)).AsDto();
        var item = await _itemsRepository.GetAsync(id);
        if (item is null) return NotFound();

        return item.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItem)
    {
        // var item = new ItemDto(Guid.NewGuid(), createItem.Name, createItem.Description, createItem.Price, DateTimeOffset.UtcNow);

        // items.Add(item);
        var item = new Item(
            name: createItem.Name,
            description: createItem.Description,
            price: createItem.Price,
            createdDate: DateTimeOffset.UtcNow);

        await _itemsRepository.CreateAsync(item);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, UpdateItemDto updateItemDto)
    {
        // var existingItem1 = items.SingleOrDefault(item => item.Id == id);
        var existingItem = await _itemsRepository.GetAsync(id);

        if (existingItem is null) return NotFound();

        // var updateItem = existingItem with
        // {
        //     Name = updateItemDto.Name,
        //     Description = updateItemDto.Description,
        //     Price = updateItemDto.Price
        // };

        // var index = items.FindIndex(existingItem => existingItem.Id == id);
        // items[index] = updateItem;
        existingItem.Name = updateItemDto.Name;
        existingItem.Description = updateItemDto.Description;
        existingItem.Price = updateItemDto.Price;

        await _itemsRepository.UpdateAsync(existingItem);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        // var index = items.FindIndex(existingItem => existingItem.Id == id);

        // if (index < 0) return NotFound();

        // items.RemoveAt(index);
        var existingItem = await _itemsRepository.GetAsync(id);
        if (existingItem is null) return NotFound();

        await _itemsRepository.RemoveAsync(existingItem.Id);

        return NoContent();
    }
}
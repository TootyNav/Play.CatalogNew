using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{

    private static readonly List<ItemDto> items = new(){
        new ItemDto(Guid.NewGuid(), "Potion", "Restor small amount of HP", 5, DateTimeOffset.Now),
        new ItemDto(Guid.NewGuid(), "AntiDote", "Curse poison", 7, DateTimeOffset.Now),
        new ItemDto(Guid.NewGuid(), "Bronze sward", "Deal a small amount of damage", 20, DateTimeOffset.Now)
    };

    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
        return items;
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
        var item = items.Find(x => x.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    // POST /items
    [HttpPost]
    public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
    {
        var item = new ItemDto(
            Id: Guid.NewGuid(),
            Name: createItemDto.Name,
            Description: createItemDto.Description,
            Price: createItemDto.Price,
            CreatedDate: DateTimeOffset.UtcNow
        );

        items.Add(item);

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
    {
        var existingItem = items.Find(x => x.Id == id);

        if (existingItem == null)
        {
            return NotFound();
        }

        var updateItem = existingItem with
        {
            Name = updateItemDto.Name,
            Description = updateItemDto.Description,
            Price = updateItemDto.Price,
        };

        var itemIndex = items.IndexOf(existingItem);
        items[itemIndex] = updateItem;

        return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = items.Find(x => x.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        items.Remove(item);

        return NoContent();
    }


    // private readonly IItemsRepository itemsRepository;

    // public ItemsController(IItemsRepository itemsRepository)
    // {
    //     this.itemsRepository = itemsRepository;
    // }

    // [HttpGet]
    // public async Task<IEnumerable<ItemDto>> GetAsync()
    // {
    //     var items = (await itemsRepository.GetAllAsync())
    //                 .Select(item => item.AsDto());
    //     return items;
    // }

    // // GET /items/{id}
    // [HttpGet("{id}")]
    // public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    // {
    //     var item = await itemsRepository.GetAsync(id);

    //     if (item == null)
    //     {
    //         return NotFound();
    //     }

    //     return item.AsDto();
    // }

    // // POST /items
    // [HttpPost]
    // public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
    // {
    //     var item = new Item
    //     {
    //         Name = createItemDto.Name,
    //         Description = createItemDto.Description,
    //         Price = createItemDto.Price,
    //         CreatedDate = DateTimeOffset.UtcNow
    //     };

    //     await itemsRepository.CreateAsync(item);

    //     return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    // }

    // // PUT /items/{id}
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
    // {
    //     var existingItem = await itemsRepository.GetAsync(id);

    //     if (existingItem == null)
    //     {
    //         return NotFound();
    //     }

    //     existingItem.Name = updateItemDto.Name;
    //     existingItem.Description = updateItemDto.Description;
    //     existingItem.Price = updateItemDto.Price;

    //     await itemsRepository.UpdateAsync(existingItem);

    //     return NoContent();
    // }

    // // DELETE /items/{id}
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteAsync(Guid id)
    // {
    //     var item = await itemsRepository.GetAsync(id);

    //     if (item == null)
    //     {
    //         return NotFound();
    //     }

    //     await itemsRepository.RemoveAsync(item.Id);

    //     return NoContent();
    // }
}
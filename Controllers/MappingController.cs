using Microsoft.AspNetCore.Mvc;

namespace DataMappingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataMappingController : ControllerBase
    {
        private static List<DataModel> dataStore = new List<DataModel>();

        // Получить все данные
        [HttpGet]
        public ActionResult<IEnumerable<DataModel>> GetAll()
        {
            return Ok(dataStore);
        }

        // Получить данные по ID
        [HttpGet("{id}")]
        public ActionResult<DataModel> GetById(int id)
        {
            var data = dataStore.FirstOrDefault(d => d.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // Добавить данные
        [HttpPost]
        public ActionResult<DataModel> Create([FromBody] DataModel newData)
        {
            newData.Id = dataStore.Count + 1;
            dataStore.Add(newData);
            return CreatedAtAction(nameof(GetById), new { id = newData.Id }, newData);
        }

        // Обновить данные
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] DataModel updatedData)
        {
            var index = dataStore.FindIndex(d => d.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            dataStore[index] = updatedData;
            updatedData.Id = id; // Убедитесь, что ID не изменяет
            return NoContent();
        }

        // Удалить данные
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var index = dataStore.FindIndex(d => d.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            dataStore.RemoveAt(index);
            return NoContent();
        }
    }

    public class DataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

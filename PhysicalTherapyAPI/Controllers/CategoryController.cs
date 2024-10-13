using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhysicalTherapyAPI.Models;
using PhysicalTherapyAPI.Repositories.Inplementation;

namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration configuration;

        public CategoryController(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories() 
        { 
            return Ok(_unitOfWork.CategoryRepository.GetAll());
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(string CategoryName, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img == null || img.Length == 0)
                { return BadRequest("No file uploaded."); }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Category");
                var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var photoUrl = $"{configuration["Images:Category"]}/{fileName}";
                Category category = new Category()
                {
                    Name = CategoryName,
                    PhotoUrl = photoUrl
                };
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.save();
                return Ok(category);
            }
            return BadRequest();

        }

        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromQuery] string? CategoryName, int id, IFormFile? img)
        {
            var DBCategory = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            if (DBCategory == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(CategoryName))
                {
                    DBCategory.Name = CategoryName;
                }

                if (img != null && img.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Category");
                    var fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(stream);
                    }
                    var photoUrl = $"{configuration["Images:Category"]}/{fileName}";
                    DBCategory.PhotoUrl = photoUrl;
                }
                _unitOfWork.CategoryRepository.Update(DBCategory);
                _unitOfWork.save();

                return Ok(DBCategory);
            }

            return BadRequest();
        }
        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategoryById(int Id)
        {
            if (ModelState.IsValid)
            {
                Category categoryFromDB = _unitOfWork.CategoryRepository.Get(E => E.Id == Id);
                if (categoryFromDB != null)
                {
                    categoryFromDB.IsDeleted = true;
                    _unitOfWork.CategoryRepository.Remove(categoryFromDB);
                    _unitOfWork.save();
                    return Ok();
                }
                return NotFound("Not Found Category With This Id");
            }
            return BadRequest(ModelState);
        }
    }
}

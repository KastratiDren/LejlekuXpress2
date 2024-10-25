using LejlekuXpress.Data.DTO;
using LejlekuXpress.Data.ServiceInterfaces;
using LejlekuXpress.Models;
using LejlekuXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace LejlekuXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly LogService _logService;

        public ProductController(IProductService service, LogService logService)
        {
            _service = service;
            _logService = logService;
        }

        #region Add
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(ProductDTO request)
        {
            try
            {
                var product = await _service.AddProduct(request);

                // Log success
                await _logService.CreateLog(new Log
                {
                    Action = "AddProduct",
                    Message = "Product added successfully"
                });

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log failure
                await _logService.CreateLog(new Log
                {
                    Action = "AddProduct",
                    Message = $"Error adding product: {ex.Message}"
                });

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get
        [HttpGet("get")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var result = await _service.GetProduct(id);

                if (result == null)
                {
                    await _logService.CreateLog(new Log
                    {
                        Action = "GetProduct",
                        Message = "Product not found"
                    });
                    return NotFound();
                }

                // Log success
                await _logService.CreateLog(new Log
                {
                    Action = "GetProduct",
                    Message = "Product retrieved successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log failure
                await _logService.CreateLog(new Log
                {
                    Action = "GetProduct",
                    Message = $"Error retrieving product: {ex.Message}"
                });

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _service.DeleteProduct(id);

                // Log success
                await _logService.CreateLog(new Log
                {
                    Action = "DeleteProduct",
                    Message = "Product deleted successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log failure
                await _logService.CreateLog(new Log
                {
                    Action = "DeleteProduct",
                    Message = $"Error deleting product: {ex.Message}"
                });

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, ProductDTO request)
        {
            try
            {
                var result = _service.UpdateProduct(id, request);

                if (result == null)
                {
                    await _logService.CreateLog(new Log
                    {
                        Action = "UpdateProduct",
                        Message = "Product not found"
                    });
                    return NotFound();
                }

                // Log success
                await _logService.CreateLog(new Log
                {
                    Action = "UpdateProduct",
                    Message = "Product updated successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log failure
                await _logService.CreateLog(new Log
                {
                    Action = "UpdateProduct",
                    Message = $"Error updating product: {ex.Message}"
                });

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAll
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();

                // Log success
                await _logService.CreateLog(new Log
                {
                    Action = "GetAllProducts",
                    Message = "All products retrieved successfully"
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log failure
                await _logService.CreateLog(new Log
                {
                    Action = "GetAllProducts",
                    Message = $"Error retrieving products: {ex.Message}"
                });

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetByUserId
        [HttpGet("getByOwnerId")]
        public async Task<IActionResult> GetShippingAddress(int ownerId)
        {
            try
            {
                var result = await _service.GetProductByUserId(ownerId);
                if (result == null || result.Count == 0)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region UpdateIsApproved
        [HttpPut("updateisapproved")]
        public async Task<IActionResult> UpdateIsApproved(int id)
        {
            try
            {
                var result = await _service.UpdateProductIsApproved(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAllWhereNotApproved
        [HttpGet("getallnotapproved")]
        public async Task<IActionResult> GetAllWhereNotApproved()
        {
            try
            {
                var result = await _service.GetAllWhereNotApproved();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetAllWhereApprovedAndNameLike
        [HttpGet("getallwhereapprovedandnamelike")]
        public async Task<IActionResult> GetAllWhereApprovedAndNameLike(string searchQuery)
        {
            try
            {
                var result = await _service.GetAllWhereApprovedAndNameLike(searchQuery);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region GetRandomSix
        [HttpGet("getrandomsix")]
        public async Task<IActionResult> GetRandomSix()
        {
            try
            {
                var result = await _service.GetRandomSix();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

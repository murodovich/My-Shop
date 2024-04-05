using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Produts
{
    public record ModificationProductDto
    (
        int id,
        string Name,
        string Description,
        IFormFile VideoPath,
        int SortNumber
    );
}

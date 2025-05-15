using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Product.Core.Entities;
using Product.Core.Interfaces;
using Product.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using AutoMapper;
using Product.Infrastructure.Data.DTOs;

namespace Product.Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductRepository(ApplicationDbContext context,IFileProvider fileProvider,IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(AddProductDTO productDto)
        {
            if(productDto.image is not null)
            {
                var root = "/images/product";
                var productName = $"{Guid.NewGuid()}" + productDto.image.FileName;
                if(!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var src = root + productName;
                var pic_info = _fileProvider.GetFileInfo(src);
                var root_path = pic_info.PhysicalPath;
                using (var file_streem=new FileStream(root_path,FileMode.Create))
                {
                    await productDto.image.CopyToAsync(file_streem);
                }
                var result = _mapper.Map<ProductEntity>(productDto);
                result.Photo = src;
                await _context.Products.AddAsync(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
